using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class 降雨 : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float spawnDuration, spawnRate;
    public GameObject gameObjectToFade;
    public float fadeDuration = 2f;

    private 协程动画 gameObjectsMovement;
    private bool canSpawn = false;
    private float totalTimeElapsed = 0f;
    private float spawnTimer = 0f;

    private void Start()
    {
        gameObjectsMovement = FindObjectOfType<协程动画>();
        if (gameObjectsMovement != null)
        {
            gameObjectsMovement.OnAllMovementsCompleted += EnableSpawning;
        }
    }

    private void EnableSpawning()
    {
        canSpawn = true;
    }

    private void Update()
    {
        if (canSpawn)
        {
            totalTimeElapsed += Time.deltaTime;
            spawnTimer += Time.deltaTime;

            if (totalTimeElapsed < spawnDuration)
            {
                if (spawnTimer >= 1f / spawnRate)
                {
                    SpawnPrefab();
                    spawnTimer = 0f;
                }
            }
            else
            {
                StartCoroutine(FadeInAndLoadScene());
                canSpawn = false; // 停止生成
            }
        }
    }

    private IEnumerator FadeInAndLoadScene()
    {
        SpriteRenderer spriteRenderer = gameObjectToFade.GetComponent<SpriteRenderer>();
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
            yield return null;
        }

        SceneManager.LoadScene(1); // 加载索引为1的场景
    }

    private void OnDestroy()
    {
        if (gameObjectsMovement != null)
        {
            gameObjectsMovement.OnAllMovementsCompleted -= EnableSpawning;
        }
    }

    private void SpawnPrefab()
    {
        float rangeX = transform.localScale.x / 2;
        Vector3 spawnPosition = new Vector3(
            Random.Range(transform.position.x - rangeX, transform.position.x + rangeX),
            transform.position.y,
            transform.position.z
        );

        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }
}
