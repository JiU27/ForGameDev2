using UnityEngine;

public class CenterOfMass : MonoBehaviour
{
    private Rigidbody2D rb;

    // �����µ�����λ��
    public Vector2 newCenterOfMass;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // ��� Rigidbody2D ���ڣ��������µ�����λ��
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
            // ���µľֲ�����λ��ת��Ϊ��������
            Vector3 worldCenterOfMass = rb.transform.TransformPoint(newCenterOfMass);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(worldCenterOfMass, 0.1f);
        }
    }
}
