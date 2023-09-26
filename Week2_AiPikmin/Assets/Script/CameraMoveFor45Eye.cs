using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveFor45Eye : MonoBehaviour
{
    public float radius = 5f; // ��ⷶΧ�İ뾶
    public Camera mainCamera; // �����е�������������ڼ���������������
    public float moveSpeed = 5f; // GameObject���ƶ��ٶ�

    void Update()
    {
        // �����������һ�����ߵ�����λ��
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            // ��������λ�ú�GameObject��λ��֮��ľ���
            float distance = Vector3.Distance(transform.position, hit.point);

            // ������볬���˸����ķ�Χ
            if (distance > radius)
            {
                // ��ȡGameObject��ǰ��y��λ��
                float currentX = transform.position.x;
                float currentY = transform.position.y;

                // �����µ�λ�ã�������y�᲻��
                Vector3 targetPosition = hit.point;
                targetPosition.x = currentX;
                targetPosition.y = currentY;

                // ʹ��MoveTowardsƽ�����ƶ�GameObject
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
        }
    }

    // ��Unity�༭����Scene��ͼ�л��Ƹ����ߺ�Բ��
    void OnDrawGizmos()
    {
        // ����һ��Բ�������ⷶΧ
        Gizmos.color = Color.yellow; // ������ɫΪ��ɫ
        Gizmos.DrawWireSphere(transform.position, radius);

    }
}
