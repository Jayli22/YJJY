using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHandEffect : EffectScript
{
    protected override void Update()
    {
        base.Update();
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.6f)
        {
            Collider2D collider2D = GetComponent<CircleCollider2D>();
            collider2D.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player.MyInstance.TakeDamage(1);
            Player.MyInstance.BePushed(transform.position - Player.MyInstance.transform.position, 0.5f);
        }
    }
}
