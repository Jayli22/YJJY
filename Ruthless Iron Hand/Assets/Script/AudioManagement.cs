using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagement : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] ironhand;
    public AudioClip[] rush;
    public AudioClip[] bgm;


    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        int ri = Random.Range(0, bgm.Length);
        audioSource.clip = bgm[ri];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(!audioSource.isPlaying)
        {
            int ri = Random.Range(0, bgm.Length);
            audioSource.clip = bgm[ri];
            audioSource.Play();
        }
        if(Utils.GetBool("ironhand_attack"))
        {
            AudioSource t = gameObject.AddComponent<AudioSource>();
            int i = Random.Range(0, ironhand.Length);
            t.clip = ironhand[i];
            t.Play();
            StartCoroutine(TimetoDestory(t));


            Utils.SetBool("ironhand_attack", false);
        }
        if (Utils.GetBool("player_rush"))
        {
            AudioSource t = gameObject.AddComponent<AudioSource>();

            int i = Random.Range(1,rush.Length);
            t.clip = rush[i];
            t.Play();
            StartCoroutine(TimetoDestory(t));


            Utils.SetBool("player_rush", false);
        }
       
        
    }

    public IEnumerator TimetoDestory(AudioSource t)
    {
        yield return new WaitForSeconds(1f);
        Destroy(t);
    }
}
