using System.Collections;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    public string[] targetTags = { "x", "y", "z" };
    public float delay = 2f; // x 秒的等待时间在这里设置

    private void OnTriggerEnter(Collider other)
    {
        foreach (string tag in targetTags)
        {
            if (other.CompareTag(tag))
            {
                StartCoroutine(DestroyAfterSeconds(other.gameObject, delay));
                return;
            }
        }
    }

    private IEnumerator DestroyAfterSeconds(GameObject target, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (target != null) // 检查对象是否在等待期间已经被销毁
        {
            Destroy(target);
        }
    }
}

