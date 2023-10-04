using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveForBirdEye : MonoBehaviour
{
    public float moveSpeed = 5f; 

    void Update()
    {
        
        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical");

        // �����ƶ��ķ���
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        // ����GameObject��λ��
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    
}
