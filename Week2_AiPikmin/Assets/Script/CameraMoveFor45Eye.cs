using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveFor45Eye : MonoBehaviour
{
    public float radius = 5f; // 检测范围的半径
    public Camera mainCamera; // 场景中的主摄像机，用于计算鼠标的世界坐标
    public float moveSpeed = 5f; // GameObject的移动速度

    void Update()
    {
        // 从摄像机发射一条射线到鼠标的位置
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            // 计算鼠标的位置和GameObject的位置之间的距离
            float distance = Vector3.Distance(transform.position, hit.point);

            // 如果距离超出了给定的范围
            if (distance > radius)
            {
                // 获取GameObject当前的y轴位置
                float currentX = transform.position.x;
                float currentY = transform.position.y;

                // 设置新的位置，但保持y轴不变
                Vector3 targetPosition = hit.point;
                targetPosition.x = currentX;
                targetPosition.y = currentY;

                // 使用MoveTowards平滑地移动GameObject
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
        }
    }

    // 在Unity编辑器的Scene视图中绘制辅助线和圆环
    void OnDrawGizmos()
    {
        // 绘制一个圆环代表检测范围
        Gizmos.color = Color.yellow; // 设置颜色为黄色
        Gizmos.DrawWireSphere(transform.position, radius);

    }
}
