using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn; // Ҫ���ɵ�Prefab
    public float spawnDuration = 10f; // �ܹ����ɵ�ʱ�䣨�룩
    public float spawnRate = 1f; // ÿ�����ɵ�Prefab����

    private float spawnTimer = 0f;
    private float totalTimeElapsed = 0f;

    public bool HasFinishedSpawning { get; private set; } = false;
    public GameObject Land1;

    void Start()
    {
        this.enabled = false;
    }

    private void Update()
    {
        totalTimeElapsed += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        if (totalTimeElapsed < spawnDuration)
        {
            if (spawnTimer >= 1f / spawnRate)
            {
                SpawnPrefab();
                Land1.SetActive(true);
                spawnTimer = 0f;
            }
        }
        else if (!HasFinishedSpawning)
        {
            HasFinishedSpawning = true;
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
