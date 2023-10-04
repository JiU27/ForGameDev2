using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLight : MonoBehaviour
{
    [Header("Tags")]
    public string showTag = "TagX";       
    public string[] hideTags = { "TagY", "TagZ" }; 

    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        Hide();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // 显示自己
                if (hit.collider.CompareTag(showTag))
                {
                    Show();
                }
                // 隐藏自己
                foreach (string hideTag in hideTags)
                {
                    if (hit.collider.CompareTag(hideTag))
                    {
                        Hide();
                        break; // 找到匹配的tag后退出循环
                    }
                }
            }
        }
    }

    public void Show()
    {
        if (rend)
            rend.enabled = true;
    }

    public void Hide()
    {
        if (rend)
            rend.enabled = false;
    }
}
