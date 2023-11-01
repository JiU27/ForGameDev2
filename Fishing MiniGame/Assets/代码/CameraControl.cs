using Cinemachine;
using UnityEngine;
using System.Collections;
using static GameControl;

public class CameraControl : MonoBehaviour
{
    public CinemachineVirtualCamera camera1;
    public CinemachineVirtualCamera camera2;
    public CinemachineVirtualCamera camera3;
    public CinemachineVirtualCamera camera4;
    public GameObject floatObject;  // ����float����
    public float x;  // ����������ƶ��ٶ�
    public float y;  // ����float yλ���ƶ���
    public float zRotationAmount = 30f; // ����z��ת��
    public float zRotationDuration; // ����z��ת����ʱ��

    public UIControl uiControl;
    public GameControl GameControl;

    private Vector3 camera1OriginalPosition;

    private void Start()
    {
        camera1OriginalPosition = camera1.transform.position;
        camera1.Priority = 1;
        camera2.Priority = 0;
        camera3.Priority = 0;
        camera4.Priority = 0;
    }

    public void ApproachFloatObject()
    {
        camera1.transform.position = Vector3.MoveTowards(camera1.transform.position, floatObject.transform.position, x * Time.deltaTime);
    }

    public void SwitchToCamera2()
    {
        camera1.Priority = 0;
        camera2.Priority = 1;
        camera3.Priority = 0;
        camera4.Priority = 0;
        uiControl.HidePrefect();
        uiControl.ShowCanvas();
        StartCoroutine(FloatAnimation());
    }

    public void SwitchToCamera1()
    {
        camera1.Priority = 1;
        camera2.Priority = 0;
        camera3.Priority = 0;
        camera4.Priority = 0;
        uiControl.HidePrefect();
        uiControl.ShowCanvas();
        camera1.transform.position = camera1OriginalPosition;
    }

    public void SwitchToCamera3()
    {
        camera1.Priority = 0;
        camera2.Priority = 0;
        camera3.Priority = 1;
        camera4.Priority = 0;
        uiControl.ShowPrefect();
        uiControl.HideCanvas();
        StartCoroutine(DelayedSwitchToCamera1(1f));
    }

    public void SwitchToCamera4()
    {
        camera1.Priority = 0;
        camera2.Priority = 0;
        camera3.Priority = 0;
        camera4.Priority = 1;
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            SwitchToCamera1();
        }
    }

    IEnumerator DelayedSwitchToCamera1(float delay)
    {
        yield return new WaitForSeconds(delay); // �ȴ�x��
        SwitchToCamera1();
    }

    public void HandleFishEscapedState()
    {
        camera1.transform.position = camera1OriginalPosition;
        SwitchToCamera1();
    }

    public void HandleCastLineState()
    {
        camera1.transform.position = camera1OriginalPosition;
        SwitchToCamera1();
    }
    IEnumerator FloatAnimation()
    {
        Vector3 initialPosition = floatObject.transform.position;
        Vector3 targetPosition = new Vector3(floatObject.transform.position.x, floatObject.transform.position.y - y, floatObject.transform.position.z);

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / zRotationDuration;
            floatObject.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            //floatObject.transform.eulerAngles = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, zRotationAmount), t);
            yield return null;
        }


        if (GameControl.currentStage == FishingStage.ReelFish)
        {
            SwitchToCamera3(); 
        }
        else
        {
            SwitchToCamera1();
        }
    }
}
