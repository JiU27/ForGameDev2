using UnityEngine;
using System.Collections;

public class AnimateGameObject : MonoBehaviour
{
    public float xSecondsToGrow = 2f; // x - �Ŵ�ʱ��
    public float yGrowSpeed = 1.5f;   // y - �Ŵ��ٶ�
    public float mSecondsToShrink = 2f; // m - ��Сʱ��
    public float nShrinkSpeed = 1.5f; // n - ��С�ٶ�

    private Vector3 initialScale;
    private Color initialColor;

    void Start()
    {
        initialScale = transform.localScale;
        initialColor = GetComponent<SpriteRenderer>().color;
        StartCoroutine(ScaleObject());
        StartCoroutine(FadeObject());
    }

    IEnumerator ScaleObject()
    {
        while (true)
        {
            // �Ŵ�
            float elapsedTime = 0;
            while (elapsedTime < xSecondsToGrow)
            {
                transform.localScale += new Vector3(1, 1, 1) * yGrowSpeed * Time.deltaTime;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // ��С
            elapsedTime = 0;
            while (elapsedTime < mSecondsToShrink)
            {
                transform.localScale -= new Vector3(1, 1, 1) * nShrinkSpeed * Time.deltaTime;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.localScale = initialScale;  // ���ô�С
        }
    }

    IEnumerator FadeObject()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        while (true)
        {
            // ��͸��
            float elapsedTime = 0;
            while (elapsedTime < xSecondsToGrow)
            {
                Color color = spriteRenderer.color;
                color.a = Mathf.Lerp(1f, 0f, elapsedTime / xSecondsToGrow);
                spriteRenderer.color = color;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // �ȴ�
            yield return new WaitForSeconds(mSecondsToShrink);

            // �����ָ���͸��
            Color colorReset = spriteRenderer.color;
            colorReset.a = 1f;
            spriteRenderer.color = colorReset;

            yield return null;
        }
    }
}
