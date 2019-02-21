using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    private Vector2 landPosition;
    private Vector2 startPosition;
    private Timer changeTimer;
    private float t;
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
        if(changeTimer.Finished)
        {
            changeTimer.Run();
            transform.position =  Vector2.Lerp(startPosition, landPosition, t);



            t += 0.01f;
            //Debug.Log(t);
        }
        if(t >= 1)
        {
            Destroy(GetComponent<EnemyShoot>());

          //  Destroy(target);

        }
    }

    public void SetLandPosition(Vector2 l)
    {
        landPosition = l;
    }
}
