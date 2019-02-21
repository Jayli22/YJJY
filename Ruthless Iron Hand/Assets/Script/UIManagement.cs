using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagement : MonoBehaviour
{
    private int maxhp_number;
    private int curhp_number;
    private Image[] maxhp_array;
    private Image[] curhp_array;
    public Image hpheart;
    public Image empty_hpheart;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //maxhp_number = Player.MyInstance.maxhp;
        //curhp_number = Player.MyInstance.currenthp;
        //GameObject hpbar = GameObject.Find("HPbar");
        //maxhp_array = new Image[maxhp_number];
        //for (int i = 0; i < curhp_number; i++)
        //{
        //    maxhp_array[i] = Instantiate(hpheart);
        //    maxhp_array[i].transform.SetParent(hpbar.transform, false);
        //    //maxhp_array[i].transform.position += new Vector3(0.0f, uistartPosInVertical * i, 0.0f);

        //}
        
     
    }

    // Update is called once per frame
    //void Update()
    //{
    //    curhp_number = Player.MyInstance.currenthp;

    //    for (int j = curhp_number; j < maxhp_number; j++)
    //    {
    //        //maxhp_array[j] = Instantiate(empty_hpheart);
    //        // maxhp_array[j].transform.SetParent(hpbar.transform, false);
    //        //maxhp_array[j].GetComponent<Image>().overrideSprite = empty_hpheart.sprite;
            
    //    }
    //}
}
