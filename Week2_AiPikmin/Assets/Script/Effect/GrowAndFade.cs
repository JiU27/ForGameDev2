using System.Collections;
using UnityEngine;

public class GrowAndFade : MonoBehaviour
{
    public float growRate = 1f; 
    public float fadeDuration = 3f; 

    private Renderer rend;  

    private void Start()
    {
        // 获取Renderer组件
        rend = GetComponent<Renderer>();
        StartCoroutine(FadeOutAndDestroy());
    }

    private void Update()
    {
        // 使GameObject的x和z值放大
        transform.localScale += new Vector3(growRate * Time.deltaTime, 0, growRate * Time.deltaTime);
    }

    IEnumerator FadeOutAndDestroy()
    {
        if (rend != null)
        {
            float elapsedTime = 0f;
            Color initialColor = rend.material.color;
            Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0); 

            while (elapsedTime < fadeDuration)
            {
                // 逐渐改变材质的透明度
                rend.material.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // 完全设置为透明
            rend.material.color = targetColor;
            Destroy(gameObject); // 从游戏中销毁GameObject
        }
    }
}
