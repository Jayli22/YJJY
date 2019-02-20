using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickController : MonoBehaviour
{
    public void Click(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
