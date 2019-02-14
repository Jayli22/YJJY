using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int m_maxhp;
    public int m_currenthp;
    //private int max_mp;
    //private int current_mp;
    public float m_movespeed;
    protected bool m_is_alive;
    protected bool m_is_dizzy;
    protected Animator m_animator;


    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    protected virtual void Awake()
    {
        m_is_alive = true;
        m_animator = GetComponent<Animator>();
    
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
        m_currenthp -= damage;
        if (m_currenthp <= 0)
        {
            m_is_alive = false;
            //m_animator.SetBool("death", false);

        }
    }
}
