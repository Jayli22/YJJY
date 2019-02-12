using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    Rigidbody2D rigi;
    protected Vector2 direction;
    private static CharacterMove instance;
    public static CharacterMove MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CharacterMove>();
            }
            return instance;
        }
    }

    // Use this for initialization
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetInput();
        Move();
    }


    public void Move()
    {

        rigi.velocity = direction * 1;



    }

    private void GetInput()
    {
        direction = Vector2.zero;

        Vector2 currentVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }


    }
}