using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerIndicatorController : MonoBehaviour
{
    public GameController gameController;
    public Slider powerSlider;
    public TextMeshProUGUI powerText;
    public float powerChangeSpeed = 10f;
    public float maxPower = 100f;
    public float minPower = 0f;

    private bool isLocked = false;
    private float currentPower = 0f;
    private int powerDirection = 1;
    private float lockedPower;

    //public bool IsLocked { get { return isLocked; } }


    void Start()
    {
        currentPower = (maxPower - minPower) / 2;

        if (powerSlider != null)
        {
            powerSlider.minValue = minPower;
            powerSlider.maxValue = maxPower;
            powerSlider.value = currentPower;
        }
    }

    void Update()
    {
        if (gameController.currentState == GameController.GameState.SettingPower)
        {
            if (isLocked)
            {
                // �����һ��״̬��������������ô�ڻص����״̬ʱ��������
                ResetPower();
            }

            currentPower += powerChangeSpeed * Time.deltaTime * powerDirection;
            currentPower = Mathf.Clamp(currentPower,minPower,maxPower);

            if (currentPower >= maxPower || currentPower <= minPower)
            {
                powerDirection *= -1;
            }

            if (powerSlider != null)
            {
                powerSlider.value = currentPower;
            }

            if (powerText != null)
            {
                powerText.text = "Power: " + currentPower.ToString("F1");
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //LockPower();
            }
        }
    }

    public void LockPower()
    {
        if (!isLocked) // ����ж�ȷ��������ֻ����δ������ʱ�ű�����
        {
            isLocked = true;
            lockedPower = currentPower;

            if (powerText != null)
            {
                powerText.text = "Locked Power: " + lockedPower.ToString("F1");
            }
        }

        //gameController.LaunchRing(); // ������һ������ʼ���
    }


    public void ResetPower()
    {
        isLocked = false;
        currentPower = 0f;

        if (powerSlider != null)
        {
            powerSlider.value = currentPower;
        }
        if (powerText != null)
        {
            powerText.text = "Power: " + currentPower.ToString("F1");
        }
    }

    public float GetLockedPower()
    {
        return lockedPower;
        //Debug.Log("1");
    }
}
