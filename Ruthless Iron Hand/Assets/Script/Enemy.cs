using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    Rigidbody2D rb2d;
    Vector2 pushdirection;
    public Timer pushed_time;
    private EnemyAI AI;
    bool moving;
    // Start is called before the first frame update
    protected override void Start()
    {
        pushed_time = gameObject.AddComponent<Timer>();
        pushed_time.Duration = 1f;

        rb2d = GetComponent<Rigidbody2D>();
        moving = false;

        AI = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (pushed_time.Finished)
        {
            // rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
            //rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            AI.moveable = true;
            Debug.Log("Freeze");
        }

        if(!is_alive)
        {
            Destroy(gameObject);
        }
    }



    public void bePushed(float pushDegree)
    {
        moving = true;
        AI.moveable = false;

        //rb2d.constraints = RigidbodyConstraints2D.None;
        pushdirection.x = Mathf.Cos(pushDegree);
        pushdirection.y = Mathf.Sin(pushDegree);
        rb2d.AddForce(5 * pushdirection, ForceMode2D.Impulse);

    }

    
    protected  void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && Player.MyInstance.Hitable)
        {
            Player.MyInstance.TakeDamage(1);

        }
        else
        {
            TakeDamage(10);
        }
    }


       

}
