using UnityEngine;

public class CircularMotion : MonoBehaviour
{
    public Vector3 centerPoint = new Vector3(0, 0, 0); // 圆心
    public float radius = 5.0f;                         // 半径
    public float rotationSpeed = 1.0f;                  // 旋转速度

    private float angle = 0;                            // 初始化角度

    void Update()
    {
        // 计算新的x和z坐标
        float xPos = centerPoint.x + radius * Mathf.Cos(angle);
        float zPos = centerPoint.z + radius * Mathf.Sin(angle);

        // 更新物体的位置
        transform.position = new Vector3(xPos, centerPoint.y, zPos);

        // 增加角度，以便于下一个 Update() 调用时物体移动到新位置
        angle += Time.deltaTime * rotationSpeed;
    }
}
