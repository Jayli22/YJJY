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
    protected bool m_bepushed;
    protected Timer bepushed_time;

    protected Vector2 m_pushdirection;
    protected Rigidbody2D m_rb2d;

    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    protected virtual void Awake()
    {
        m_is_alive = true;
        m_animator = GetComponent<Animator>();
        bepushed_time = gameObject.AddComponent<Timer>();
        bepushed_time.Duration = 1f;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (bepushed_time.Finished)
        {
            m_rb2d.velocity = Vector2.zero;
            m_bepushed = false;
        }
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

    public virtual void bePushed(Vector2 dir)
    {
        //is_move = true;
        
        //m_pushdirection = transform.position - Player.MyInstance.transform.position;
        //m_pushdirection = m_pushdirection.normalized;
        m_bepushed = true;

        ////rb2d.constraints = RigidbodyConstraints2D.None;
        //pushdirection.x = Mathf.Cos(pushDegree);
        //pushdirection.y = Mathf.Sin(pushDegree);
        //float pushAngle = Mathf.Atan2(pushdirection.y, pushdirection.x);

        //m_animator.SetBool("hit", true);
        bepushed_time.Run();
        m_rb2d.AddForce(3 * dir, ForceMode2D.Impulse);

    }
}
