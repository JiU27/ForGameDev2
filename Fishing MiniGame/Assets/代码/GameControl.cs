using Cinemachine;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    // Enums
    public enum FishingStage
    {
        CastLine,
        WaitForFish,
        TryingToCaught,
        ReelFish,
        FishCaught,
        FishEscaped
    }

    public UIControl uiControl;

    // Public fields
    public FishingStage currentStage;
    public GameObject floatObject, rob, fishPrefab, TurnTable;
    public TextMeshProUGUI fishCountText;
    public TextMeshProUGUI reelFishTimerText;
    public Slider fishEnergySlider;
    public float judgementTime = 5f;
    public float startTime = 4f;
    public float endTime = 2f;

    public CinemachineVirtualCamera virtualCamera;
    private Vector3 originalCameraPosition;

    public float fishEnergyRecoveryRate = 1f;
    public float fishEnergyDepletionRate = 2f;
    public float reelSpeed = 4.0f;
    public float reelFishTimeLimit = 30.0f;
    public float reelFishTimer = 10f;

    // Private fields
    private int fishCount;
    private float floatYRange = 0.05f;
    private float floatSpeed = 0.1f;
    private float tryingToCaughtStartTime;
    private Vector3 floatOriginalPosition = new Vector3(-19, -0.13f, -0.5f);

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        fishCount = Random.Range(1, 3);
        fishCountText.text = "Fish count: " + fishCount;
        currentStage = FishingStage.CastLine;
        originalCameraPosition = virtualCamera.transform.position;
        reelFishTimer = 10f;
        fishEnergySlider.value = fishEnergySlider.maxValue; // 假设滑动条的最大值代表鱼的最大精力
        //floatOriginalPosition = floatObject.transform.position;
    }

    private void Update()
    {
        switch (currentStage)
        {
            case FishingStage.CastLine:
                uiControl.CastLine();
                if (fishCount <= 0)
                {
                    // 游戏结束
                    fishCountText.text = "All fish caught!";

                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        // 重新加载索引为0的场景
                        SceneManager.LoadScene(0);
                    }
                }
                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        StartCoroutine(FloatAnimation());
                        StartCoroutine(RobCastAnimation());
                    }
                }
                
                break;
            case FishingStage.WaitForFish:
                HandleFloatMovement();
                break;
            case FishingStage.TryingToCaught:
                uiControl.TryingToCaught(judgementTime, startTime, endTime);
                HandleFishTryingToBeCaught();
                break;
            case FishingStage.ReelFish:
                
                uiControl.ReelFish();
                HandleReelFish();
                break;
        }

        if (currentStage == FishingStage.TryingToCaught)
        {
            virtualCamera.transform.position = Vector3.MoveTowards(virtualCamera.transform.position, floatObject.transform.position, 0.2f * Time.deltaTime);
        }
    }

    private void SwitchStage(FishingStage nextStage)
    {
        StopCoroutine(WaitForFishToBite(0));

        if (nextStage == FishingStage.TryingToCaught)
        {
            tryingToCaughtStartTime = Time.time;
            judgementTime = 3f;  // 重设倒计时时间
        }
        else if (nextStage == FishingStage.WaitForFish)
        {
            float randomWaitTime = Random.Range(5.0f, 10.0f);  // 您可以根据需要更改此范围
            StartCoroutine(WaitForFishToBite(randomWaitTime));
        }
        else if(nextStage == FishingStage.ReelFish)
        {
            reelFishTimer = 10f;
            fishEnergySlider.value = fishEnergySlider.maxValue;
        }

        currentStage = nextStage;
        switch (nextStage)
        {
            case FishingStage.FishCaught:
                Instantiate(fishPrefab, floatObject.transform.position, Quaternion.identity);
                StartCoroutine(MoveFloatToStartPosition());
                fishCount--;
                fishCountText.text = "Fish count: " + fishCount;
                break;
            case FishingStage.FishEscaped:
                StartCoroutine(MoveFloatToStartPosition());
                break;
        }
    }

    System.Collections.IEnumerator FloatAnimation()
    {
        float animationDuration = 2.0f; // 您可以根据需要修改这个值

        // 定义开始，中间和结束的位置
        Vector3 startPosition = floatObject.transform.position;
        Vector3 midPosition1 = new Vector3(-12, 4, 0.4f);
        Vector3 midPosition2 = new Vector3(-5, 2, 0.8f);
        Vector3 midPosition3 = midPosition1;
        Vector3 endPosition = new Vector3(-19, 0f, -0.5f);

        // 执行动画
        for (float t = 0; t < 1; t += Time.deltaTime / (animationDuration / 4))
        {
            floatObject.transform.position = Vector3.Lerp(startPosition, midPosition1, t);
            yield return null;
        }

        for (float t = 0; t < 1; t += Time.deltaTime / (animationDuration / 4))
        {
            floatObject.transform.position = Vector3.Lerp(midPosition1, midPosition2, t);
            yield return null;
        }

        for (float t = 0; t < 1; t += Time.deltaTime / (animationDuration / 4))
        {
            floatObject.transform.position = Vector3.Lerp(midPosition2, midPosition3, t);
            yield return null;
        }

        for (float t = 0; t < 1; t += Time.deltaTime / (animationDuration / 4))
        {
            floatObject.transform.position = Vector3.Lerp(midPosition3, endPosition, t);
            yield return null;
        }
    }

    System.Collections.IEnumerator RobCastAnimation()
    {
        // 旋转动画的持续时间
        float animationDuration = 2.0f; // 您可以根据需要修改这个值

        // 开始和结束的旋转角度
        Quaternion startRotation = rob.transform.rotation;
        Quaternion midRotation = Quaternion.Euler(0, 0, 10);
        Quaternion endRotation = Quaternion.Euler(0, 0, 50);

        // 第一个阶段的旋转
        for (float t = 0; t < 1; t += Time.deltaTime / (animationDuration / 2))
        {
            rob.transform.rotation = Quaternion.Lerp(startRotation, midRotation, t);
            yield return null;
        }
        rob.transform.rotation = midRotation;

        // 第二个阶段的旋转
        for (float t = 0; t < 1; t += Time.deltaTime / (animationDuration / 2))
        {
            rob.transform.rotation = Quaternion.Lerp(midRotation, endRotation, t);
            yield return null;
        }
        rob.transform.rotation = endRotation;

        // 进入下一个状态
        SwitchStage(FishingStage.WaitForFish);
    }

    private void HandleFloatMovement()
    {
        floatObject.transform.position = new Vector3(
            floatObject.transform.position.x,
            Mathf.PingPong(Time.time * floatSpeed, floatYRange * 2) - floatYRange,
            floatObject.transform.position.z
        );
    }
    private IEnumerator WaitForFishToBite(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        floatObject.transform.position = new Vector3(
            floatObject.transform.position.x,
            -0.13f,
            floatObject.transform.position.z
        );
        SwitchStage(FishingStage.TryingToCaught);
    }

    private void HandleFishTryingToBeCaught()
    {
        judgementTime -= Time.deltaTime;
        Debug.Log($"Remaining Judgement Time: {judgementTime}");

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Input Detected (Mouse or Space)");
            ResetCameraPosition();

            if (judgementTime > startTime && judgementTime < endTime)
            {
                Debug.Log("Switching to ReelFish Stage");
                SwitchStage(FishingStage.ReelFish);
            }
            else
            {
                Debug.Log("Switching to FishEscaped Stage (due to incorrect timing)");
                SwitchStage(FishingStage.FishEscaped);
            }
        }
        else if (judgementTime <= 0)
        {
            Debug.Log("Switching to FishEscaped Stage (due to exceeding judgement time)");
            ResetCameraPosition();
            SwitchStage(FishingStage.FishEscaped);
        }
    }


    private void HandleReelFish()
    {
        if (Input.GetKey(KeyCode.A))
        {
            TurnTable.transform.Rotate(Vector3.back * reelSpeed);
            fishEnergySlider.value -= fishEnergyDepletionRate;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            TurnTable.transform.Rotate(Vector3.forward * reelSpeed);
            fishEnergySlider.value -= fishEnergyDepletionRate;
        }
        else
        {
            fishEnergySlider.value += fishEnergyRecoveryRate;
        }

        reelFishTimer -= Time.deltaTime;

        if (fishEnergySlider.value <= 0)
        {
            SwitchStage(FishingStage.FishCaught);
        }
        else if (reelFishTimer < reelFishTimeLimit)
        {
            SwitchStage(FishingStage.FishEscaped);
        }

        float lerpFactor = 1f - (fishEnergySlider.value / fishEnergySlider.maxValue);
        floatObject.transform.position = Vector3.Lerp(floatOriginalPosition, new Vector3(-14, -0.15f, 0.2f), lerpFactor);
        reelFishTimerText.text = reelFishTimer.ToString("F2");

    }

    private IEnumerator MoveFloatToStartPosition()
    {
        Vector3 startPosition = floatObject.transform.position;
        Vector3 endPosition = new Vector3(-12, 2, 0.5f);
        float duration = 0.5f;  // 您可以根据需要更改此值

        for (float t = 0; t < 1; t += Time.deltaTime / duration)
        {
            floatObject.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        
            SwitchStage(FishingStage.CastLine);
        
    }

    private void ResetCameraPosition()
    {
        virtualCamera.transform.position = originalCameraPosition;
    }
}
