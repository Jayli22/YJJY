using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackMenuClick : MonoBehaviour
{
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Destroy(GameObject.FindGameObjectWithTag("GameController"));
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Destroy(GameObject.FindGameObjectWithTag("MainCamera"));

        ChangeLevel.NextLevel("TitleScene");
        GameObject.FindGameObjectWithTag("Finish").SetActive(false);
    }
}
