using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveFor45Eye : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        // ��ȡ�����������
        float horizontal = -Input.GetAxis("Vertical");
        float vertical = Input.GetAxis("Horizontal");

        // �����ƶ��ķ���
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        // ����GameObject��λ��
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
