using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public Timer m_pushed_time;
    public Timer m_dizzy_time;
    protected EnemyAI m_AI;
    protected AudioSource audioSource;
    protected bool m_is_float = false;

    //protected bool is_move;
    // Start is called before the first frame update
    protected override void Start()
    {
        m_pushed_time = gameObject.AddComponent<Timer>();
        m_dizzy_time = gameObject.AddComponent<Timer>();
        audioSource = gameObject.GetComponent<AudioSource>();

        m_pushed_time.Duration = 1f;
        m_dizzy_time.Duration = 1.5f;
        m_rb2d = GetComponent<Rigidbody2D>();
       // is_move = false;

        m_AI = GetComponent<EnemyAI>();
        m_AI.MovingSpeed = m_movespeed;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (m_pushed_time.Finished && !m_is_dizzy)
        {
            // rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
            //rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            m_AI.m_moveable = true;
            m_animator.SetBool("move", true);
            m_animator.SetBool("hit", false);
            m_is_float = false;

        }
        if (m_dizzy_time.Finished)
        {
            m_AI.m_moveable = true;
            m_animator.SetBool("dizzy", false);
            m_rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            m_is_dizzy = false;
        }
        if (!m_is_alive)
        {
            GetComponent<Collider2D>().enabled = false;
            m_AI.m_moveable = false;
            if (!m_animator.GetCurrentAnimatorStateInfo(0).IsName("SlimeDeath"))
            {
                m_animator.SetTrigger("death");
                m_rb2d.constraints = RigidbodyConstraints2D.FreezeAll;

            }
            if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("SlimeDeath") && m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
            {
                Destroy(gameObject);
            }
        }
    }



    public override void bePushed(Vector2 dir)
    {
        //is_move = true;
        m_AI.m_moveable = false;
        m_animator.SetBool("move", false);
        m_pushdirection =  transform.position - Player.MyInstance.transform.position;
        m_pushdirection = m_pushdirection.normalized;
        m_is_float = true;

    //float pushAngle = Mathf.Atan2(pushdirection.y, pushdirection.x);
    ////rb2d.constraints = RigidbodyConstraints2D.None;
    //pushdirection.x = Mathf.Cos(pushDegree);
    //pushdirection.y = Mathf.Sin(pushDegree);
    m_animator.SetBool("hit",true);

        m_rb2d.AddForce(3 * m_pushdirection, ForceMode2D.Impulse);

    }

    
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && Player.MyInstance.Hitable && m_is_alive)
        {
            Player.MyInstance.TakeDamage(1);
            

        }
        else if (collision.transform.tag == "Map" && m_is_float)
        {
            m_AI.m_moveable = false;
            m_animator.SetBool("dizzy", true);
            m_is_dizzy = true;
            //rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            //AI.move
            m_rb2d.velocity = Vector2.zero;
            m_dizzy_time.Run();
           // Debug.Log("Wall");
            // TakeDamage(10);
        }
    }

    //protected virtual void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.transform.tag == "Player" && Player.MyInstance.Hitable)
    //    {
    //        Player.MyInstance.TakeDamage(1);

    //    }
    //    else if(collision.transform.tag == "Map")
    //    {
    //        rb2d.velocity = Vector2.zero;
    //        Debug.Log("Wall");
    //       // TakeDamage(10);
    //    }
    //}




}
