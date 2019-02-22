using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockIronGiant : Character
{
    public GameObject[] bulletPrefabList;
    public GameObject[] enemyPrefabList;
    public GameObject[] barrierPrefabList;
    public AudioClip[] shootAudio;
    public AudioClip[] RightAttackAudio;

    private Timer shootingTime;
    private Timer attackCoolDown;

    private bool shootingFinish;
    private bool shootbarrier;
    public GameObject rockHandPrefab;
    private Vector3 ShotPosition;
    [SerializeField]
    private StatBar hpBar;
    private AudioSource audioSource;
    public GameObject EndPanel;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        shootingTime = gameObject.AddComponent<Timer>();
        shootingTime.Duration = 3f;
        ShotPosition= transform.position;
        ShotPosition.x = ShotPosition.x + 1.3f;
       hpBar.Initialize(currenthp, maxhp);
        attackCoolDown = gameObject.AddComponent<Timer>();
        attackCoolDown.Duration = 3f;
        attackCoolDown.Run();
        audioSource = gameObject.AddComponent<AudioSource>();

    }

    // Update is called once per frame
    protected override void Update()
    {
        if(shootingTime.Finished)
        {
            animator.SetBool("Shooting", false);
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("RockIronGiantShotStart") 
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        {
            animator.SetBool("Shooting", true);
            if (shootbarrier)
            {
                ShootBarrier();
            }
            else
            {
                int i = Random.Range(1, 5);

                StartCoroutine(Shoot(i));
            }
            

        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("RockIronGiantRightAttack")
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        {
            animator.SetTrigger("RightHandRelease");

            RightHandAttackRelease();
        }

        if(attackCoolDown.Finished && is_alive)
        {
            ChooseAttack();
            attackCoolDown.Run();
        }
        hpBar.MyCurrentValue = currenthp;
        hpBar.MyMaxValue = maxhp;
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
                    shootingTime.Duration = 3f;
                    shootingTime.Run();
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
                    shootingTime.Duration = 3f;
                    shootingTime.Run();
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
                    shootingTime.Duration = 3f;
                    shootingTime.Run();
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
                    shootingTime.Duration = 3f;
                    shootingTime.Run();
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
        int i = Random.Range(0, shootAudio.Length);
        audioSource.clip = shootAudio[i];
        audioSource.volume = 0.5f;
        audioSource.Play();

       
    }

    public void ShotEnemy()
    {
        animator.SetTrigger("StartShoot");
        int i = Random.Range(0, enemyPrefabList.Length);
        GameObject enemy1 = Instantiate(enemyPrefabList[i], ShotPosition, transform.rotation);
        i = Random.Range(0, enemyPrefabList.Length);
        GameObject enemy2 = Instantiate(enemyPrefabList[i], ShotPosition, transform.rotation);
        i = Random.Range(0, enemyPrefabList.Length);
        GameObject enemy3 = Instantiate(enemyPrefabList[i], ShotPosition, transform.rotation);

        enemy1.AddComponent<EnemyShoot>();
        enemy1.GetComponent<EnemyShoot>().SetLandPosition(EnemyGenerator.MyInstance.ChoosePosition());
        enemy1.GetComponent<EnemyAI>().moveable = false;
        enemy2.AddComponent<EnemyShoot>();
        enemy2.GetComponent<EnemyShoot>().SetLandPosition(EnemyGenerator.MyInstance.ChoosePosition());
        enemy2.GetComponent<EnemyAI>().moveable = false;
        enemy3.AddComponent<EnemyShoot>();
        enemy3.GetComponent<EnemyShoot>().SetLandPosition(EnemyGenerator.MyInstance.ChoosePosition());
        enemy3.GetComponent<EnemyAI>().moveable = false;
    }
    public void ShootBarrier()
    {
        animator.SetTrigger("StartShoot");
        int i = Random.Range(0, barrierPrefabList.Length);
        GameObject barrier1 = Instantiate(barrierPrefabList[i], ShotPosition, transform.rotation);
        //barrier1.AddComponent<EnemyShoot>();
        // barrier1.GetComponent<EnemyShoot>().SetLandPosition(EnemyGenerator.MyInstance.ChoosePosition(),0.01f);
        Vector2 d = Player.MyInstance.transform.position - ShotPosition;

        barrier1.GetComponent<DestructibleObject>().BossPush(d,1);
    }
    public void RightHandAttack()
    {
        animator.SetTrigger("RightHandAttack");
        audioSource.clip = RightAttackAudio[0];
        audioSource.volume = 1;

        audioSource.Play();
        
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
        //Debug.Log(Mathf.Cos(angle));
        return degree;
    }


    public override void BePushed(Vector2 dir)
    {

       
    }
    public override void BePushed(Vector2 dir, float floattime)
    {
        

    }


    public override void TakeDamage(int damage)
    {
        //health reduce 
        currenthp -= damage;
        if (currenthp <= 0)
        {
            is_alive = false;
            animator.SetTrigger("death");
            StopAllCoroutines();
            Utils.SetBool("BossOver", true);
            Player.MyInstance.GetComponent<Collider2D>().enabled = false;
            Player.MyInstance.transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
            Player.MyInstance.Hitable = false;
            Player.MyInstance.enabled = false;
            EndPanel.SetActive(true);
            //Time.timeScale = 0;
        }
    }

    public void ChooseAttack()
    {
        int i = Random.Range(1, 5);
        switch(i)
        {
            case 1:
                ShotEnemy();

                break;
            case 2:
                RightHandAttack();
                break;
            case 3:
                ShootAction();

                break;
            case 4:
                ShootBarrier();
                break;
        }

    }
}
