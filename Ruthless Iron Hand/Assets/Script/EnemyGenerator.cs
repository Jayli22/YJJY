using CreativeSpore.SuperTilemapEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public int enmeyNumber;
    public GameObject[] enemyPrefabList;
    public TilemapGroup tilemapGroup;
    public GameObject[] summonEffect;
    private static EnemyGenerator instance;
    public static EnemyGenerator MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EnemyGenerator>();
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        if(!tilemapGroup)
        {
            tilemapGroup = FindObjectOfType<TilemapGroup>();
        }

        //int x = Random.Range(tilemapGroup[0].MinGridX, tilemapGroup[0].MaxGridX);
        //int y = Random.Range(tilemapGroup[0].MinGridY, tilemapGroup[0].MaxGridY);

        //if (tilemapGroup[0].GetTile(x, y)== null)
        //{
        //    x = Random.Range(tilemapGroup[0].MinGridX, tilemapGroup[0].MaxGridX);
        //    y = Random.Range(tilemapGroup[0].MinGridY, tilemapGroup[0].MaxGridY);
        //}

        //GameObject enemy1 = Instantiate(summonEffect[0], ChoosePosition(), transform.rotation);
        //enemy1.GetComponent<SummonEffect>().SetObjectType(enemyPrefabList[0]);

    }

    

    // Update is called once per frame
    void Update()
    {
        if (!tilemapGroup)
        {
            tilemapGroup = FindObjectOfType<TilemapGroup>();
        }
    }


    public Vector2 ChoosePosition()
    {
        Vector2 position;
        int x = Random.Range(tilemapGroup.Tilemaps[0].MinGridX, tilemapGroup.Tilemaps[0].MaxGridX);
        //Debug.Log(tilemapGroup.Tilemaps[0].MinGridX + "," + tilemapGroup.Tilemaps[0].MaxGridX);
       // Debug.Log(tilemapGroup.Tilemaps[0].MinGridY + "," + tilemapGroup.Tilemaps[0].MaxGridY);
        //Debug.Log(tilemapGroup.Tilemaps[0].GetTile(0, 0).uv);
        int y = Random.Range(tilemapGroup.Tilemaps[0].MinGridY, tilemapGroup.Tilemaps[0].MaxGridY);
        while (tilemapGroup.Tilemaps[0].GetTile(x, y) == null)
        {
            x = Random.Range(tilemapGroup.Tilemaps[0].MinGridX, tilemapGroup.Tilemaps[0].MaxGridX);
            y = Random.Range(tilemapGroup.Tilemaps[0].MinGridY, tilemapGroup.Tilemaps[0].MaxGridY);
          //  Debug.Log(tilemapGroup.Tilemaps[0].GetTile(-7, 1));

        }

        position = TilemapUtils.GetTileCenterPosition(tilemapGroup["Ground"], x, y);
        
        //Debug.Log(TilemapUtils.GetTileCenterPosition(tilemapGroup["Ground"], x, y) +"x:" +x + "y:" +y);
        return position;
    }

    public void SetTileMap()
    {
        if (!tilemapGroup)
        {
            tilemapGroup = FindObjectOfType<TilemapGroup>();
        }
    }
}
