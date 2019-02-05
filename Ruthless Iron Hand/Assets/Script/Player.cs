using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Rigidbody2D rb2d;
    protected Vector2 direction;
    private static Player instance;

    int playerMoveUnitsPerSecond;
    [SerializeField]
    GameObject prefabIronhand;
    Timer CoolDownTimer;
    bool canPush;
    int healthPoint;
    Vector2 position;

    public static Player MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }
            return instance;
        }
    }



    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        playerMoveUnitsPerSecond = 20;
        CoolDownTimer = gameObject.AddComponent<Timer>();
        CoolDownTimer.Duration = 1;
        canPush = true;
        healthPoint = 100;
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
        float verticalInput = Input.GetAxis("Vertical");
        GetInput();
        Move();
        if (Input.GetMouseButtonDown(1) && canPush)
        {
            GameObject ironhandobject = Instantiate(prefabIronhand, transform.position, Quaternion.identity);
            Ironhand ironhandscript = ironhandobject.GetComponent<Ironhand>();
            Vector3 mouseposition = Input.mousePosition;
            Vector3 handposition = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 direction = mouseposition - handposition;
            direction.z = 0f;
            direction = direction.normalized;
            float pushAngle = Mathf.Atan2(direction.y, direction.x);
            ironhandscript.pushing(pushAngle);
            canPush = false;
            CoolDownTimer.Run();
        }

    }
    public void Freeze()
    {
        rb2d.velocity = new Vector2(0, 0);
    }

    public void getHurt(int damagePoint)
    {
        healthPoint -= damagePoint;
        print(healthPoint);
        if(healthPoint<=0)
        {
            Destroy(gameObject);
        }
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
    public void Move()
    {

        rb2d.velocity = direction * 1;



    }
}
