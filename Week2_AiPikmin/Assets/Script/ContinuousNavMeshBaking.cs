using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class ContinuousNavMeshBaking : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;
    public float bakeInterval = 5.0f; // ºæ±º¼ä¸ôÊ±¼ä£¨Ãë£©

    private float timeSinceLastBake = 0.0f;

    private void Start()
    {
        if (navMeshSurface == null)
        {
            Debug.LogError("NavMeshSurface is not assigned.");
            enabled = false;
            return;
        }

        BakeNavMesh();
    }

    private void Update()
    {
        timeSinceLastBake += Time.deltaTime;

        if (timeSinceLastBake >= bakeInterval)
        {
            BakeNavMesh();
            timeSinceLastBake = 0.0f;
        }
    }

    private void BakeNavMesh()
    {
        navMeshSurface.BuildNavMesh();
        Debug.Log("NavMesh baked.");
    }
}
