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
        // �����λ�ô���Ļ�ռ�ת��Ϊ����ռ�
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // ����Ƿ����˴�GameObject��Collider
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (hit.collider != null && hit.collider.gameObject == this.gameObject)
        {
            Debug.Log(gameObject.name + " was clicked.");
        }
    }
}
