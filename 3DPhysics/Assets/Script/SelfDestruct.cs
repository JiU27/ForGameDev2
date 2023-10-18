using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float delayBeforeDestroy = 2f;
    public GameObject prefabToSpawnOnCollision; // Reference to the prefab you want to spawn

    private bool hasScored = false;
    public bool isActive = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (hasScored) return;

        switch (collision.gameObject.tag)
        {
            case "Bowl":
            case "Bowl-2":
                SpawnPrefabAtCollisionPoint(collision.contacts[0].point); // Spawn the prefab at the collision point
                DestroyAfterDelay();
                break;

            case "Ground":
                SpawnPrefabAtCollisionPoint(collision.contacts[0].point); // Spawn the prefab at the collision point
                DestroyAfterDelay();
                break;
        }
    }

    private void SpawnPrefabAtCollisionPoint(Vector3 position)
    {
        if (prefabToSpawnOnCollision != null) // Safety check
        {
            Instantiate(prefabToSpawnOnCollision, position, Quaternion.identity);
        }
    }

    private void DestroyAfterDelay()
    {
        isActive = false;
        Destroy(gameObject, delayBeforeDestroy);
    }
}
