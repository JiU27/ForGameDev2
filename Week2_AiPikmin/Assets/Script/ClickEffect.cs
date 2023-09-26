using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEffect : MonoBehaviour
{
    public Camera mainCamera; // 通常是场景中的主摄像机
    public GameObject prefabToSpawn; // 你想生成的Prefab

    void Update()
    {
        // 检测鼠标左键点击
        if (Input.GetMouseButtonDown(0))
        {
            // 从摄像机发射一条射线到鼠标的位置
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 如果射线碰撞到一个GameObject
            if (Physics.Raycast(ray, out hit))
            {
                // 在击中位置生成Prefab
                Instantiate(prefabToSpawn, hit.point, Quaternion.identity);
            }
        }
    }
}
