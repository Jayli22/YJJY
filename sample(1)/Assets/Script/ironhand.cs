using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ironhand : MonoBehaviour {
    Rigidbody2D rb2d;
    Vector2 pushdirection;
    Timer EndTimer;
	// Use this for initialization
	void Awake () {
        rb2d = GetComponent<Rigidbody2D>();
        EndTimer = gameObject.AddComponent<Timer>();
        EndTimer.Duration = 0.1f;
        EndTimer.Run();
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
    }
    public void pushing(float pushAngle)
    {
        print(pushAngle*Mathf.Rad2Deg);
        pushdirection.x = Mathf.Cos(pushAngle);
        pushdirection.y = Mathf.Sin(pushAngle);
        transform.rotation = Quaternion.AngleAxis((pushAngle*Mathf.Rad2Deg)-90, Vector3.forward);
        rb2d.AddForce(50 * pushdirection, ForceMode2D.Impulse);
    }
}
