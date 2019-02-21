using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalIn : MonoBehaviour
{
    private bool count = false;
    private static PortalIn instance;

    public static PortalIn Instance {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PortalIn>();
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !count)
        {
            int i = Utils.GetInt("Level");
            i++;
            Utils.SetInt("Level", i);
            ChangeLevel.NextLevel(i.ToString());
            count = true;
            //gameObject.AddComponent<ChangeScene>();
        }
    }
}
