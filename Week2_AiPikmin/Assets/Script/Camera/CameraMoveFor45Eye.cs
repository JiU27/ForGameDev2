using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveFor45Eye : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        // 获取反向的轴输入
        float horizontal = -Input.GetAxis("Vertical");
        float vertical = Input.GetAxis("Horizontal");

        // 计算移动的方向
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        // 更新GameObject的位置
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
