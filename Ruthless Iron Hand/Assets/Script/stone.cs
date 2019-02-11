using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour {
    Rigidbody2D rb2d;
    Vector2 pushdirection;
    public Timer pushed_time;
    bool moving;
    // Use this for initialization
    void Start () {
        pushed_time = gameObject.AddComponent<Timer>();
        pushed_time.Duration = 1f;

        rb2d = GetComponent<Rigidbody2D>();
        moving = false;
    }
	
	// Update is called once per frame
	void Update () {
       if(pushed_time.Finished)
        {
           // rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            //Debug.Log("Freeze");
        }
    }
    public void bePushed(float pushDegree)
    {
        moving = true;
        rb2d.constraints = RigidbodyConstraints2D.None;
        pushdirection.x = Mathf.Cos(pushDegree);
        pushdirection.y = Mathf.Sin(pushDegree);
        rb2d.AddForce(500 * pushdirection, ForceMode2D.Impulse);

    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        Destroy(gameObject);
    }
}
