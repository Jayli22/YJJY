using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ironhand : MonoBehaviour {
    private Rigidbody2D m_rb2d;
    private Vector2 m_pushdirection;
    public Timer m_endTimer;
    public Timer m_attack_deltatime;
    private float m_pushDegree;
	// Use this for initialization
	void Awake () {
        m_rb2d = GetComponent<Rigidbody2D>();
        m_endTimer = gameObject.AddComponent<Timer>();
        m_endTimer.Duration = 0.5f;
        m_endTimer.Run();
        m_attack_deltatime = gameObject.AddComponent<Timer>();
        m_attack_deltatime.Duration = 0.4f;
        m_attack_deltatime.Run();

    }
	
	// Update is called once per frame
	void Update () {
       // Vector3 mouseposition = Input.mousePosition;
       // Vector3 handposition = Camera.main.WorldToScreenPoint(transform.position);
       // Vector3 direction = mouseposition - handposition;
       // direction.z = 0f;
       // direction = direction.normalized;
       // float pushangel = Mathf.Atan(direction.y / direction.x);
       // float pushdegree = Mathf.Rad2Deg * pushangel - 90;
       // transform.rotation = Quaternion.AngleAxis(pushdegree, Vector3.forward);
       if(m_endTimer.Finished)
        {
            Destroy(gameObject);
        }
       if(m_attack_deltatime.Finished)
        {
            PolygonCollider2D polycollider =  GetComponent<PolygonCollider2D>();
            polycollider.enabled = true;
        }
    }
    public void pushing(float pushAngle)
    {
        print(pushAngle*Mathf.Rad2Deg);
        m_pushDegree = pushAngle;
        m_pushdirection.x = Mathf.Cos(pushAngle);
        m_pushdirection.y = Mathf.Sin(pushAngle);
        transform.rotation = Quaternion.AngleAxis((pushAngle*Mathf.Rad2Deg) -90, Vector3.forward);
        //rb2d.AddForce(5 * pushdirection, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("Barrier"))
        {
            //Debug.Log("Barrier collider");
            Rigidbody2D target_rb2d = coll.gameObject.GetComponent<Rigidbody2D>();
            // coll.gameObject.GetComponent<DestructibleObject>().m_pushed_time.Run();
            Vector2 dir = coll.transform.position - transform.position;
            coll.gameObject.GetComponent<DestructibleObject>().bePushed(m_pushdirection);
            //PushAway(pushDegree, target_rb2d);
        }
        else if (coll.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("Enemy collider");
            Rigidbody2D target_rb2d = coll.gameObject.GetComponent<Rigidbody2D>();
            Vector2 dir = coll.transform.position - transform.position;

            coll.gameObject.GetComponent<Enemy>().BePushed(m_pushdirection, 5f);
            coll.GetComponent<Enemy>().TakeDamage(10);
            
            //PushAway(pushDegree, target_rb2d);
        }
        else if (coll.gameObject.CompareTag("Boss"))
        {
            //Debug.Log("Enemy collider");
           // Rigidbody2D target_rb2d = coll.gameObject.GetComponent<Rigidbody2D>();
            //Vector2 dir = coll.transform.position - transform.position;

           // coll.gameObject.GetComponent<Enemy>().BePushed(m_pushdirection, 5f);
            coll.GetComponent<RockIronGiant>().TakeDamage(10);

            //PushAway(pushDegree, target_rb2d);
        }
        //else if (coll.gameObject.CompareTag("bullet"))
        //{
        //    Bullet bulletScript = coll.gameObject.GetComponent<Bullet>();
        //    bulletScript.bePushed(pushDegree);
        //}
    }

    //public void PushAway(float pushDegree, Rigidbody2D rb2d)
    //{
    //    //moving = true;
    //    rb2d.constraints = RigidbodyConstraints2D.None;
    //    pushdirection.x = Mathf.Cos(pushDegree);
    //    pushdirection.y = Mathf.Sin(pushDegree);
    //    rb2d.AddForce(1 * pushdirection, ForceMode2D.Impulse);

    //}
}
