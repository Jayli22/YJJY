using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMonster : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        Vector2 movedir = Player.MyInstance.transform.position - transform.position;

        if (movedir.y > 0)
        {
            m_animator.SetBool("directionUp", true);
        }
        else
        {
            m_animator.SetBool("directionUp", false);

        }
        if (m_pushed_time.Finished && !m_is_dizzy)
        {
            // rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
            //rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            m_AI.m_moveable = true;
            //m_animator.SetBool("move", true);
            
            m_animator.SetBool("hit", false);

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
            if (!m_animator.GetCurrentAnimatorStateInfo(0).IsName("EyeMonsterDeathDown") 
                && !m_animator.GetCurrentAnimatorStateInfo(0).IsName("EyeMonsterDeathUp"))
            {
                m_animator.SetTrigger("death");
                m_rb2d.constraints = RigidbodyConstraints2D.FreezeAll;

            }
            if ((m_animator.GetCurrentAnimatorStateInfo(0).IsName("EyeMonsterDeathDown")
                || m_animator.GetCurrentAnimatorStateInfo(0).IsName("EyeMonsterDeathUp")) 
                && m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
