using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : DestructibleObject
{
    public GameObject floatPillarPrefab;
    // Start is called before the first frame update
    public override void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public override void Update()
    {
    }
    public override void FixedUpdate()
    {
       
    }

    public override void bePushed(Vector2 dir)  //被击飞方法
    {
        animator.SetTrigger("Destory");
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        Vector2 p = transform.position ;
        p.x = p.x + dir.normalized.x * 0.7f;
        p.y = p.y + dir.normalized.y * 1.1f;
        Debug.Log(Mathf.Atan2(dir.y, dir.x));
        GameObject floatPillar = Instantiate(floatPillarPrefab, p, transform.rotation);
        floatPillar.GetComponent<FloatPillar>().bePushed(dir);
    }
    public override void bePushed(Vector2 dir, float time)  //被击飞方法
    {
        animator.SetTrigger("Destory");
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        Vector2 p = transform.position;
        p.x = p.x + dir.normalized.x * 0.7f;
        p.y = p.y + dir.normalized.y * 1.1f;
        Debug.Log(Mathf.Atan2(dir.y, dir.x));
        GameObject floatPillar = Instantiate(floatPillarPrefab, p, transform.rotation);
        floatPillar.GetComponent<FloatPillar>().bePushed(dir);
    }

}
