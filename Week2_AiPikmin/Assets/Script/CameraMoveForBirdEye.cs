using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveForBirdEye : MonoBehaviour
{
    public float moveSpeed = 5f; // GameObject���ƶ��ٶ�

    void Update()
    {
        // ��ȡWASD/Arrow��������
        float horizontal = Input.GetAxis("Horizontal"); // A/D or ��/�Ҽ�ͷ
        float vertical = Input.GetAxis("Vertical");     // W/S or ��/�¼�ͷ

        // �����ƶ��ķ���
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        // ����GameObject��λ��
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    
}
