using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    protected Rigidbody2D rb2d;
    protected Vector2 pushdirection;
    public Timer pushed_time;
    public Timer dizzy_time;
    protected EnemyAI AI;
    //protected bool is_move;
    // Start is called before the first frame update
    protected override void Start()
    {
        pushed_time = gameObject.AddComponent<Timer>();
        dizzy_time = gameObject.AddComponent<Timer>();

        pushed_time.Duration = 1f;
        dizzy_time.Duration = 1.5f;
        rb2d = GetComponent<Rigidbody2D>();
       // is_move = false;

        AI = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (pushed_time.Finished && !is_dizzy)
        {
            // rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
            //rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            AI.moveable = true;
            animator.SetBool("move", true);
            animator.SetBool("hit", false);

        }
        if(dizzy_time.Finished)
        {
            AI.moveable = true;
            animator.SetBool("dizzy", false);
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            is_dizzy = false;
        }
        if (!is_alive)
        {
            GetComponent<Collider2D>().enabled = false;
            AI.moveable = false;
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



    public void bePushed(float pushDegree)
    {
        //is_move = true;
        AI.moveable = false;
        animator.SetBool("move", false);
        pushdirection =  transform.position - Player.MyInstance.transform.position;
        pushdirection = pushdirection.normalized;

        //float pushAngle = Mathf.Atan2(pushdirection.y, pushdirection.x);
        ////rb2d.constraints = RigidbodyConstraints2D.None;
        //pushdirection.x = Mathf.Cos(pushDegree);
        //pushdirection.y = Mathf.Sin(pushDegree);
        animator.SetBool("hit",true);

        rb2d.AddForce(3 * pushdirection, ForceMode2D.Impulse);

    }

    
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && Player.MyInstance.Hitable && is_alive)
        {
            Player.MyInstance.TakeDamage(1);

        }
        else if (collision.transform.tag == "Map")
        {
            AI.moveable = false;
            animator.SetBool("dizzy", true);
            is_dizzy = true;
            //rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            //AI.move
            dizzy_time.Run();
            Debug.Log("Wall");
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
