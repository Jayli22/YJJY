using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel
{
    public static void NextLevel(string level)
    {
        // ChangeScene.loadNextScene(level);
        //  SceneManager.LoadScene(level);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(level);
        if(asyncOperation.isDone)
        {
            Debug.Log("场景加载完成");
        }
    }

}
