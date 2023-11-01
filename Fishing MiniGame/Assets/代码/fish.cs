using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class fish : MonoBehaviour
{
    private Vector3 startPoint;
    public Vector3 midPoint = new Vector3(-11, 4, 0.5f);
    public Vector3 endPoint = new Vector3(-10, 0.9f, 0);
    public float travelTime = 1f; // 您可以设置为需要的x秒
    public float waitTime = 3.0f;   // 您可以设置为需要的y秒

    public Vector3 initialRotation = new Vector3(0, -40, 0); // 新添加的公共变量，代表鱼的初始旋转角度

    private void Start()
    {
        // 设置鱼的初始旋转
        transform.rotation = Quaternion.Euler(initialRotation);

        // 获取鱼的初始位置
        startPoint = transform.position;

        // 开始移动鱼
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
        // 从初始位置移动到中间位置
        for (float t = 0; t < 1; t += Time.deltaTime / (travelTime / 2))
        {
            transform.position = Vector3.Lerp(startPoint, midPoint, t);
            yield return null;
        }

        // 从中间位置移动到结束位置
        for (float t = 0; t < 1; t += Time.deltaTime / (travelTime / 2))
        {
            transform.position = Vector3.Lerp(midPoint, endPoint, t);
            yield return null;
        }

        // 等待y秒
        //yield return new WaitForSeconds(waitTime);

        // 销毁自己
        //Destroy(gameObject);
    }
}
