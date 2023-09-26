using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveForBirdEye : MonoBehaviour
{
    public float moveSpeed = 5f; // GameObject的移动速度

    void Update()
    {
        // 获取WASD/Arrow键的输入
        float horizontal = Input.GetAxis("Horizontal"); // A/D or 左/右箭头
        float vertical = Input.GetAxis("Vertical");     // W/S or 上/下箭头

        // 计算移动的方向
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        // 更新GameObject的位置
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    
}
