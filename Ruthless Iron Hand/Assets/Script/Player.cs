using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

	private Rigidbody2D m_rb2d;
	protected Vector2 m_movedirection;
	private static Player m_instance;

	//int playerMoveUnitsPerSecond;
	[SerializeField]
	public GameObject m_ironhandprefab;
	[SerializeField]
	public GameObject m_rush_effect; 
	private Timer m_CoolDownTimer;
	private Timer m_RushCoolDownTimer;
	private Timer m_CancelTimer;
	private bool m_canPush;
	private bool m_ismove;
	private int m_healthPoint;
	private bool hitable = true;
	private bool is_rush;

	public static Player MyInstance
	{
		get
		{
			if (m_instance == null)
			{
				m_instance = FindObjectOfType<Player>();
			}
			return m_instance;
		}
	}

	public bool Hitable { get => hitable; set => hitable = value; }



	// Use this for initialization
	protected override void Start () {
		m_rb2d = GetComponent<Rigidbody2D>();
		//playerMoveUnitsPerSecond = 20;
		m_CoolDownTimer = gameObject.AddComponent<Timer>();
		m_CoolDownTimer.Duration = 0.5f;
		m_RushCoolDownTimer = gameObject.AddComponent<Timer>();
		m_RushCoolDownTimer.Duration = 0.3f;
		m_RushCoolDownTimer.Run();
		m_CancelTimer = gameObject.AddComponent<Timer>();
		m_CancelTimer.Duration = 0.5f;
		m_CancelTimer.Run();
		m_canPush = true;
		m_healthPoint = 100;
	}
	
	// Update is called once per frame
	protected override void Update () {
		if(!m_canPush&&m_CoolDownTimer.Finished)
		{
			m_canPush = true;
		}
		
	}
	protected override void FixedUpdate()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");

		
			GetInput();
	   
		if(is_rush)
		{
			if (m_CancelTimer.Finished)
			{
				StartCoroutine(Rush());

			}
			}
		else
		{
			if (m_CancelTimer.Finished)
			{
		   
			Move();
			}
			
		}
		
	}
	public void Freeze()
	{
		m_rb2d.velocity = new Vector2(0, 0);
	}

	public void getHurt(int damagePoint)
	{
		m_healthPoint -= damagePoint;
		print(m_healthPoint);
		if(m_healthPoint<=0)
		{
			Destroy(gameObject);
		}
	}
	private void GetInput()
	{
		m_movedirection = Vector2.zero;
		m_animator.SetInteger("DirectionX", 0);
		m_animator.SetInteger("DirectionY", 0);

		Vector2 currentVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
		
		if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
		{
			m_movedirection += Vector2.up;
			m_animator.SetInteger("DirectionY", 1);
			m_ismove = true;
		}
		if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
		{
			m_movedirection += Vector2.down;
			m_animator.SetInteger("DirectionY", -1);
			m_ismove = true;

		}
		if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
		{
			m_movedirection += Vector2.left;
			m_animator.SetInteger("DirectionX", -1);
			m_ismove = true;

		}
		if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
		{
			m_movedirection += Vector2.right;
			m_animator.SetInteger("DirectionX", 1);
			m_ismove = true;
		}

		if (Input.GetMouseButtonDown(1) && m_canPush )
		{
			m_animator.SetTrigger("Attack");
			m_CancelTimer.Run();
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
				m_animator.SetInteger("AttackDirection", 0);

			}
			else if (pushAngle * Mathf.Rad2Deg >= 45 && pushAngle * Mathf.Rad2Deg <= 135)
			{
				direction_position.y = direction_position.y + 0.1f;
				m_animator.SetInteger("AttackDirection", 1);

			}
			else if (pushAngle * Mathf.Rad2Deg <= -135 || pushAngle * Mathf.Rad2Deg >= 135)
			{
				direction_position.x = direction_position.x - 0.1f;
				m_animator.SetInteger("AttackDirection", 2);

			}
			else if (pushAngle * Mathf.Rad2Deg <= -45 && pushAngle * Mathf.Rad2Deg >= -135)
			{
				direction_position.y = direction_position.y - 0.1f;
				m_animator.SetInteger("AttackDirection", 3);

			}
			GameObject ironhandobject = Instantiate(m_ironhandprefab, direction_position, Quaternion.identity);
			Ironhand ironhandscript = ironhandobject.GetComponent<Ironhand>();
			ironhandscript.pushing(pushAngle);
			m_canPush = false;
			m_CoolDownTimer.Run();
			
		}

		if (Input.GetKeyDown(KeyCode.Space) && m_RushCoolDownTimer.Finished)
		{
			is_rush = true;
		}

	}
	public void Move()
	{

		//rb2d.velocity = direction * move_speed;
		//Debug.Log(rb2d.velocity);
		//Debug.Log(rb2d.velocity.x +","+ rb2d.velocity.y);
		Vector3 dir = new Vector3(m_movedirection.x, m_movedirection.y, 0);
		transform.position += dir.normalized * m_movespeed * Time.deltaTime;

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
			m_ismove = false;

		}
		else
		{
			m_ismove = true;
		}

	}

	public override void TakeDamage(int damage)
	{
		
			m_currenthp -= 1;
		
		if (m_currenthp <= 0)
		{
			//Invoke("ChangeNextScene", 2);

			m_is_alive = false;
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
		m_RushCoolDownTimer.Run();
		Hitable = false;
		GameObject rush_effectobj = Instantiate(m_rush_effect, transform.position, Quaternion.identity);

		//Debug.Log("Rush");
		if (//rb2d.velocity.x == 0 && rb2d.velocity.y == 0
			m_movedirection.x == 0 && m_movedirection.y == 0)
		{
			if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdleUp") || m_animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttackUp"))
			{
				m_movedirection += Vector2.up;
			}
			else if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdleDown") || m_animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttackDown"))
			{
				m_movedirection += Vector2.down;

			}
			else if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdleLeft") || m_animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttackLeft"))
			{
				m_movedirection += Vector2.left;

			}
			else if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdleRight") || m_animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttackRight"))
			{
				m_movedirection += Vector2.right;

			}
			Debug.Log(m_movedirection);
		}
		m_animator.SetTrigger("Rush");
		m_rb2d.velocity = m_movedirection * m_movespeed * 5;

		yield return new WaitForSeconds(0.2f);
		Hitable = true;
		m_rb2d.velocity = Vector2.zero;

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

		m_rb2d.AddForce(3 * pushdirection, ForceMode2D.Impulse);

	}
}
