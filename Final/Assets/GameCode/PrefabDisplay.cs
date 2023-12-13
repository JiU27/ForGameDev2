using UnityEngine;

public class PrefabDisplay : MonoBehaviour
{
    public GameObject[] displayItems;

    public float xOffset = 1.5f; // Prefab֮���ˮƽ���
    public Vector3 displayOffset = Vector3.zero; // PrefabDisplay ��λ��ƫ��

    public void InitializeDisplay(GameObject[] prefabs)
    {
        displayItems = new GameObject[prefabs.Length];

        for (int i = 0; i < prefabs.Length; i++)
        {
            // ʹ��xOffset��displayOffset��ȷ��λ��
            Vector3 position = new Vector3(i * xOffset, 0, 0) + this.transform.position + displayOffset;

            GameObject displayItem = Instantiate(prefabs[i], position, Quaternion.identity);
            Vector3 originalScale = displayItem.transform.localScale;
            displayItem.transform.localScale = originalScale * 0.5f;

            // �Ƴ�BoxCollider2D��Rigidbody2D���
            RemoveComponents<BoxCollider2D>(displayItem);
            RemoveComponents<Rigidbody2D>(displayItem);

            displayItems[i] = displayItem;
        }
    }

    private void RemoveComponents<T>(GameObject gameObject) where T : Component
    {
        T[] components = gameObject.GetComponentsInChildren<T>();
        foreach (T component in components)
        {
            Destroy(component);
        }
    }

    public void RemoveDisplayItem(int index)
    {
        if (index < displayItems.Length)
        {
            Destroy(displayItems[index]);
            displayItems[index] = null;
        }
    }
}
