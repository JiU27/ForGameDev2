using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLine : MonoBehaviour
{
    private LineRenderer lineRenderer;

    public bool enableDrawing = true;

    void Start()
    {
        GameObject lineObject = new GameObject("MouseLine");
        lineRenderer = lineObject.AddComponent<LineRenderer>();

        lineRenderer.startWidth = 0.05f; 
        lineRenderer.endWidth = 0.05f; 
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        lineRenderer.sortingOrder = -100;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.zero);
    }

    void Update()
    {
        if (!enableDrawing)
        {
            return; // 如果绘制被禁用，不执行任何操作
        }

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;

        lineRenderer.SetPosition(0, mouseWorldPosition);
        lineRenderer.SetPosition(1, new Vector3(mouseWorldPosition.x, mouseWorldPosition.y - 10, 0)); 
    }

    public void StopDrawing()
    {
        enableDrawing = false;
        lineRenderer.enabled = false; 
    }
}
