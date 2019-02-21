using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceGhost : Enemy
{
    public GameObject m_freeze_effect;


    private GameObject freeze_effect;

    public AudioClip[] explosion;

    protected override void Update()
    {



        if (pushed_time.Finished && !is_dizzy)
        {
            // rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
            //rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            thisAI.moveable = true;
            is_float = false;
            rb2d.velocity = Vector2.zero;
            animator.SetBool("move", true);
            animator.SetBool("hit", false);

        }
        if (dizzy_time.Finished)
        {
            thisAI.moveable = true;
            animator.SetBool("dizzy", false);
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            is_dizzy = false;
        }
        if (freeze_effect != null)
            if (freeze_effect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
            {
                Destroy(freeze_effect);
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

    protected void DeathExplosion()
    {
        Utils.SetBool("ice_explosion", true);

        Vector2 p;
        p.x = transform.position.x;
        p.y = transform.position.y + 1;
        freeze_effect = Instantiate(m_freeze_effect, p, transform.rotation);
        // Utils.SetBool("freeze_explosion", true);
        int i = Random.Range(0, explosion.Length);
        freeze_effect.GetComponent<EffectScript>().AudioSource.clip = explosion[i];
        freeze_effect.GetComponent<EffectScript>().AudioSource.Play();
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1);
        foreach (Collider2D obj in hitColliders)
        {
            Vector2 dir;
            dir = obj.transform.position - transform.position;

           
                if (obj.GetComponent<Player>())
                {
                    obj.GetComponent<Player>().Stun(1.5f);
                    //obj.GetComponent<Player>().TakeDamage(50);
                    obj.GetComponent<Animator>().SetBool("dizzy", true);

                }
                else if (obj.GetComponent<DestructibleObject>())
                {
                    obj.GetComponent<DestructibleObject>().bePushed(dir);
                }
                else if(obj.GetComponent<Enemy>())
                {
                    obj.GetComponent<Enemy>().TakeDamage(50);
                    obj.GetComponent<Enemy>().Stun(1f);
                    obj.GetComponent<Animator>().SetBool("dizzy", true);
                }
                else if(obj.GetComponent<RockIronGiant>())
            {
                obj.GetComponent<RockIronGiant>().TakeDamage(50);

            }
            //obj.enabled = false;

        }
        Destroy(gameObject);
    }
    public override void TakeDamage(int damage)
    {
        //health reduce 
        currenthp -= damage;
        if (currenthp <= 0)
        {
            is_alive = false;
            //DeathExplosion();
            //m_animator.SetBool("death", false);

        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (collision.transform.tag == "Player" && Player.MyInstance.Hitable && is_alive)
        {
            Player.MyInstance.TakeDamage(10);
            // DeathExplosion();


        }
        else if ((collision.transform.tag == "Map" || collision.transform.tag == "Void" || collision.transform.tag == "Barrier" || collision.transform.tag == "Boss") && is_float)
        {
            Stun();
           
            //m_dizzy_time.Run();
            DeathExplosion();
            TakeDamage(10);
            //Destroy(gameObject);
            // Debug.Log("Wall");
            // TakeDamage(10);
        }
        else if (collision.transform.tag == "Enemy")
        {
            if (is_float)
            {
                //  collision.transform.GetComponent<Enemy>().BePushed(rb2d.velocity);
                DeathExplosion();

            }
        }

    }
}
