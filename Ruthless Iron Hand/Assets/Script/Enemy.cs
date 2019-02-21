using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    
    protected EnemyAI thisAI;
    protected AudioSource audioSource;

    //protected bool is_move;
    // Start is called before the first frame update
    protected override void Start()
    {
        pushed_time = gameObject.AddComponent<Timer>();
        dizzy_time = gameObject.AddComponent<Timer>();
        audioSource = gameObject.GetComponent<AudioSource>();

        pushed_time.Duration = 1f;
        dizzy_time.Duration = 1.5f;
        rb2d = GetComponent<Rigidbody2D>();
       // is_move = false;

        thisAI = GetComponent<EnemyAI>();
        thisAI.MovingSpeed = movespeed;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (pushed_time.Finished && !is_dizzy)
        {
            // rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
            //rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            thisAI.moveable = true;
            rb2d.velocity = Vector2.zero;
            animator.SetBool("move", true);
            animator.SetBool("hit", false);
            is_float = false;

        }
        if (dizzy_time.Finished)
        {
            Sober();
            if(!pushed_time.Finished)
            {
                thisAI.moveable = true;
                rb2d.velocity = Vector2.zero;
                animator.SetBool("move", true);
                animator.SetBool("hit", false);
                is_float = false;
            }
        }
        if (!is_alive)
        {
            GetComponent<Collider2D>().enabled = false;
            thisAI.moveable = false;
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SlimeDeath"))
            {
                animator.SetTrigger("death");
                rb2d.constraints = RigidbodyConstraints2D.FreezeAll;

            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("SlimeDeath") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
            {
                Destroy(gameObject);
            }
        }
    }



    public override void BePushed(Vector2 dir)
    {
        //is_move = true;
        thisAI.moveable = false;
        //m_animator.SetBool("move", false);
        //m_pushdirection =  transform.position - Player.MyInstance.transform.position;
        //m_pushdirection = m_pushdirection.normalized;
        dir = dir.normalized;
        is_float = true;
        pushed_time.Run();

        //float pushAngle = Mathf.Atan2(pushdirection.y, pushdirection.x);
        ////rb2d.constraints = RigidbodyConstraints2D.None;
        //pushdirection.x = Mathf.Cos(pushDegree);
        //pushdirection.y = Mathf.Sin(pushDegree);
        animator.SetBool("hit",true);

        rb2d.AddForce(3 * dir, ForceMode2D.Impulse);

    }

    public override void BePushed(Vector2 dir, float floattime)
    {
        //is_move = true;
        thisAI.moveable = false;
        //m_animator.SetBool("move", false);
        //m_pushdirection = transform.position - Player.MyInstance.transform.position;
        //m_pushdirection = m_pushdirection.normalized;
        dir = dir.normalized;
        is_float = true;
        pushed_time.Duration = floattime;
        pushed_time.Run();

        //float pushAngle = Mathf.Atan2(pushdirection.y, pushdirection.x);
        ////rb2d.constraints = RigidbodyConstraints2D.None;
        //pushdirection.x = Mathf.Cos(pushDegree);
        //pushdirection.y = Mathf.Sin(pushDegree);
        animator.SetBool("hit", true);

        rb2d.AddForce(3 * dir, ForceMode2D.Impulse);

    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && Player.MyInstance.Hitable && is_alive)
        {
            Player.MyInstance.TakeDamage(10);
            

        }
        else if ((collision.transform.tag == "Map" || collision.transform.tag == "Void" || collision.transform.tag == "Barrier") && is_float)
        {
            Stun();
            TakeDamage(10);
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

    public override void Stun()
    {
        is_dizzy = true;
        thisAI.moveable = false;
        rb2d.velocity = Vector2.zero;
        animator.SetBool("dizzy", true);
        dizzy_time.Run();


    }
    public override void Sober()
    {
        thisAI.moveable = true;
        animator.SetBool("dizzy", false);
       // m_rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        is_dizzy = false;
    }

    public void Stopmoving()
    {
        thisAI.moveable = false;
    }

    public void Startmoving()
    {
        thisAI.moveable = true;
    }
}
