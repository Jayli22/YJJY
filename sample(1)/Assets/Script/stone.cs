using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stone : MonoBehaviour {
    Rigidbody2D rb2d;
    Vector2 pushdirection;
    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
       
    }
    public void bePushed(float pushDegree)
    {
        pushdirection.x = Mathf.Cos(pushDegree);
        pushdirection.y = Mathf.Sin(pushDegree);
        rb2d.AddForce(50 * pushdirection, ForceMode2D.Impulse);

    }
}
