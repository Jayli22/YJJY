using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagement : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip ironhand_1;
    public AudioClip rush_1;
    public AudioClip rush_2;



    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Utils.GetBool("ironhand_attack"))
        {
            AudioSource t = gameObject.AddComponent<AudioSource>();
            t.clip = ironhand_1;
            t.Play();
            StartCoroutine(TimetoDestory(t));


            Utils.SetBool("ironhand_attack", false);
        }
        if (Utils.GetBool("player_rush"))
        {
            int i = Random.Range(1,3);
            switch (i)
            {
                case 1:
                    audioSource.clip = rush_1;
                    audioSource.Play();
                    break;
                case 2:
                    audioSource.clip = rush_2;
                    audioSource.Play();
                    break;
            }
            
            Utils.SetBool("player_rush", false);
        }
       
        
    }

    public IEnumerator TimetoDestory(AudioSource t)
    {
        yield return new WaitForSeconds(1f);
        Destroy(t);
        Debug.Log("111");
    }
}
