using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ironhand : MonoBehaviour {
    Rigidbody2D rb2d;
    Vector2 pushdirection;
	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mouseposition = Input.mousePosition;
        Vector3 handposition =Camera.main.WorldToScreenPoint(transform.position);
        Vector3 direction = mouseposition-handposition;
        direction.z = 0f;
        direction = direction.normalized;
        float pushangel = Mathf.Atan(direction.y / direction.x);
        float pushdegree = Mathf.Rad2Deg*pushangel-90;
        transform.rotation = Quaternion.AngleAxis(pushdegree, Vector3.forward);  
        
        if(Input.GetMouseButtonDown(1))
        {
            pushdirection.x = Mathf.Cos(pushangel);
            pushdirection.y = Mathf.Sin(pushangel);
            rb2d.AddForce(50 * pushdirection, ForceMode2D.Impulse);
        }
    }
    public void pushing(vecto)
}
