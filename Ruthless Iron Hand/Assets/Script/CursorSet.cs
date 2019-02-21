using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSet : MonoBehaviour
{
    public Texture2D texture;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Awake()
    {
        Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
