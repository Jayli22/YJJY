using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickController : MonoBehaviour
{

    public AudioClip cilckClip;

   
    public void Click(string scene)
    {
        SceneManager.LoadScene(scene);
        Utils.SetInt("Level", 1);
       AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = cilckClip;
        audioSource.Play();
    }
    public void Exit()
    {
        Application.Quit();
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = cilckClip;
        audioSource.Play();

    }
}
