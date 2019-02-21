﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour {
    protected Rigidbody2D rb2d;
    protected Vector2 pushDirection;
   // public Timer pushed_time;   // 被击飞时间
    public int damage;
    private bool floating;  // is pushed or not
    protected Animator animator;
    protected AudioSource audioSource; // music source

    public AudioClip[] crush;
 

    // Use this for initialization
   public virtual void Awake () {
        audioSource = gameObject.AddComponent<AudioSource>();
      //  pushed_time = gameObject.AddComponent<Timer>();
      //  pushed_time.Duration = 1f; 
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        floating = false;
    }
	
	// Update is called once per frame
	public virtual void Update () {
       //if(pushed_time.Finished)
       // {
       //     rb2d.velocity = Vector2.zero;
       //     rb2d.constraints = RigidbodyConstraints2D.FreezeAll; // 
       // }
    }

    public virtual void FixedUpdate()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("BoxDestruction")  //判断破坏动画是否播放完毕
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >1.0f)
        {
             Destroy(gameObject);
        }
    }
    public virtual void bePushed(Vector2 dir)  //被击飞方法
    {
        floating = true;
        animator.SetBool("Float", true); 
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        gameObject.layer = 15;

        //  pushed_time.Run();
        rb2d.velocity = dir * 8f;

    }
    
    public virtual void bePushed(Vector2 dir,float pushpower)  //被击飞方法
    {
        gameObject.layer = 15;

        floating = true;
        animator.SetBool("Float", true);
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
     //   pushed_time.Duration = pushedtime;
     //   pushed_time.Run();
        rb2d.velocity = dir * pushpower;

    }

    public virtual void BossPush(Vector2 dir,float pushpower)
    {
        gameObject.layer = 18;

        floating = true;
        animator.SetBool("Float", true);
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        //   pushed_time.Duration = pushedtime;
        //   pushed_time.Run();
        rb2d.velocity = dir * pushpower;

    }
    protected virtual void OnCollisionEnter2D(Collision2D coll)
    {
        if (floating && coll.gameObject.tag != "PlayerAttack")
        {
            animator.SetBool("Destroyed",true);
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            rb2d.velocity = Vector2.zero;
            DeathExplosion();
 
            audioSource.clip = crush[Random.Range(0, crush.Length)];
            audioSource.Play();
        }
    }

    protected virtual void DeathExplosion()   //破坏时爆炸方法
    {
        // Utils.SetBool("freeze_explosion", true);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.2f);  //获取被破坏时范围内的所有物体，第一个参数为本身位置，第二个参数为判断半径
        foreach (Collider2D obj in hitColliders)
        {
            Vector2 dir;
            dir = obj.transform.position - transform.position;
            if (obj.tag == "Player")
            {
                Player.MyInstance.BePushed(dir, 0.5f);
                Player.MyInstance.TakeDamage(damage);

            }
            else if (obj.GetComponent<Character>())
                {
                obj.GetComponent<Character>().TakeDamage(damage);
                obj.GetComponent<Character>().BePushed(dir,0.1f);
                }
                else if (obj.GetComponent<DestructibleObject>())
                {
                    obj.GetComponent<DestructibleObject>().bePushed(dir);
                }
                //obj.enabled = false;
            
        }
        //Destroy(gameObject);
    }

   
}
