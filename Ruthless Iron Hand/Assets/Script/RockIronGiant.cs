using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockIronGiant : Character
{
    public GameObject[] bulletPrefabList;
    public GameObject[] enemyPrefabList;
    private Timer ShootingTime;
    private bool shootingFinish;
    public GameObject rockHandPrefab;
    private Vector3 ShotPosition;
    // Start is called before the first frame update
    protected override void Start()
    {
        ShootingTime = gameObject.AddComponent<Timer>();
        ShootingTime.Duration = 3f;
        ShootAction();
        ShotPosition= transform.position;
        ShotPosition.x = ShotPosition.x + 1.3f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(ShootingTime.Finished)
        {
            animator.SetBool("Shooting", false);
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("RockIronGiantShotStart") 
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        {
            animator.SetBool("Shooting", true);
            int i = Random.Range(1,5);
            StartCoroutine(Shoot(i));
            
        }
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("RockIronGiantRightAttack")
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        {
            animator.SetTrigger("RightHandRelease");

            RightHandAttackRelease();
        }
    }

    public void ShootAction()
    {
        animator.SetTrigger("StartShoot");
        shootingFinish = false;
    }

    public IEnumerator Shoot(int shootType)
    {
       
        switch (shootType)
        {
         
            case 1:
                {
                    ShootingTime.Duration = 3f;
                    ShootingTime.Run();
                    Vector2 d = Player.MyInstance.transform.position - ShotPosition;
                    int i = Random.Range(0, bulletPrefabList.Length);
                    ShootBullet(d, i);
                    yield return new WaitForSeconds(0.5f);
                    ShootBullet(d, i);
                    yield return new WaitForSeconds(0.5f);
                    ShootBullet(d, i);
                    yield return new WaitForSeconds(0.5f);
                    ShootBullet(d, i);
                    yield return new WaitForSeconds(0.5f);
                    ShootBullet(d, i);
                    yield return new WaitForSeconds(0.5f);
                    ShootBullet(d, i);
                    yield return new WaitForSeconds(0.5f);
                    shootingFinish = true;

                    break;
                }
            case 2:
                {
                    ShootingTime.Duration = 3f;
                    ShootingTime.Run();
                    Vector2 d = Player.MyInstance.transform.position - ShotPosition;
                    Vector2 d1 = Degree(30, d);
                    Vector2 d2 = Degree(-30, d);

                    int i = Random.Range(0, bulletPrefabList.Length);
                    ShootBullet(d, i);
                    ShootBullet(d1, i);
                    ShootBullet(d2, i);
                    yield return new WaitForSeconds(0.5f);
                    ShootBullet(d, i);
                    ShootBullet(d1, i);
                    ShootBullet(d2, i);
                    yield return new WaitForSeconds(0.5f);
                    ShootBullet(d, i);
                    ShootBullet(d1, i);
                    ShootBullet(d2, i);
                    yield return new WaitForSeconds(0.5f);
                    ShootBullet(d, i);
                    ShootBullet(d1, i);
                    ShootBullet(d2, i);
                    yield return new WaitForSeconds(0.5f);
                    ShootBullet(d, i);
                    ShootBullet(d1, i);
                    ShootBullet(d2, i);
                    yield return new WaitForSeconds(0.5f);
                    ShootBullet(d, i);
                    ShootBullet(d1, i);
                    ShootBullet(d2, i);
                    yield return new WaitForSeconds(0.5f);
                    shootingFinish = true;
                    break;
                }
            case 3:
                {
                    ShootingTime.Duration = 3f;
                    ShootingTime.Run();
                    Vector2 d = Player.MyInstance.transform.position - ShotPosition;
                    Vector2 d1 = Degree(30, d);
                    Vector2 d2 = Degree(-30, d);
                    Vector2 d3 = Degree(45, d);
                    Vector2 d4 = Degree(-45, d);
                    Vector2 d5 = Degree(60, d);
                    Vector2 d6 = Degree(-60, d);
                    Vector2 d7 = Degree(-15, d);
                    Vector2 d8 = Degree(15, d);

                    int i = Random.Range(0, bulletPrefabList.Length);
                    ShootBullet(d, i);
                    ShootBullet(d1, i);
                    ShootBullet(d2, i);
                    ShootBullet(d3, i);
                    ShootBullet(d4, i);
                    ShootBullet(d5, i);
                    ShootBullet(d6, i);
                    ShootBullet(d7, i);
                    ShootBullet(d8, i);
                    yield return new WaitForSeconds(0.5f);
                    ShootBullet(d, i);
                    ShootBullet(d1, i);
                    ShootBullet(d2, i);
                    ShootBullet(d3, i);
                    ShootBullet(d4, i);
                    ShootBullet(d5, i);
                    ShootBullet(d6, i);
                    ShootBullet(d7, i);
                    ShootBullet(d8, i);
                    yield return new WaitForSeconds(0.5f);
                    ShootBullet(d, i);
                    ShootBullet(d1, i);
                    ShootBullet(d2, i);
                    ShootBullet(d3, i);
                    ShootBullet(d4, i);
                    ShootBullet(d5, i);
                    ShootBullet(d6, i);
                    ShootBullet(d7, i);
                    ShootBullet(d8, i);
                    yield return new WaitForSeconds(0.5f);
                    ShootBullet(d, i);
                    ShootBullet(d1, i);
                    ShootBullet(d2, i);
                    ShootBullet(d3, i);
                    ShootBullet(d4, i);
                    ShootBullet(d5, i);
                    ShootBullet(d6, i);
                    ShootBullet(d7, i);
                    ShootBullet(d8, i);
                    yield return new WaitForSeconds(0.5f);
                    ShootBullet(d, i);
                    ShootBullet(d1, i);
                    ShootBullet(d2, i);
                    ShootBullet(d3, i);
                    ShootBullet(d4, i);
                    ShootBullet(d5, i);
                    ShootBullet(d6, i);
                    ShootBullet(d7, i);
                    ShootBullet(d8, i);
                    yield return new WaitForSeconds(0.5f);
                    ShootBullet(d, i);
                    ShootBullet(d1, i);
                    ShootBullet(d2, i);
                    ShootBullet(d3, i);
                    ShootBullet(d4, i);
                    ShootBullet(d5, i);
                    ShootBullet(d6, i);
                    ShootBullet(d7, i);
                    ShootBullet(d8, i);
                    yield return new WaitForSeconds(0.5f);
                    shootingFinish = true;
                    break;
                }
            case 4:
                {
                    ShootingTime.Duration = 3f;
                    ShootingTime.Run();
                    Vector2 d = Player.MyInstance.transform.position - ShotPosition;
                    Vector2 d1 = Degree(-80, d);
                    Vector2 d2 = Degree(-70, d);
                    Vector2 d3 = Degree(-60, d);
                    Vector2 d4 = Degree(-50, d);
                    Vector2 d5 = Degree(-40, d);
                    Vector2 d6 = Degree(-30, d);
                    Vector2 d7 = Degree(-20, d);
                    Vector2 d8 = Degree(-10, d);
                    Vector2 d9 = Degree(10, d);
                    Vector2 d10 = Degree(20, d);
                    Vector2 d11 = Degree(30, d);
                    Vector2 d12 = Degree(40, d);
                    Vector2 d13 = Degree(50, d);
                    Vector2 d14 = Degree(60, d);
                    Vector2 d15 = Degree(70, d);
                    Vector2 d16 = Degree(80, d);

                    int i = Random.Range(0, bulletPrefabList.Length);
                    ShootBullet(d1, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d2, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d3, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d4, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d5, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d6, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d7, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d8, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d9, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d10, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d11, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d12, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d13, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d14, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d15, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d16, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d15, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d14, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d13, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d12, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d11, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d10, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d9, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d8, i);
                    yield return new WaitForSeconds(0.1f);

                    ShootBullet(d7, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d6, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d5, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d4, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d3, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d2, i);
                    yield return new WaitForSeconds(0.1f);
                    ShootBullet(d1, i);
                    yield return new WaitForSeconds(0.1f);

                    shootingFinish = true;
                    break;
                }
        }
    }

    public void ShootBullet(Vector2 dir, int bulletType)
    {
        GameObject bullet = Instantiate(bulletPrefabList[bulletType], ShotPosition, transform.rotation);
       // Vector2 dir = Player.MyInstance.transform.position - transform.position;
        bullet.GetComponent<Rigidbody2D>().AddForce(60 * dir.normalized);

       
    }

    public void ShotEnemy()
    {
        animator.SetTrigger("StartShoot");

    }

    public void RightHandAttack()
    {
        animator.SetTrigger("RightHandAttack");
        
    }

    public void RightHandAttackRelease()
    {
        Vector2 p = Player.MyInstance.transform.position;
        p.y = p.y + 0.7f;
        GameObject rockHand = Instantiate(rockHandPrefab, p, transform.rotation);

    }

    public Vector2 Degree(float angle, Vector2 d)
    {

        Vector2 degree = Quaternion.AngleAxis(angle,Vector3.forward)*d;
        Debug.Log(Mathf.Cos(angle));
        return degree;
    }
}
