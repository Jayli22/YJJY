using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatPillar : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector2 pushDirection;
    public Timer pushed_time;   // 被击飞时间
    public int Damage;
    private bool floating;  // is pushed or not
    private Animator animator;
    protected AudioSource audioSource; // music source

    public AudioClip[] crush;


    // Use this for initialization
    public virtual void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        pushed_time = gameObject.AddComponent<Timer>();
        pushed_time.Duration = 1f;
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        floating = false;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        //if (pushed_time.Finished)
        //{
        //    rb2d.velocity = Vector2.zero;
        //    rb2d.constraints = RigidbodyConstraints2D.FreezeAll; // 
        //}
    }

    public virtual void FixedUpdate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("FloatPillarDestory")  //判断破坏动画是否播放完毕
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        {
            Destroy(gameObject);
        }
    }
    public virtual void bePushed(Vector2 dir)  //被击飞方法
    {

        rb2d.velocity = dir * 5f;

    }

    protected virtual void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag != "Void")
        {
            animator.SetTrigger("Hit");
            //rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            rb2d.velocity = Vector2.zero;
            DeathExplosion();

            audioSource.clip = crush[Random.Range(0, crush.Length)];
            audioSource.Play();
        }
    }

    protected virtual void DeathExplosion()   //破坏时爆炸方法
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
                    obj.GetComponent<Character>().BePushed(dir, 0.1f);
                    obj.GetComponent<Character>().TakeDamage(10);
                }
                else if (obj.GetComponent<DestructibleObject>())
                {
                    obj.GetComponent<DestructibleObject>().bePushed(dir, 0.1f);
                }
                //obj.enabled = false;
            }
        }
        //Destroy(gameObject);
    }
}
