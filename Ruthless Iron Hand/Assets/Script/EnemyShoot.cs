using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    private Vector2 landPosition;
    private Vector2 startPosition;
    private Timer changeTimer;
    private float t;
    private bool isTarget = false;
    private GameObject target;
    private float landSpeed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        changeTimer = gameObject.AddComponent<Timer>();
        changeTimer.Duration = 0.01f;
        changeTimer.Run();
        startPosition = transform.position;
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isTarget)
        {
            target = Instantiate(targetPrefab, landPosition, transform.rotation);
            isTarget = true;
            Debug.Log("target");
        }
        if (changeTimer.Finished)
        {
            changeTimer.Run();
            transform.position =  Vector2.Lerp(startPosition, landPosition, t);
            t += landSpeed;
            //Debug.Log(t);
        }
        if(t >= 1)
        {
            Destroy(GetComponent<EnemyShoot>());

          //  Destroy(target);

        }
    }
    //set target position and the landspeed(0.01-0.1) to get the position
    public void SetLandPosition(Vector2 l,float timespeed)
    {
        landPosition = l;
        landSpeed = timespeed;
    }

    public void SetLandPosition(Vector2 l)
    {
        landPosition = l;
    }
}
