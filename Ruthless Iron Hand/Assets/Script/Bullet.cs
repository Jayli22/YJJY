using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	private Rigidbody2D rb2d;
	public int damagePoint = 1;
	private Vector3 shotdirection;
	private bool friendly = false;
    public float move_speed;
	// Use this for initialization
	private void Awake()
	{
		rb2d = gameObject.GetComponent<Rigidbody2D>();
        shotdirection = Player.MyInstance.transform.position - transform.position;

    }

	// Update is called once per frame
	void Update () {
        //transform.position += shotdirection.normalized * move_speed * Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.CompareTag("Player")&&!friendly)
		{
            Player.MyInstance.TakeDamage(1);
		}
		if(coll.gameObject.CompareTag("Map"))
		{
		}
        Destroy(gameObject);

    }
    public void bePushed(float pushDegree)
	{
		//friendly = true;
		//pushdirection.x = Mathf.Cos(pushDegree);
		//pushdirection.y = Mathf.Sin(pushDegree);
		//rb2d.AddForce(70 * pushdirection, ForceMode2D.Impulse);

	}
}
