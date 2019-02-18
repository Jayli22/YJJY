using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonEffect : MonoBehaviour
{
    private  GameObject generateObject;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        {
            Instantiate(generateObject, transform.position, transform.rotation);
            Debug.Log("this");
            Destroy(gameObject);
        }
    }
    public void SetObjectType(GameObject g)
    {
        generateObject = g;
    }
}
