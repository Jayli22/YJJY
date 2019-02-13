using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ironhand : MonoBehaviour {
    Rigidbody2D rb2d;
    Vector2 pushdirection;
    Timer EndTimer;
    Timer attack_delta_time;
    float pushDegree;
	// Use this for initialization
	void Awake () {
        rb2d = GetComponent<Rigidbody2D>();
        EndTimer = gameObject.AddComponent<Timer>();
        EndTimer.Duration = 0.5f;
        EndTimer.Run();
        attack_delta_time = gameObject.AddComponent<Timer>();
        attack_delta_time.Duration = 0.4f;
        attack_delta_time.Run();

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
       if(EndTimer.Finished)
        {
            Destroy(gameObject);
        }
       if(attack_delta_time.Finished)
        {
            PolygonCollider2D polycollider =  GetComponent<PolygonCollider2D>();
            polycollider.enabled = true;
        }
    }
    public void pushing(float pushAngle)
    {
        print(pushAngle*Mathf.Rad2Deg);
        pushDegree = pushAngle;
        pushdirection.x = Mathf.Cos(pushAngle);
        pushdirection.y = Mathf.Sin(pushAngle);
        transform.rotation = Quaternion.AngleAxis((pushAngle*Mathf.Rad2Deg) -90, Vector3.forward);
        //rb2d.AddForce(5 * pushdirection, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("Barrier"))
        {
            Debug.Log("Barrier collider");
            Rigidbody2D target_rb2d = coll.gameObject.GetComponent<Rigidbody2D>();
            coll.gameObject.GetComponent<DestructibleObject>().pushed_time.Run();
            coll.gameObject.GetComponent<DestructibleObject>().bePushed(pushDegree);
            //PushAway(pushDegree, target_rb2d);
        }
        else if (coll.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy collider");
            Rigidbody2D target_rb2d = coll.gameObject.GetComponent<Rigidbody2D>();
            coll.gameObject.GetComponent<Enemy>().pushed_time.Run();
            coll.gameObject.GetComponent<Enemy>().bePushed(pushDegree);
            coll.GetComponent<Enemy>().TakeDamage(10);
            
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
