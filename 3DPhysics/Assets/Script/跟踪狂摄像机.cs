using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class 跟踪狂摄像机 : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCam;
    private GameObject currentRing;

    private CameraController cameraController;

    void Start()
    {
        virtualCam = GetComponent<CinemachineVirtualCamera>();
        cameraController = FindObjectOfType<CameraController>(); // 获取CameraController组件
    }

    void Update()
    {
        // 如果当前正在跟踪的Ring对象为null或者不再存在，则查找新的Ring对象
        if (currentRing == null)
        {
            GameObject foundRing = GameObject.FindGameObjectWithTag("Ring");
            if (foundRing != null)
            {
                currentRing = foundRing;
                virtualCam.LookAt = currentRing.transform;

                // 当检测到Ring时，调用CameraController中的方法
                cameraController.DetectedRing();
            }
        }
    }
}
