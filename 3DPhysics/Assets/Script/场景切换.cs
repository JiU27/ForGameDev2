using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class 场景切换 : MonoBehaviour
{
    public Material newMaterial; // Drag your new material here through the Inspector
    public int IndexCode; // 场景的IndexCode
    public float y = 2f; // 默认等待2秒，可以在编辑器中修改

    private void OnCollisionEnter(Collision collision)
    {
        ChangeMaterial();
        StartCoroutine(WaitAndChangeScene());        
    }

    private void ChangeMaterial()
    {
        Renderer rend = GetComponent<Renderer>();
        if (rend != null && newMaterial != null)
        {
            rend.material = newMaterial;
        }
        else if (rend == null)
        {
            Debug.LogError("No Renderer component found on the GameObject.");
        }
        else if (newMaterial == null)
        {
            Debug.LogError("No new material assigned.");
        }
    }

    IEnumerator WaitAndChangeScene()
    {
        yield return new WaitForSeconds(y);
        SceneManager.LoadScene(IndexCode);
    }
}
