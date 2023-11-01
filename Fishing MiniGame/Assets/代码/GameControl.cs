using Cinemachine;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEditor;

public class GameControl : MonoBehaviour
{
    public enum FishingStage
    {
        CastLine,
        WaitForFish,
        TryingToCaught,
        ReelFish,
        FishCaught,
        FishEscaped,
        Camera4
    }

    // Public Variables
    public UIControl uiControl;

    public ReelFish reelFishScript1;
    public ReelFish reelFishScript2;
    public ReelFish reelFishScript3;
    public ReelFish[] reelFishScripts;
    private int currentReelFishIndex;


    public FishingStage currentStage;
    public GameObject floatObject, rob, fishPrefab1, fishPrefab2, fishPrefab3;
    public TextMeshProUGUI fishCountText;
    public TextMeshProUGUI fishWeight;
    public TextMeshProUGUI fishName;
    public UnityEngine.UI.Slider fishEnergySlider;
    public float judgementTime = 5f;
    public float startTime = 4f;
    public float endTime = 2f;

    // Private Variables
    private CameraControl cameraControl;
    private int fishCount;
    private float floatYRange = 0.05f;
    private float floatSpeed = 0.1f;
    private float tryingToCaughtStartTime;
    private Vector3 floatOriginalPosition = new Vector3(-19, -0.13f, -0.5f);

    private void Start()
    {
        InitializeGame();
        cameraControl = FindObjectOfType<CameraControl>();
        reelFishScripts = new ReelFish[] { reelFishScript1, reelFishScript2, reelFishScript3 };
    }

    private void Update()
    {
        HandleGameStages();
        if (currentStage == FishingStage.FishEscaped)
        {
            cameraControl.HandleFishEscapedState();
        }
        else if (currentStage == FishingStage.CastLine)
        {
            cameraControl.HandleCastLineState();
        }
    }

    private void InitializeGame()
    {
        fishCount = Random.Range(2, 4);
        fishCountText.text = "Fish count: " + fishCount;
        currentStage = FishingStage.CastLine;
        reelFishScript1.SetTime();
        reelFishScript2.SetTime();
        reelFishScript3.SetTime();
        fishEnergySlider.value = fishEnergySlider.maxValue;
    }

    public void HandleGameStages()
    {
        switch (currentStage)
        {
            case FishingStage.CastLine:
                uiControl.CastLine();
                if (fishCount <= 0)
                {               
                    fishCountText.text = "All fish caught! Press R to reset.";

                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        SceneManager.LoadScene(0);
                    }
                }
                else
                {
                    if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
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
                cameraControl.ApproachFloatObject();
                uiControl.TryingToCaught(judgementTime, startTime, endTime);
                HandleFishTryingToBeCaught();
                break;

            case FishingStage.ReelFish:
                uiControl.ReelFish();
                reelFishScripts[currentReelFishIndex].HandleReelFish();
                break;

            case FishingStage.Camera4:
                uiControl.Camera4();
                cameraControl.SwitchToCamera4();
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                {
                    SwitchStage(FishingStage.CastLine);
                }
                break;
        }
    }

    public void SwitchStage(FishingStage nextStage)
    {
        StopCoroutine(WaitForFishToBite(0));

        if (nextStage == FishingStage.TryingToCaught)
        {
            tryingToCaughtStartTime = Time.time;
            judgementTime = 3f; 
        }
        else if (nextStage == FishingStage.WaitForFish)
        {
            float randomWaitTime = Random.Range(4f, 8f); 
            StartCoroutine(WaitForFishToBite(randomWaitTime));
        }
        else if (nextStage == FishingStage.ReelFish)
        {
            currentReelFishIndex = Random.Range(0, reelFishScripts.Length);
            reelFishScripts[currentReelFishIndex].BoolSet();
            reelFishScripts[currentReelFishIndex].SetTime();
            fishEnergySlider.value = fishEnergySlider.maxValue;
        }
        else if (nextStage == FishingStage.Camera4)
        {
            switch (currentReelFishIndex)
            {
                case 0:
                    fishName.text = "Normal Fish";
                    fishWeight.text = Random.Range(4f, 5.5f).ToString("F1") + "in";
                    break;
                case 1:
                    fishName.text = "Little Fish";
                    fishWeight.text = Random.Range(2f, 3.5f).ToString("F1") + "in";
                    break;
                case 2:
                    fishName.text = "Shake";
                    fishWeight.text = Random.Range(11f, 16f).ToString("F1") + "in";
                    break;
            }
        }


        currentStage = nextStage;
        switch (nextStage)
        {
            case FishingStage.FishCaught:
                GameObject fishPrefab = null;
                switch (currentReelFishIndex)
                {
                    case 0:
                        fishPrefab = fishPrefab1;
                        break;
                    case 1:
                        fishPrefab = fishPrefab2;
                        break;
                    case 2:
                        fishPrefab = fishPrefab3;
                        break;
                }
                Instantiate(fishPrefab, floatObject.transform.position, Quaternion.identity);
                StartCoroutine(MoveFloatToStartPosition1());
                fishCount--;
                fishCountText.text = "Fish count: " + fishCount;
                break;

            case FishingStage.FishEscaped:
                StartCoroutine(MoveFloatToStartPosition2());
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
        
        float animationDuration = 2.0f; 

        
        Quaternion startRotation = rob.transform.rotation;
        Quaternion midRotation = Quaternion.Euler(0, 0, 10);
        Quaternion endRotation = Quaternion.Euler(0, 0, 50);

        
        for (float t = 0; t < 1; t += Time.deltaTime / (animationDuration / 2))
        {
            rob.transform.rotation = Quaternion.Lerp(startRotation, midRotation, t);
            yield return null;
        }
        rob.transform.rotation = midRotation;

        
        for (float t = 0; t < 1; t += Time.deltaTime / (animationDuration / 2))
        {
            rob.transform.rotation = Quaternion.Lerp(midRotation, endRotation, t);
            yield return null;
        }
        rob.transform.rotation = endRotation;

        
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

        if (judgementTime > startTime && judgementTime < endTime)
        {
            cameraControl.SwitchToCamera2();
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Input Detected (Mouse or Space)");
            

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
            
            SwitchStage(FishingStage.FishEscaped);
        }
    }

    private IEnumerator MoveFloatToStartPosition1()
    {
        Vector3 startPosition = floatObject.transform.position;
        Vector3 endPosition = new Vector3(-12, 2, 0.5f);
        float duration = 0.5f;

        for (float t = 0; t < 1; t += Time.deltaTime / duration)
        {
            floatObject.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        SwitchStage(FishingStage.Camera4);

    }


    private IEnumerator MoveFloatToStartPosition2()
    {
        Vector3 startPosition = floatObject.transform.position;
        Vector3 endPosition = new Vector3(-12, 2, 0.5f);
        float duration = 0.5f;

        for (float t = 0; t < 1; t += Time.deltaTime / duration)
        {
            floatObject.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        SwitchStage(FishingStage.CastLine);
        
    }

}
