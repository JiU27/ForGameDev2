using UnityEngine;

public class CircularMotion : MonoBehaviour
{
    public Vector3 centerPoint = new Vector3(0, 0, 0); // Բ��
    public float radius = 5.0f;                         // �뾶
    public float rotationSpeed = 1.0f;                  // ��ת�ٶ�

    private float angle = 0;                            // ��ʼ���Ƕ�

    void Update()
    {
        // �����µ�x��z����
        float xPos = centerPoint.x + radius * Mathf.Cos(angle);
        float zPos = centerPoint.z + radius * Mathf.Sin(angle);

        // ���������λ��
        transform.position = new Vector3(xPos, centerPoint.y, zPos);

        // ���ӽǶȣ��Ա�����һ�� Update() ����ʱ�����ƶ�����λ��
        angle += Time.deltaTime * rotationSpeed;
    }
}
