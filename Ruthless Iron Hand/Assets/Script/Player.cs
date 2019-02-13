using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

    Rigidbody2D rb2d;
    protected Vector2 direction;
    private static Player instance;

    int playerMoveUnitsPerSecond;
    [SerializeField]
    public GameObject prefabIronhand;
    [SerializeField]
    public GameObject rush_effect; 
    private Timer CoolDownTimer;
    private Timer RushCoolDownTimer;
    private Timer CancelTimer;
    bool canPush;
    private bool is_move;
    int healthPoint;
    Vector2 position;
    private bool hitable = true;
    private bool is_rush;

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

    public bool Hitable { get => hitable; set => hitable = value; }



    // Use this for initialization
    protected override void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        playerMoveUnitsPerSecond = 20;
        CoolDownTimer = gameObject.AddComponent<Timer>();
        CoolDownTimer.Duration = 1;
        RushCoolDownTimer = gameObject.AddComponent<Timer>();
        RushCoolDownTimer.Duration = 0.3f;
        RushCoolDownTimer.Run();
        CancelTimer = gameObject.AddComponent<Timer>();
        CancelTimer.Duration = 0.5f;
        CancelTimer.Run();
        canPush = true;
        healthPoint = 100;
    }
	
	// Update is called once per frame
    protected override void Update () {
        if(!canPush&&CoolDownTimer.Finished)
        {
            canPush = true;
        }
		
	}
    protected override void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        
            GetInput();
       
        if(is_rush)
        {
            if (CancelTimer.Finished)
            {
                StartCoroutine(Rush());

            }
            }
        else
        {
            if (CancelTimer.Finished)
            {
           
            Move();
            }
            
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
        animator.SetInteger("DirectionX", 0);
        animator.SetInteger("DirectionY", 0);

        Vector2 currentVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            direction += Vector2.up;
            animator.SetInteger("DirectionY", 1);
            is_move = true;
        }
        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            direction += Vector2.down;
            animator.SetInteger("DirectionY", -1);
            is_move = true;

        }
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            direction += Vector2.left;
            animator.SetInteger("DirectionX", -1);
            is_move = true;

        }
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            direction += Vector2.right;
            animator.SetInteger("DirectionX", 1);
            is_move = true;
        }

        if (Input.GetMouseButtonDown(1) && canPush && !is_move)
        {
            animator.SetTrigger("Attack");
            CancelTimer.Run();
            Vector2 direction_position = transform.position;
            Vector3 mouseposition = Input.mousePosition;
            Vector3 handposition = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 m_direction = mouseposition - handposition;
            //direction.z = 0f;
            m_direction = m_direction.normalized;
            float pushAngle = Mathf.Atan2(m_direction.y, m_direction.x);
            if (pushAngle * Mathf.Rad2Deg >= -45 && pushAngle * Mathf.Rad2Deg <= 45)
            {
                direction_position.x = direction_position.x + 0.1f;
                animator.SetInteger("AttackDirection", 0);

            }
            else if (pushAngle * Mathf.Rad2Deg >= 45 && pushAngle * Mathf.Rad2Deg <= 135)
            {
                direction_position.y = direction_position.y + 0.1f;
                animator.SetInteger("AttackDirection", 1);

            }
            else if (pushAngle * Mathf.Rad2Deg <= -135 || pushAngle * Mathf.Rad2Deg >= 135)
            {
                direction_position.x = direction_position.x - 0.1f;
                animator.SetInteger("AttackDirection", 2);

            }
            else if (pushAngle * Mathf.Rad2Deg <= -45 && pushAngle * Mathf.Rad2Deg >= -135)
            {
                direction_position.y = direction_position.y - 0.1f;
                animator.SetInteger("AttackDirection", 3);

            }
            GameObject ironhandobject = Instantiate(prefabIronhand, direction_position, Quaternion.identity);
            Ironhand ironhandscript = ironhandobject.GetComponent<Ironhand>();
            ironhandscript.pushing(pushAngle);
            canPush = false;
            CoolDownTimer.Run();
            
        }

        if (Input.GetKeyDown(KeyCode.Space) && RushCoolDownTimer.Finished)
        {
            is_rush = true;
        }

    }
    public void Move()
    {

        //rb2d.velocity = direction * move_speed;
        //Debug.Log(rb2d.velocity);
        //Debug.Log(rb2d.velocity.x +","+ rb2d.velocity.y);
        Vector3 dir = new Vector3(direction.x, direction.y, 0);
        transform.position += dir.normalized * move_speed * Time.deltaTime;

        if (rb2d.velocity.x == 0 && rb2d.velocity.y == 0 )
        {
            is_move = false;

        }
        else
        {
            is_move = true;
        }

    }

    public override void TakeDamage(int damage)
    {
        
            current_hp -= 1;
        
        if (current_hp <= 0)
        {
            //Invoke("ChangeNextScene", 2);

            is_alive = false;
        }
        StartCoroutine(ChangeHitable());
    }
    public IEnumerator ChangeHitable()
    {
        Hitable = false;
        Player.MyInstance.gameObject.layer = 10;
        //  GetComponent<BoxCollider2D>().enabled = 
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        Hitable = true;
        Player.MyInstance.gameObject.layer = 11;
    }

    public IEnumerator Rush()
    {
        if (is_rush)
            is_rush = false;
        RushCoolDownTimer.Run();
        Hitable = false;
        GameObject rush_effectobj = Instantiate(rush_effect, transform.position, Quaternion.identity);

        //Debug.Log("Rush");
        if (//rb2d.velocity.x == 0 && rb2d.velocity.y == 0
            direction.x == 0 && direction.y == 0)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdleUp") || animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttackUp"))
            {
                direction += Vector2.up;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdleDown") || animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttackDown"))
            {
                direction += Vector2.down;

            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdleLeft") || animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttackLeft"))
            {
                direction += Vector2.left;

            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdleRight") || animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttackRight"))
            {
                direction += Vector2.right;

            }
            Debug.Log(direction);
        }
        animator.SetTrigger("Rush");
        rb2d.velocity = direction * move_speed * 5;

        yield return new WaitForSeconds(0.2f);
        Hitable = true;
        rb2d.velocity = direction * move_speed * 5;

        yield return new WaitForSeconds(0.1f);

        Destroy(rush_effectobj);

    }

    public void bePushed(float pushDegree)
    {
        //is_move = true;
        //AI.moveable = false;
        //animator.SetBool("move", false);
        //rb2d.constraints = RigidbodyConstraints2D.None;
        Vector2 pushdirection;
        pushdirection.x = Mathf.Cos(pushDegree);
        pushdirection.y = Mathf.Sin(pushDegree);
        //animator.SetTrigger("hit");

        rb2d.AddForce(3 * pushdirection, ForceMode2D.Impulse);

    }
}
