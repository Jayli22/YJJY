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
        Instantiate(portal, dir, transform.rotation);
        Player.MyInstance.transform.position = dir;

    }

    // Update is called once per frame
    void Update()
    {
       
        //if (MainInterfaceGUIScript.async.isDone == false)
        //{
        //    //________没 有加载完要做的事情如Logo__________________________
        //}
        //else
        //{
        //    //全部加载完成后显示的东东。。
        //}
    }
}
