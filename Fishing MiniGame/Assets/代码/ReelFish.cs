using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static GameControl;

public class ReelFish : MonoBehaviour
{
    public GameControl gameControl;
    public GameObject floatObject, TurnTable;
    public Slider reelFishTimerSlider;  
    public Slider fishEnergySlider;
    public float fishEnergyRecoveryRate = 1f;
    public float fishEnergyDepletionRate = 2f;
    public float reelSpeed = 4.0f;

    public float x = 2f;  // 玩家按下A或D时Slider的增加速率
    public float y = 1f;  // 玩家松开A或D时Slider的减少速率

    private bool hasDelayedReelFish = false;
    private Vector3 floatOriginalPosition = new Vector3(-19, -0.13f, -0.5f);
    private bool isActive = false;

    public void Start()
    {
        fishEnergySlider.value = fishEnergySlider.maxValue;
        reelFishTimerSlider.value = reelFishTimerSlider.minValue;
    }

    public void BoolSet()
    {
        hasDelayedReelFish = false;
    }

    public void SetTime()
    {
        reelFishTimerSlider.value = 0f;  // 设置Slider的初始值
    }

    public void HandleReelFish()
    {
        if (!hasDelayedReelFish)
        {
            StartCoroutine(StartReelFishDelay());
            return;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            TurnTable.transform.Rotate(Input.GetKey(KeyCode.A) ? Vector3.back * reelSpeed : Vector3.forward * reelSpeed);
            fishEnergySlider.value -= fishEnergyDepletionRate * Time.deltaTime;
            reelFishTimerSlider.value += x * Time.deltaTime;  
        }
        else
        {
            fishEnergySlider.value += fishEnergyRecoveryRate * Time.deltaTime;
            reelFishTimerSlider.value -= y * Time.deltaTime;  
        }

        
        if (reelFishTimerSlider.value >= reelFishTimerSlider.maxValue)
        {
            gameControl.SwitchStage(FishingStage.FishEscaped);
        }
        else if (fishEnergySlider.value <= 0)
        {
            gameControl.SwitchStage(FishingStage.FishCaught);
        }

        float lerpFactor = 1f - (fishEnergySlider.value / fishEnergySlider.maxValue);
        floatObject.transform.position = Vector3.Lerp(floatOriginalPosition, new Vector3(-14, floatObject.transform.position.y, 0.2f), lerpFactor);
    }

    private IEnumerator StartReelFishDelay()
    {
        yield return new WaitForSeconds(3f);
        hasDelayedReelFish = true;
    }
}
