using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	Rigidbody2D rb2d;
	int damagePoint;
	Vector2 pushdirection;
	bool friendly;

	// Use this for initialization
	private void Awake()
	{
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		damagePoint = 10;
		friendly = false;
	}

	// Update is called once per frame
	void Update () {
		
	}
	private void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.CompareTag("player")&&!friendly)
		{
			Player playerScript = coll.gameObject.GetComponent<Player>();
			playerScript.getHurt(damagePoint);
		}
		if(coll.gameObject.CompareTag("wall")|| coll.gameObject.CompareTag("stone"))
		{
			Destroy(gameObject);
		}
	}
	public void bePushed(float pushDegree)
	{
		friendly = true;
		pushdirection.x = Mathf.Cos(pushDegree);
		pushdirection.y = Mathf.Sin(pushDegree);
		rb2d.AddForce(70 * pushdirection, ForceMode2D.Impulse);

	}
}
