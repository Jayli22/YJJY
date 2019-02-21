using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public GameObject portalPrefab;
    private static GameManagement instance;
    public bool count;
    public Texture2D texture;
    public GameObject guideArrowPrefab;
    private GameObject guideArrow;

    public static GameManagement MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManagement>();
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        count = false;
        Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
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
            if(guideArrow==null)
            {
                guideArrow = Instantiate(guideArrowPrefab);
            }
        }
     
    }
}
