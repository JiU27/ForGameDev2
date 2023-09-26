using System.Collections;
using UnityEngine;

public class GrowAndFade : MonoBehaviour
{
    public float growRate = 1f;    // x�����ʣ����ڷŴ�GameObject
    public float fadeDuration = 3f; // y�룬��ʾ��͸���ĳ���ʱ��

    private Renderer rend;  // GameObject��Renderer�����ڷ��ʲ���

    private void Start()
    {
        // ��ȡRenderer���
        rend = GetComponent<Renderer>();
        StartCoroutine(FadeOutAndDestroy());
    }

    private void Update()
    {
        // ʹGameObject��x��zֵ�Ŵ�
        transform.localScale += new Vector3(growRate * Time.deltaTime, 0, growRate * Time.deltaTime);
    }

    IEnumerator FadeOutAndDestroy()
    {
        if (rend != null)
        {
            float elapsedTime = 0f;
            Color initialColor = rend.material.color;
            Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0); // Ŀ����ɫ����ȫ͸����

            while (elapsedTime < fadeDuration)
            {
                // �𽥸ı���ʵ�͸����
                rend.material.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // ��ȫ����Ϊ͸��
            rend.material.color = targetColor;
            Destroy(gameObject); // ����Ϸ������GameObject
        }
    }
}
