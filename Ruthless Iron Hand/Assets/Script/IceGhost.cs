using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceGhost : Enemy
{
    public GameObject m_freeze_effect;


    private GameObject freeze_effect;

    public AudioClip explosion_1;
    public AudioClip explosion_2;
    public AudioClip explosion_3;
    public AudioClip explosion_4;

    protected override void Update()
    {


        if (Utils.GetBool("ice_explosion"))
        {


            audioSource.clip = explosion_4;
            audioSource.Play();


            Utils.SetBool("ice_explosion", false);
        }


        if (m_pushed_time.Finished && !m_is_dizzy)
        {
            // rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
            //rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            m_AI.m_moveable = true;
            m_animator.SetBool("move", true);
            m_animator.SetBool("hit", false);

        }
        if (m_dizzy_time.Finished)
        {
            m_AI.m_moveable = true;
            m_animator.SetBool("dizzy", false);
            m_rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            m_is_dizzy = false;
        }
        if (freeze_effect != null)
            if (freeze_effect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
            {
                Destroy(freeze_effect);
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
                //Debug.Log("destory");
            }
        }


    }

    protected void DeathExplosion()
    {
        Utils.SetBool("ice_explosion", true);

        Vector2 p;
        p.x = transform.position.x;
        p.y = transform.position.y + 1;
        freeze_effect = Instantiate(m_freeze_effect, p, transform.rotation);
    }
    public override void TakeDamage(int damage)
    {
        //health reduce 
        m_currenthp -= damage;
        if (m_currenthp <= 0)
        {
            m_is_alive = false;
            DeathExplosion();
            //m_animator.SetBool("death", false);

        }
    }
}
