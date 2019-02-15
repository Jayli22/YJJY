using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour {
    Rigidbody2D rb2d;
    Vector2 pushdirection;
    public Timer pushed_time;
    public int Damage;
    private bool floating;
    private Animator animator;
    protected AudioSource m_audioSource;

    public AudioClip[] crush;
 

    // Use this for initialization
    void Start () {
        m_audioSource = gameObject.AddComponent<AudioSource>();
        pushed_time = gameObject.AddComponent<Timer>();
        pushed_time.Duration = 1f;
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        floating = false;
    }
	
	// Update is called once per frame
	void Update () {
       if(pushed_time.Finished)
        {
           // rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            //Debug.Log("Freeze");
        }
    }

    public void FixedUpdate()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("BoxDestruction")&& animator.GetCurrentAnimatorStateInfo(0).normalizedTime >1.0f)
        {
             Destroy(gameObject);
        }
    }
    public void bePushed(float pushDegree)
    {
        floating = true;
        animator.SetBool("Float", true);
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        pushdirection = transform.position - Player.MyInstance.transform.position;
        pushdirection = pushdirection.normalized;
        //pushdirection.x = Mathf.Cos(pushDegree);
        //pushdirection.y = Mathf.Sin(pushDegree);
        //rb2d.AddForce(500 * pushdirection, ForceMode2D.Impulse);
        rb2d.velocity = pushdirection * 5f;

    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (floating)
        {
            animator.SetBool("Destroyed",true);
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            rb2d.velocity = Vector2.zero;


            m_audioSource.clip = crush[Random.Range(0, crush.Length)];
            m_audioSource.Play();
        }
    }
}
