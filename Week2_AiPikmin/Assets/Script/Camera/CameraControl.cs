using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public List<CinemachineVirtualCamera> cameraList;
    public int activeCameraIndex = 0;
    public Camera mainCamera; // �������������Raycasting


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
                // ������Ķ����Ƿ���"TargetObject"��ǩ
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
            // ������������������ȼ�Ϊ0
            foreach (var vcam in cameraList)
            {
                vcam.Priority = 0;
            }

            // ���ݸ�����������������������ȼ�Ϊ100
            cameraList[cameraIndex].Priority = 100;
        }
    }
}
