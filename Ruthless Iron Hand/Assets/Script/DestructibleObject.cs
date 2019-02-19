using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour {
    Rigidbody2D rb2d;
    Vector2 pushdirection;
    public Timer pushed_time;   // 被击飞时间
    public int Damage;
    private bool floating;  // is pushed or not
    private Animator animator;
    protected AudioSource audioSource; // music source

    public AudioClip[] crush;
 

    // Use this for initialization
    void Start () {
        audioSource = gameObject.AddComponent<AudioSource>();
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
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll; // 
        }
    }

    public void FixedUpdate()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("BoxDestruction")  //判断破坏动画是否播放完毕
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >1.0f)
        {
             Destroy(gameObject);
        }
    }
    public void bePushed(Vector2 dir)  //被击飞方法
    {
        floating = true;
        animator.SetBool("Float", true); 
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation; 

        pushed_time.Run();
        rb2d.velocity = dir * 5f;

    }
    public void bePushed(Vector2 dir,float pushedtime)  //被击飞方法
    {
        floating = true;
        animator.SetBool("Float", true);
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        pushed_time.Duration = pushedtime;
        pushed_time.Run();
        rb2d.velocity = dir * 5f;

    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (floating && coll.collider.tag != "Void")
        {
            animator.SetBool("Destroyed",true);
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            rb2d.velocity = Vector2.zero;
            DeathExplosion();
 
            audioSource.clip = crush[Random.Range(0, crush.Length)];
            audioSource.Play();
        }
    }

    protected void DeathExplosion()   //破坏时爆炸方法
    {
        // Utils.SetBool("freeze_explosion", true);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);  //获取被破坏时范围内的所有物体，第一个参数为本身位置，第二个参数为判断半径
        foreach (Collider2D obj in hitColliders)
        {
            Vector2 dir;
            dir = obj.transform.position - transform.position;

            if (obj.GetComponent<Rigidbody2D>())
            {
                if (obj.GetComponent<Character>())
                {
                    obj.GetComponent<Character>().BePushed(dir,0.1f);
                    obj.GetComponent<Character>().TakeDamage(10);
                }
                else if (obj.GetComponent<DestructibleObject>())
                {
                    obj.GetComponent<DestructibleObject>().bePushed(dir,0.1f);
                }
                //obj.enabled = false;
            }
        }
        //Destroy(gameObject);
    }
}
