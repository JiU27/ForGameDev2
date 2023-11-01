using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastLine : MonoBehaviour
{
    public GameObject gameObject1;
    public GameObject gameObject2;
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        DrawLine();
    }

    private void DrawLine()
    {
        if (gameObject1 && gameObject2)
        {
            lineRenderer.SetPosition(0, gameObject1.transform.position);
            lineRenderer.SetPosition(1, gameObject2.transform.position);
        }
    }
}
