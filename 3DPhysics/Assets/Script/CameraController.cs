using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera cameraA; // 主摄像机
    public CinemachineVirtualCamera cameraB; // 检测到Ring后的摄像机
    public float waitTime = 5f; // 等待的秒数，可以在Inspector中设置

    private void Start()
    {
        // 游戏开始时，确保cameraA是活跃的，并且cameraB是不活跃的。
        SwitchToCameraA();
    }

    public void DetectedRing()
    {
        // 切换到cameraB，并在指定的时间后返回到cameraA。
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
        cameraA.Priority = 20; // 设置一个较高的优先级
        cameraB.Priority = 10; // 设置一个较低的优先级
    }

    private void SwitchToCameraB()
    {
        cameraA.Priority = 10;
        cameraB.Priority = 20;
    }
}
