using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

    Rigidbody2D rb2d;
    int playerMoveUnitsPerSecond;
    [SerializeField]
    GameObject prefabIronhand;
    Timer CoolDownTimer;
    bool canPush;
    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        playerMoveUnitsPerSecond = 20;
        CoolDownTimer = gameObject.AddComponent<Timer>();
        CoolDownTimer.Duration = 1;
        canPush = true;
    }
	
	// Update is called once per frame
	void Update () {
        if(!canPush&&CoolDownTimer.Finished)
        {
            canPush = true;
        }
		
	}
    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput= Input.GetAxis("Vertical");
        if ( horizontalInput != 0|| verticalInput != 0)
        {
            Vector2 position = rb2d.position;
            position.x += horizontalInput * playerMoveUnitsPerSecond *
                Time.deltaTime;
            position.y += verticalInput * playerMoveUnitsPerSecond *
                Time.deltaTime;
            rb2d.MovePosition(position);
        }
        if (Input.GetMouseButtonDown(1) && canPush )
        {
            GameObject ironhandobject = Instantiate(prefabIronhand, transform.position, Quaternion.identity);
            ironhand ironhandscript = ironhandobject.GetComponent<ironhand>();
            Vector3 mouseposition = Input.mousePosition;
            Vector3 handposition = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 direction = mouseposition - handposition;
            direction.z = 0f;  
            direction = direction.normalized;
            float pushAngle = Mathf.Atan2(direction.y,direction.x);
            ironhandscript.pushing(pushAngle);
            canPush = false;
            CoolDownTimer.Run();

        }

    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("stone"))
        {

        }
    }

}
