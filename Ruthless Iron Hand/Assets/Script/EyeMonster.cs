using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMonster : Enemy
{
    // Start is called before the first frame update
    private Timer shottime_cooldown;
    private bool is_shotting = false;
    public GameObject bulletPrefab;

    protected override void Start()
    {
        base.Start();
        shottime_cooldown = gameObject.AddComponent<Timer>();
        shottime_cooldown.Duration = 1.5f;
        shottime_cooldown.Run();
    }

    // Update is called once per frame
    protected override void Update()
    {

        if(is_shotting)
        {
            Stopmoving();
        }
        else
        {
            Startmoving();
        }

        if(shottime_cooldown.Finished)
        {
            StartCoroutine(Shot());
        }



        Vector2 movedir = Player.MyInstance.transform.position - transform.position;

        if (movedir.y > 0)
        {
            animator.SetBool("directionUp", true);
        }
        else
        {
            animator.SetBool("directionUp", false);

        }
        if (pushed_time.Finished && !is_dizzy)
        {
            // rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
            //rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            thisAI.moveable = true;
            //m_animator.SetBool("move", true);
            
            animator.SetBool("hit", false);  

        }
        if (dizzy_time.Finished)
        {
            thisAI.moveable = true;
            animator.SetBool("dizzy", false);
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            is_dizzy = false;
        }
        if (!is_alive)
        {
            GetComponent<Collider2D>().enabled = false;
            thisAI.moveable = false;
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("EyeMonsterDeathDown") 
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("EyeMonsterDeathUp"))
            {
                animator.SetTrigger("death");
                rb2d.constraints = RigidbodyConstraints2D.FreezeAll;

            }
            if ((animator.GetCurrentAnimatorStateInfo(0).IsName("EyeMonsterDeathDown")
                || animator.GetCurrentAnimatorStateInfo(0).IsName("EyeMonsterDeathUp")) 
                && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
            {
                Destroy(gameObject);
            }
        }
    }


    public IEnumerator Shot()
    {
        shottime_cooldown.Run();
        animator.SetBool("attack", true);
        is_shotting = true;
        GameObject bullet = Instantiate(bulletPrefab,transform.position,transform.rotation);
        Vector2 dir = Player.MyInstance.transform.position - transform.position;
        bullet.GetComponent<Rigidbody2D>().AddForce(60 * dir.normalized);
        yield return new WaitForSeconds(0.5f);
        is_shotting = false;
        animator.SetBool("attack", false);

    }
}
