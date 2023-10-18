using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera cameraA; // �������
    public CinemachineVirtualCamera cameraB; // ��⵽Ring��������
    public float waitTime = 5f; // �ȴ���������������Inspector������

    private void Start()
    {
        // ��Ϸ��ʼʱ��ȷ��cameraA�ǻ�Ծ�ģ�����cameraB�ǲ���Ծ�ġ�
        SwitchToCameraA();
    }

    public void DetectedRing()
    {
        // �л���cameraB������ָ����ʱ��󷵻ص�cameraA��
        SwitchToCameraB();
        StartCoroutine(SwitchBackAfterDelay());
    }

    private IEnumerator SwitchBackAfterDelay()
    {
        yield return new WaitForSeconds(waitTime);
        SwitchToCameraA();
    }

    private void SwitchToCameraA()
    {
        cameraA.Priority = 20; // ����һ���ϸߵ����ȼ�
        cameraB.Priority = 10; // ����һ���ϵ͵����ȼ�
    }

    private void SwitchToCameraB()
    {
        cameraA.Priority = 10;
        cameraB.Priority = 20;
    }
}
