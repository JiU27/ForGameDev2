using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 炮灰 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Rain"))
        {
            Debug.Log("撞上了");
            StartCoroutine(ChangeColorAndFadeOut());
        }
    }

    private IEnumerator ChangeColorAndFadeOut()
    {
        float changeColorDuration = 1f;
        float elapsed = 0f;
        Color originalColor = spriteRenderer.color;
        Color targetColor = Color.red;

        while (elapsed < changeColorDuration)
        {
            elapsed += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(originalColor, targetColor, elapsed / changeColorDuration);
            yield return null;
        }

        float fadeDuration = 2f;
        elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            spriteRenderer.color = new Color(targetColor.r, targetColor.g, targetColor.b, alpha);
            yield return null;
        }

        Destroy(gameObject); 
    }
}
