using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideScript : MonoBehaviour
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TestForRotation();

    }

    void TestForRotation()
    {

        Vector2 vecA = PortalIn.Instance.transform.position;
        Vector2 vecB = Player.MyInstance.transform.position;
        //Vector3 direction = vecB - vecA;                                    ///< 终点减去起点（AB方向与X轴的夹角）
        Vector2 direction = vecA - vecB;                                  ///< （BA方向与X轴的夹角）
        direction = direction.normalized;                          ///< 向量规范化
        float angle = Mathf.Atan2(direction.y, direction.x);              ///< 计算旋转角度
        //float dot = Vector2.Dot(direction, Vector2.up);                  ///< 判断是否Vector3.right在同一方向
        // if (dot < 0) 
        //  angle = 360 - angle;
        //Debug.Log(direction);
        angle = angle * Mathf.Rad2Deg;
        GetComponent<Transform>().rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
        //.Slerp(GetComponent<Transform>().rotation, Quaternion.Euler(0, 0, angle), 0.1f);

        Vector2 position = Player.MyInstance.transform.position;
       // if (angle > 0)
      //  {
            position.x = Player.MyInstance.transform.position.x + 0.2f * Mathf.Cos(angle * Mathf.Deg2Rad);
            position.y = Player.MyInstance.transform.position.y + 0.2f * Mathf.Sin(angle * Mathf.Deg2Rad);
       // }
        //else
        //        {
        //    position.x = Player.MyInstance.transform.position.x - 0.2f * Mathf.Cos(angle);
        //    position.y = Player.MyInstance.transform.position.y - 0.2f * Mathf.Sin(angle);
        //}
        GetComponent<Transform>().position = position;

    //   Rotate(0, 0, angle);
   // Debug.Log(angle);
    }

}
