using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;

    void Start()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player == null)
        {
            Debug.LogError("【CameraController】场景中未找到PlayerController组件！相机将无法跟随", this);
            enabled = false;// 禁用脚本
            return;
        }
        target = player.transform;//寻找玩家控制器中的玩家坐标
    }

    //别的场景更新被调用时，该更新才被调用
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y,transform.position.z);
    }
}
