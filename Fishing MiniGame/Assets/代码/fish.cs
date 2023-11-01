using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class fish : MonoBehaviour
{
    private Vector3 startPoint;
    public Vector3 midPoint = new Vector3(-11, 4, 0.5f);
    public Vector3 endPoint = new Vector3(-10, 0.9f, 0);
    public float travelTime = 1f; // ����������Ϊ��Ҫ��x��
    public float waitTime = 3.0f;   // ����������Ϊ��Ҫ��y��

    public Vector3 initialRotation = new Vector3(0, -40, 0); // ����ӵĹ���������������ĳ�ʼ��ת�Ƕ�

    private void Start()
    {
        // ������ĳ�ʼ��ת
        transform.rotation = Quaternion.Euler(initialRotation);

        // ��ȡ��ĳ�ʼλ��
        startPoint = transform.position;

        // ��ʼ�ƶ���
        StartCoroutine(FishMovement());
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(gameObject);
        }
    }

    System.Collections.IEnumerator FishMovement()
    {
        // �ӳ�ʼλ���ƶ����м�λ��
        for (float t = 0; t < 1; t += Time.deltaTime / (travelTime / 2))
        {
            transform.position = Vector3.Lerp(startPoint, midPoint, t);
            yield return null;
        }

        // ���м�λ���ƶ�������λ��
        for (float t = 0; t < 1; t += Time.deltaTime / (travelTime / 2))
        {
            transform.position = Vector3.Lerp(midPoint, endPoint, t);
            yield return null;
        }

        // �ȴ�y��
        //yield return new WaitForSeconds(waitTime);

        // �����Լ�
        //Destroy(gameObject);
    }
}
