using UnityEngine;

public class CenterOfMass : MonoBehaviour
{
    private Rigidbody2D rb;

    // 声明新的质心位置
    public Vector2 newCenterOfMass;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // 如果 Rigidbody2D 存在，则设置新的质心位置
        if (rb != null)
        {
            rb.centerOfMass = newCenterOfMass;
        }
    }

    void OnDrawGizmos()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        if (rb != null)
        {
            // 将新的局部质心位置转换为世界坐标
            Vector3 worldCenterOfMass = rb.transform.TransformPoint(newCenterOfMass);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(worldCenterOfMass, 0.1f);
        }
    }
}
