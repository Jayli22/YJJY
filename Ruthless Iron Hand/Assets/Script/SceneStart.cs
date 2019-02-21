using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStart : MonoBehaviour
{
    public GameObject portal;
    // Start is called before the first frame update
    void Start()
    {
        EnemyGenerator.MyInstance.SetTileMap();
        Vector2 dir = EnemyGenerator.MyInstance.ChoosePosition();
        GameObject p = Instantiate(portal, dir, transform.rotation);
        p.AddComponent<PortalOut>();
        Player.MyInstance.transform.position = dir;
        GameManagement.MyInstance.count = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
}
