using System.Collections;
using UnityEngine;

public class PlayerDestroy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Thing" || collision.gameObject.tag == "Rain")
        {
            Vector2 inDirection = rb.velocity;
            Vector2 inNormal = collision.contacts[0].normal;
            Vector2 reflect = Vector2.Reflect(inDirection, inNormal);

            rb.AddForce(reflect * 0.1f, ForceMode2D.Impulse);

            StartCoroutine(FadeOutAndDestroy());
        }
        else if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator FadeOutAndDestroy()
    {
        yield return new WaitForSeconds(0f); 

        // 在1秒内逐渐变透明
        float fadeDuration = 1f;
        float fadeElapsed = 0;
        Color originalColor = spriteRenderer.color;

        while (fadeElapsed < fadeDuration)
        {
            fadeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, fadeElapsed / fadeDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        Destroy(gameObject); 
    }
}
