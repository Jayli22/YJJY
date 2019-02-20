using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalOut : MonoBehaviour
{
    private Timer showTime;
    // Start is called before the first frame update
    void Awake()
    {
        showTime = gameObject.AddComponent<Timer>();
        showTime.Duration = 5f;
        showTime.Run();
    }

    // Update is called once per frame
    void Update()
    {
        if(showTime.Finished)
        {
            Destroy(gameObject);
        }
    }
}
