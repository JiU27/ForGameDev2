using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public List<CinemachineVirtualCamera> cameraList;
    public int activeCameraIndex = 0;
    public Camera mainCamera; // 主摄像机，用于Raycasting


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // 检查点击的对象是否有"TargetObject"标签
                if (hit.collider.CompareTag("FireBall"))
                {
                    
                    cameraList[2].Priority = 100;
                }
                else if (hit.collider.CompareTag("IceBall"))
                {
                    
                    cameraList[3].Priority = 100;
                }
                else if (hit.collider.CompareTag("ThunderBall"))
                {
                    
                    cameraList[4].Priority = 100;
                }
            }
        }
    }

    public void SwitchToTargetCamera(int cameraIndex)
    {
        if (cameraList.Count > 0 && cameraIndex >= 0 && cameraIndex < cameraList.Count)
        {
            // 设置所有摄像机的优先级为0
            foreach (var vcam in cameraList)
            {
                vcam.Priority = 0;
            }

            // 根据给定的索引设置摄像机的优先级为100
            cameraList[cameraIndex].Priority = 100;
        }
    }
}
