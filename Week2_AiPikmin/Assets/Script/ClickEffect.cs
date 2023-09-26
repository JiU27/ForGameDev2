using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEffect : MonoBehaviour
{
    public Camera mainCamera; // ͨ���ǳ����е��������
    public GameObject prefabToSpawn; // �������ɵ�Prefab

    void Update()
    {
        // ������������
        if (Input.GetMouseButtonDown(0))
        {
            // �����������һ�����ߵ�����λ��
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // ���������ײ��һ��GameObject
            if (Physics.Raycast(ray, out hit))
            {
                // �ڻ���λ������Prefab
                Instantiate(prefabToSpawn, hit.point, Quaternion.identity);
            }
        }
    }
}
