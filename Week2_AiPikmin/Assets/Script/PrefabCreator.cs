using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabCreator : MonoBehaviour
{
    public GameObject prefabToCreate; // Assign the prefab to create in Unity Inspector
    public float detectionRadius = 5f;
    public string targetTag = "x";
    private GameObject spawnedPrefab;

    void Update()
    {
        // Detect objects within radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);

        // Flag to check if target is in range
        bool targetInRange = false;

        foreach (var hitCollider in hitColliders)
        {
            // If a GameObject with the target tag is within range, set the flag to true and break the loop
            if (hitCollider.CompareTag(targetTag))
            {
                targetInRange = true;
                break;
            }
        }

        // If target is in range and prefab is not spawned, spawn it
        if (targetInRange && spawnedPrefab == null)
        {
            spawnedPrefab = Instantiate(prefabToCreate, transform.position, Quaternion.identity);
        }
        // If target is out of range and prefab is spawned, destroy it
        else if (!targetInRange && spawnedPrefab != null)
        {
            Destroy(spawnedPrefab);
        }
    }

    // Optional: Visualize the detection radius in the Unity Editor
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
