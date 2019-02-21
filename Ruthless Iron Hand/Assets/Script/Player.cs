using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

	protected Vector2 movedirection;
	private static Player instance;

	//int playerMoveUnitsPerSecond;
	[SerializeField]
	public GameObject ironhandprefab;
	[SerializeField]
	public GameObject rush_effect; 
	private Timer coolDownTimer;
	private Timer rushCoolDownTimer;
	private Timer cancel_attack_Timer;
	private bool canPush;
	private bool ismove;
	private int healthPoint;
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

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

    }


    // Use this for initialization
    protected override void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		//playerMoveUnitsPerSecond = 20;
		coolDownTimer = gameObject.AddComponent<Timer>();
		coolDownTimer.Duration = 0.5f;
		rushCoolDownTimer = gameObject.AddComponent<Timer>();
		rushCoolDownTimer.Duration = 0.3f;
		rushCoolDownTimer.Run();
		cancel_attack_Timer = gameObject.AddComponent<Timer>();
		cancel_attack_Timer.Duration = 0.5f;
        pushed_time.Duration = 1f;
        dizzy_time.Duration = 1f;
        cancel_attack_Timer.Run();
		canPush = true;
		healthPoint = 100;
	}
	
	// Update is called once per frame
	protected override void Update () {
		
    }
	protected override void FixedUpdate()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");
        if(dizzy_time.Finished)
        {
            Sober();
        }
        if (!canPush && coolDownTimer.Finished)
        {
            canPush = true;
        }
        if (bepushed && pushed_time.Finished)
        {
            rb2d.velocity = Vector2.zero;
            bepushed = false;
        }
        if (bepushed || is_dizzy)
        {

        }
        else
        {
            GetInput();

        

            if (is_rush)
		    {
			    if (cancel_attack_Timer.Finished)
			    {
			    	StartCoroutine(Rush());
    
			    }
		    }
		    else
		    {
			    if (cancel_attack_Timer.Finished)
			    {
		   
			    Move();
			    }
			
		    }
        }
    }
	
	
	private void GetInput()
	{
		movedirection = Vector2.zero;
		animator.SetInteger("directionX", 0);
		animator.SetInteger("directionY", 0);

		Vector2 currentVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
		
		if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
		{
			movedirection += Vector2.up;
			animator.SetInteger("directionY", 1);
			ismove = true;
		}
		if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
		{
			movedirection += Vector2.down;
			animator.SetInteger("directionY", -1);
			ismove = true;

		}
		if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
		{
			movedirection += Vector2.left;
			animator.SetInteger("directionX", -1);
			ismove = true;

		}
		if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
		{
			movedirection += Vector2.right;
			animator.SetInteger("directionX", 1);
			ismove = true;
		}

		if (Input.GetMouseButtonDown(1) && canPush )
		{
            

			animator.SetTrigger("attack");
			cancel_attack_Timer.Run();
            Utils.SetBool("ironhand_attack", true);
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
				animator.SetInteger("attackDirection", 0);

			}
			else if (pushAngle * Mathf.Rad2Deg >= 45 && pushAngle * Mathf.Rad2Deg <= 135)
			{
				direction_position.y = direction_position.y + 0.1f;
				animator.SetInteger("attackDirection", 1);

			}
			else if (pushAngle * Mathf.Rad2Deg <= -135 || pushAngle * Mathf.Rad2Deg >= 135)
			{
				direction_position.x = direction_position.x - 0.1f;
				animator.SetInteger("attackDirection", 2);

			}
			else if (pushAngle * Mathf.Rad2Deg <= -45 && pushAngle * Mathf.Rad2Deg >= -135)
			{
				direction_position.y = direction_position.y - 0.1f;
				animator.SetInteger("attackDirection", 3);

			}
			GameObject ironhandobject = Instantiate(ironhandprefab, direction_position, Quaternion.identity);
			Ironhand ironhandscript = ironhandobject.GetComponent<Ironhand>();
			ironhandscript.pushing(pushAngle);
			canPush = false;
			coolDownTimer.Run();
			
		}

		if (Input.GetKeyDown(KeyCode.Space) && rushCoolDownTimer.Finished)
		{
			is_rush = true;
		}

	}
	public void Move()
	{

		//rb2d.velocity = m_direction * move_speed;
		//Debug.Log(rb2d.velocity);
		//Debug.Log(rb2d.velocity.x +","+ rb2d.velocity.y);
		Vector3 dir = new Vector3(movedirection.x, movedirection.y, 0);
		transform.position += dir.normalized * movespeed * Time.deltaTime;

		//if (m_rb2d.velocity.x == 0 && m_rb2d.velocity.y == 0 )
		//{
		//	m_ismove = false;

		//}
		//else
		//{
		//	m_ismove = true;
		//}
		if (dir.x == 0 && dir.y == 0)
		{
			ismove = false;

		}
		else
		{
			ismove = true;
		}

	}

	public override void TakeDamage(int damage)
	{
		
        if(hitable)
        {
            currenthp -= damage;

        }

        if (currenthp <= 0)
        {
            //Invoke("ChangeNextScene", 2);

            Death();
        }
        else
        {
            StartCoroutine(ChangeHitable());
        }
	}
	public IEnumerator ChangeHitable()
	{
		Hitable = false;
		Player.MyInstance.gameObject.layer = 10;
        Transform child = transform.GetChild(0);
        child.gameObject.layer = 10;
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
        child.gameObject.layer = 12;

        Player.MyInstance.gameObject.layer = 11;
	}

	public IEnumerator Rush()
	{
		if (is_rush)
			is_rush = false;
        Utils.SetBool("player_rush", true);

        rushCoolDownTimer.Run();
        gameObject.layer = 10;
        Transform child = transform.GetChild(0);
        child.gameObject.layer = 10;
        Hitable = false;

		GameObject rush_effectobj = Instantiate(rush_effect, transform.position, Quaternion.identity);

        //Debug.Log("Rush");
        if (//rb2d.velocity.x == 0 && rb2d.velocity.y == 0
			movedirection.x == 0 && movedirection.y == 0)
		{
			if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdleUp") || animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttackUp"))
			{
				movedirection += Vector2.up;
			}
			else if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdleDown") || animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttackDown"))
			{
				movedirection += Vector2.down;

			}
			else if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdleLeft") || animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttackLeft"))
			{
				movedirection += Vector2.left;

			}
			else if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdleRight") || animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttackRight"))
			{
				movedirection += Vector2.right;

			}
		}


        animator.SetTrigger("rush");
        rb2d.velocity = movedirection * movespeed * 5;
        Debug.Log(rb2d.velocity);

        Vector3 dir = new Vector3(movedirection.x, movedirection.y, 0);
       // transform.position += dir.normalized * m_movespeed * 60 * Time.deltaTime;
        yield return new WaitForSeconds(0.2f);
		Hitable = true;
		rb2d.velocity = Vector2.zero;
        gameObject.layer = 11;
       child.gameObject.layer = 12;

        yield return new WaitForSeconds(0.1f);

		Destroy(rush_effectobj);

	}

    public override void BePushed(Vector2 dir)
    {

        bepushed = true;
        dir = dir.normalized;

        Debug.Log("push");
        pushed_time.Run();
        rb2d.velocity = 3 * dir * movespeed;

    }
    public override void BePushed(Vector2 dir, float floattime)
    {
        //is_move = true;

        //m_pushdirection = transform.position - Player.MyInstance.transform.position;
        //m_pushdirection = m_pushdirection.normalized;
        bepushed = true;
        ////rb2d.constraints = RigidbodyConstraints2D.None;
        //pushdirection.x = Mathf.Cos(pushDegree);
        //pushdirection.y = Mathf.Sin(pushDegree);
        //float pushAngle = Mathf.Atan2(pushdirection.y, pushdirection.x);
        //m_animator.SetBool("hit", true);
        pushed_time.Duration = floattime;
        pushed_time.Run();
        //m_rb2d.AddForce(3 * dir, ForceMode2D.Impulse);
        rb2d.velocity = 3 * dir * movespeed;

    }

    protected  void OnCollisionEnter2D(Collision2D collision)
    {        
        if ((collision.transform.tag == "Map" || collision.transform.tag == "Barrier"|| collision.transform.tag == "Void")  && bepushed)
        {
            animator.SetBool("dizzy", true);
            is_dizzy = true;
            dizzy_time.Run();
            //rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            //AI.move
            rb2d.velocity = Vector2.zero;

        }

    }

    protected void Death()
    {
        GetComponent<Collider2D>().enabled = false;
        Transform child = transform.GetChild(0);
        Destroy(child.gameObject);
        is_alive = false;
        animator.SetTrigger("death");
        //if (!animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerDeath"))
        //{
        //    animator.SetTrigger("death");
        //    rb2d.constraints = RigidbodyConstraints2D.FreezeAll;

        //}
        rb2d.velocity = Vector2.zero;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerDeath")
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        {
            Destroy(gameObject);
        }
    }
}
