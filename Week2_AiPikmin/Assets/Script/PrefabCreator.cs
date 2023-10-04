using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabCreator : MonoBehaviour
{
    public string targetTag = "x"; // ���ı�ǩ
    public GameObject spawnPrefab; // ��Ҫ������Ԥ����
    public float detectionRadius = 5.0f; // ���뾶
    private GameObject spawnedObject; // ���ô����Ķ���

    void Start()
    {
        // ȷ����ļ������һ�����崥������������Ϊ����ģʽ
        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        if (sphereCollider == null)
        {
            sphereCollider = gameObject.AddComponent<SphereCollider>();
        }
        sphereCollider.isTrigger = true;
        sphereCollider.radius = detectionRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        // �������������Ķ�������ȷ�ı�ǩ�����ҵ�ǰû�д����Ķ���
        if (other.CompareTag(targetTag) && spawnedObject == null)
        {
            // ����һ��Ԥ�����ʵ��
            spawnedObject = Instantiate(spawnPrefab, transform.position, transform.rotation);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ����뿪�������Ķ�������ȷ�ı�ǩ�����ҵ�ǰ��һ�������Ķ���
        if (other.CompareTag(targetTag) && spawnedObject != null)
        {
            // ���ٸö���
            Destroy(spawnedObject);
            spawnedObject = null;
        }
    }
}
