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

    // Start is called before the first frame update
    void Start()
    {
        if(!tilemapGroup)
        {
            tilemapGroup = FindObjectOfType<TilemapGroup>();
        }

        int x = Random.Range(tilemapGroup[0].MinGridX, tilemapGroup[0].MaxGridX);
        int y = Random.Range(tilemapGroup[0].MinGridY, tilemapGroup[0].MaxGridY);

        if (tilemapGroup[0].GetTile(x, y).uv == null)
        {
            x = Random.Range(tilemapGroup[0].MinGridX, tilemapGroup[0].MaxGridX);
            y = Random.Range(tilemapGroup[0].MinGridY, tilemapGroup[0].MaxGridY);
        }

        GameObject enemy1 = Instantiate(summonEffect[0], ChoosePosition(), transform.rotation);
        enemy1.GetComponent<SummonEffect>().SetObjectType(enemyPrefabList[0]);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }


    public Vector2 ChoosePosition()
    {
        Vector2 position;
        int x = Random.Range(tilemapGroup[0].MinGridX, tilemapGroup[0].MaxGridX);
        int y = Random.Range(tilemapGroup[0].MinGridY, tilemapGroup[0].MaxGridY);
        while (tilemapGroup[0].GetTile(x, y).uv == null)
        {
            x = Random.Range(tilemapGroup[0].MinGridX, tilemapGroup[0].MaxGridX);
            y = Random.Range(tilemapGroup[0].MinGridY, tilemapGroup[0].MaxGridY);
        }
       
            position = tilemapGroup[0].GetTile(x, y).uv.position;
        
        return position;
    }
}
