using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stone : MonoBehaviour {
    Rigidbody2D rb2d;
    Vector2 pushdirection;
    bool moving;
    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        moving = false;
    }
	
	// Update is called once per frame
	void Update () {
       
    }
    public void bePushed(float pushDegree)
    {
        moving = true;
        rb2d.constraints = RigidbodyConstraints2D.None;
        pushdirection.x = Mathf.Cos(pushDegree);
        pushdirection.y = Mathf.Sin(pushDegree);
        rb2d.AddForce(50 * pushdirection, ForceMode2D.Impulse);

    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("player")&&moving)
        {
            player playerScript = coll.gameObject.GetComponent<player>();
            playerScript.Freeze();
            Destroy(gameObject);
        }
    }
}
