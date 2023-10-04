using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabCreator : MonoBehaviour
{
    public string targetTag = "x"; // 检测的标签
    public GameObject spawnPrefab; // 你要创建的预制体
    public float detectionRadius = 5.0f; // 检测半径
    private GameObject spawnedObject; // 引用创建的对象

    void Start()
    {
        // 确保你的检测器有一个球体触发器并且设置为触发模式
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
        // 如果进入检测区域的对象有正确的标签，并且当前没有创建的对象
        if (other.CompareTag(targetTag) && spawnedObject == null)
        {
            // 创建一个预制体的实例
            spawnedObject = Instantiate(spawnPrefab, transform.position, transform.rotation);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 如果离开检测区域的对象有正确的标签，并且当前有一个创建的对象
        if (other.CompareTag(targetTag) && spawnedObject != null)
        {
            // 销毁该对象
            Destroy(spawnedObject);
            spawnedObject = null;
        }
    }
}
