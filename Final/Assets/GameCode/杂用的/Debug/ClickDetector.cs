using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckClick();
        }
    }

    private void CheckClick()
    {
        // 将鼠标位置从屏幕空间转换为世界空间
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 检测是否点击了此GameObject的Collider
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (hit.collider != null && hit.collider.gameObject == this.gameObject)
        {
            Debug.Log(gameObject.name + " was clicked.");
        }
    }
}
