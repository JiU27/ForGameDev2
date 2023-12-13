using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseDetective : MonoBehaviour
{
    private int initialPlayerCount;
    public float reloadDelay = 5f;
    private SpriteRenderer spriteRenderer;
    private bool isReloading = false;
    public RainSpawner rainSpawner;
    public int winSceneIndex;

    void Start()
    {
        initialPlayerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        int currentPlayerCount = GameObject.FindGameObjectsWithTag("Player").Length;

        if (currentPlayerCount < initialPlayerCount && !isReloading)
        {
            StartCoroutine(FadeOutAndReload());
            isReloading = true;
        }

        if (rainSpawner.HasFinishedSpawning)
        {
            CheckWinCondition();
        }
    }

    private void CheckWinCondition()
    {
        if (GameObject.FindGameObjectsWithTag("Rain").Length == 0 &&
            GameObject.FindGameObjectsWithTag("Player").Length == initialPlayerCount)
        {
            StartCoroutine(FadeOutAndLoadScene(winSceneIndex));
        }
    }
    private IEnumerator FadeOutAndReload()
    {
        float elapsedTime = 0;
        Color color = spriteRenderer.color;
        while (elapsedTime < reloadDelay)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / reloadDelay);
            spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator FadeOutAndLoadScene(int sceneIndex)
    {
        float elapsedTime = 0;
        Color color = spriteRenderer.color;
        while (elapsedTime < reloadDelay)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / reloadDelay);
            spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        SceneManager.LoadScene(sceneIndex);
    }
}
