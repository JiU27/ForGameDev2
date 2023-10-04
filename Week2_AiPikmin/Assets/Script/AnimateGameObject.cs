using UnityEngine;
using System.Collections;

public class AnimateGameObject : MonoBehaviour
{
    public float xSecondsToGrow = 2f; // x - 放大时间
    public float yGrowSpeed = 1.5f;   // y - 放大速度
    public float mSecondsToShrink = 2f; // m - 缩小时间
    public float nShrinkSpeed = 1.5f; // n - 缩小速度

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
            // 放大
            float elapsedTime = 0;
            while (elapsedTime < xSecondsToGrow)
            {
                transform.localScale += new Vector3(1, 1, 1) * yGrowSpeed * Time.deltaTime;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // 缩小
            elapsedTime = 0;
            while (elapsedTime < mSecondsToShrink)
            {
                transform.localScale -= new Vector3(1, 1, 1) * nShrinkSpeed * Time.deltaTime;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.localScale = initialScale;  // 重置大小
        }
    }

    IEnumerator FadeObject()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        while (true)
        {
            // 变透明
            float elapsedTime = 0;
            while (elapsedTime < xSecondsToGrow)
            {
                Color color = spriteRenderer.color;
                color.a = Mathf.Lerp(1f, 0f, elapsedTime / xSecondsToGrow);
                spriteRenderer.color = color;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // 等待
            yield return new WaitForSeconds(mSecondsToShrink);

            // 立即恢复不透明
            Color colorReset = spriteRenderer.color;
            colorReset.a = 1f;
            spriteRenderer.color = colorReset;

            yield return null;
        }
    }
}
