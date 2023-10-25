using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
    public CinemachineVirtualCamera camera1;
    public CinemachineVirtualCamera camera2;
    public Slider rangeSlider;

    public void SwitchToCamera2(float focusValue)
    {
        camera1.Priority = 0;
        camera2.Priority = 1;
        rangeSlider.value = focusValue;
    }

    public void SwitchToCamera1()
    {
        camera1.Priority = 1;
        camera2.Priority = 0;
    }
}
