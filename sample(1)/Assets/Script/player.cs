using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

    Rigidbody2D rb2d;
    int playerMoveUnitsPerSecond;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        playerMoveUnitsPerSecond = 20;
    }
	
	// Update is called once per frame
	void Update () {
		
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
        
    }

}
