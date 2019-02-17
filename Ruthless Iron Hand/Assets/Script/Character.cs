using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int maxhp;
    public int currenthp;
    //private int max_mp;
    //private int current_mp;
    public float movespeed;
    protected bool is_alive;
    protected bool is_dizzy = false;
    protected Animator animator;
    protected bool bepushed;

    protected Timer pushed_time;
    protected Timer dizzy_time;

    protected Vector2 pushdirection;
    protected Rigidbody2D rb2d;


    // Start is called before the first frame update
    protected virtual void Start()
    {
       
    }

    protected virtual void Awake()
    {
        is_alive = true;
        animator = GetComponent<Animator>();
        pushed_time = gameObject.AddComponent<Timer>();
        dizzy_time = gameObject.AddComponent<Timer>();
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
        currenthp -= damage;
        if (currenthp <= 0)
        {
            is_alive = false;
            //m_animator.SetBool("death", false);

        }
    }

    public virtual void BePushed(Vector2 dir)
    {
        //is_move = true;
        
        //m_pushdirection = transform.position - Player.MyInstance.transform.position;
        //m_pushdirection = m_pushdirection.normalized;
        bepushed = true;
        dir = dir.normalized;
        ////rb2d.constraints = RigidbodyConstraints2D.None;
        //pushdirection.x = Mathf.Cos(pushDegree);
        //pushdirection.y = Mathf.Sin(pushDegree);
        //float pushAngle = Mathf.Atan2(pushdirection.y, pushdirection.x);
        //m_animator.SetBool("hit", true);
        Debug.Log("push");
        pushed_time.Run();
        // m_rb2d.AddForce(3 * dir, ForceMode2D.Impulse);]
        rb2d.velocity = 3 * dir * movespeed;

    }
    public virtual void BePushed(Vector2 dir, float floattime)
    {
        //is_move = true;

        //m_pushdirection = transform.position - Player.MyInstance.transform.position;
        //m_pushdirection = m_pushdirection.normalized;
        bepushed = true;

        ////rb2d.constraints = RigidbodyConstraints2D.None;
        //pushdirection.x = Mathf.Cos(pushDegree);
        //pushdirection.y = Mathf.Sin(pushDegree);
        //float pushAngle = Mathf.Atan2(pushdirection.y, pushdirection.x);
        //m_animator.SetBool("hit", true);

        pushed_time.Run();
        //m_rb2d.AddForce(3 * dir, ForceMode2D.Impulse);
        rb2d.velocity = 3 * dir * movespeed;

    }
    public virtual void Stun(float stuntime)
    {
        is_dizzy = true;
        animator.SetBool("dizzy", true);
        rb2d.velocity = Vector2.zero;
        dizzy_time.Duration = stuntime;
        dizzy_time.Run();
    }
    public virtual void Stun()
    {
        is_dizzy = true;
        animator.SetBool("dizzy", true);
        rb2d.velocity = Vector2.zero;
        dizzy_time.Run();

    }
    public virtual void Sober()
    {
        is_dizzy = false;
        animator.SetBool("dizzy", false);
    }
}
