using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour
{
    private Animator m_animator;
    private AudioSource m_audioSource;

    public AudioSource AudioSource { get => m_audioSource; set => m_audioSource = value; }

    // Start is called before the first frame update
    void Awake()
    {
        m_animator = GetComponent<Animator>();
        AudioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        {
            Destroy(gameObject);
        }

    }
}
