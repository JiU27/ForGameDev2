using UnityEngine;

public class UIControl : MonoBehaviour
{
    // Public fields
    public GameObject ShowCatchFish;
    public GameObject ShowCatchFishMaterial;
    public GameObject ShowFishState;
    public Material material1;
    public Material material2;
    public GameObject Sigh;

    // Start is called before the first frame update
    void Start()
    {
        InitializeUI();
    }

    public void InitializeUI()
    {
        // At the start of the game:
        Sigh.SetActive(false);
        ShowFishState.SetActive(false);
        ShowCatchFish.SetActive(true);
        SetMaterial(ShowCatchFishMaterial, material1);
    }

    public void TryingToCaught(float judgementTime, float startTime, float endTime)
    {
        // When GameControl is at TryingToCaught:
        if (judgementTime > startTime && judgementTime < endTime)
        {
            SetMaterial(ShowCatchFishMaterial, material2);
            Sigh.SetActive(true);
        }
        else
        {
            SetMaterial(ShowCatchFishMaterial, material1);
            Sigh.SetActive(false);
        }
    }

    public void ReelFish()
    {
        // When GameControl is at ReelFish:
        ShowCatchFish.SetActive(false);
        ShowFishState.SetActive(true);
        Sigh.SetActive(false);
        SetMaterial(ShowCatchFishMaterial, material1);
    }

    public void CastLine()
    {
        // When GameControl is at CastLine:
        ShowFishState.SetActive(false);
        ShowCatchFish.SetActive(true);
        Sigh.SetActive(false);
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
}
