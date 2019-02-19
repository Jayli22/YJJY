using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour
{
    protected Animator animator;
    private AudioSource audioSource;

    public AudioSource AudioSource { get => audioSource; set => audioSource = value; }

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        AudioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        {
            Destroy(gameObject);
        }

    }
}
