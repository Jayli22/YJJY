using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int max_hp;
    public int current_hp;
    //private int max_mp;
    //private int current_mp;
    public float move_speed;
    protected bool is_alive;
    protected bool is_dizzy;
    protected Animator animator;


    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    protected virtual void Awake()
    {
        is_alive = true;
        animator = GetComponent<Animator>();
    
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate()
    {

    }


    public virtual void TakeDamage(int damage)
    {
        //health reduce 
        current_hp -= damage;
        if (current_hp <= 0)
        {
            is_alive = false;
            animator.SetBool("is_alive", false);

        }
    }
}
