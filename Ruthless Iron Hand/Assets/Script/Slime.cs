using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    protected override void OnCollisionEnter2D(Collision2D collision)
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
        else if (collision.transform.tag == "Enemy")
        {
            if (is_float)
            {
                collision.transform.GetComponent<Enemy>().BePushed(rb2d.velocity);
            }
        }

        if (collision.gameObject.GetComponent<RockIronGiant>())
        {
            this.TakeDamage(100);
        }
    }
}
