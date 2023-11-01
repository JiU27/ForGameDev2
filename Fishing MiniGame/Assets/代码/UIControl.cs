using UnityEngine;
using System.Collections;

public class UIControl : MonoBehaviour
{
    public GameObject ShowCatchFish;
    public GameObject ShowCatchFishMaterial;
    public GameObject ShowFishState;
    public GameObject Prefect;
    public GameObject Canvas;
    public GameObject FishText;
    public Material material1;
    public Material material2;

    void Start()
    {
        InitializeUI();
    }

    public void InitializeUI()
    {
        ShowFishState.SetActive(false);
        Prefect.SetActive(false);
        ShowCatchFish.SetActive(true);
        FishText.SetActive(false);
        SetMaterial(ShowCatchFishMaterial, material1);
    }

    public void TryingToCaught(float judgementTime, float startTime, float endTime)
    {
        if (judgementTime > startTime && judgementTime < endTime)
        {
            SetMaterial(ShowCatchFishMaterial, material2);
        }
        else
        {
            SetMaterial(ShowCatchFishMaterial, material1);
        }
    }

    public void Camera4()
    {
        ShowCatchFish.SetActive(false);
        ShowFishState.SetActive(false);
        FishText.SetActive(true);
    }

    public void ReelFish()
    {
        ShowCatchFish.SetActive(false);
        ShowFishState.SetActive(true);
        FishText.SetActive(false);
        SetMaterial(ShowCatchFishMaterial, material1);
    }

    public void CastLine()
    {
        ShowFishState.SetActive(false);
        ShowCatchFish.SetActive(true);
        FishText.SetActive(false);
        SetMaterial(ShowCatchFishMaterial, material1);
    }

    private void SetMaterial(GameObject obj, Material mat)
    {
        Renderer rend = obj.GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material = mat;
        }
    }

    public void ShowPrefect()
    {
        Prefect.SetActive(true);
    }

    public void HidePrefect()
    {
        Prefect.SetActive(false);
    }

    public void ShowCanvas()
    {
        Canvas.SetActive(true);
    }

    public void HideCanvas()
    {
        Canvas.SetActive(false);
    }

    

}
