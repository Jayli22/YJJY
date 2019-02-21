using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public GameObject portalPrefab;
    private bool count;
    // Start is called before the first frame update
    void Start()
    {
        count = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if(SceneManager.sceneLoaded)

        if(GameObject.FindGameObjectWithTag("Enemy") == null && count == false && GameObject.FindGameObjectWithTag("Boss") == null)
        {
            //Debug.Log("no more enemy");
            count = true;
            GameObject portal =  Instantiate(portalPrefab, EnemyGenerator.MyInstance.ChoosePosition(), transform.rotation);
            portal.AddComponent<PortalIn>();
        }
    }
}
