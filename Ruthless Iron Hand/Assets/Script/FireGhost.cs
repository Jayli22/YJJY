using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGhost : Enemy
{

    public GameObject m_explosion_effect;


    private GameObject explosion_effect;

    public AudioClip[] explosion;

    protected override void Update()
    {
        if(m_bepushed)
        {

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
        if (explosion_effect!=null )
            if(explosion_effect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        {
            Destroy(explosion_effect);
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
       // Utils.SetBool("freeze_explosion", true);
        explosion_effect = Instantiate(m_explosion_effect, transform.position,transform.rotation);
        int i = Random.Range(0, explosion.Length);
        explosion_effect.GetComponent<EffectScript>().AudioSource.clip = explosion[i];
        explosion_effect.GetComponent<EffectScript>().AudioSource.Play();
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1);
        foreach(Collider2D obj in hitColliders)
        {
            Vector2 dir;
            dir = obj.transform.position - transform.position;

            if (obj.GetComponent<Rigidbody2D>())
            {
                if (obj.GetComponent<Character>())
                {
                    obj.GetComponent<Character>().bePushed(dir);
                    obj.GetComponent<Character>().TakeDamage(50);
                }
                else if (obj.GetComponent<DestructibleObject>())
                    {
                        obj.GetComponent<DestructibleObject>().bePushed(dir);
                    } 
                    //obj.enabled = false;
            }
        }
        Destroy(gameObject);
    }
    public override void TakeDamage(int damage)
    {
        //health reduce 
        m_currenthp -= damage;
        if (m_currenthp <= 0)
        {
            m_is_alive = false;
            //m_animator.SetBool("death", false);

        }
    }


    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    
        if (collision.transform.tag == "Player" && Player.MyInstance.Hitable && m_is_alive)
        {
            Player.MyInstance.TakeDamage(1);
           // DeathExplosion();


        }
        else if (collision.transform.tag == "Map" && m_is_float)
        {
            m_AI.m_moveable = false;
            m_animator.SetBool("dizzy", true);
            m_is_dizzy = true;
            //rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            //AI.move
            m_rb2d.velocity = Vector2.zero;
            //m_dizzy_time.Run();
            DeathExplosion();
            //Destroy(gameObject);
           // Debug.Log("Wall");
            // TakeDamage(10);
        }
    
    }
}
