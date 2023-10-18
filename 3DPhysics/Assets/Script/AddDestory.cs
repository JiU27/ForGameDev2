using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDestory : MonoBehaviour
{
    public float delayBeforeDestroy = 5f;  // Ä¬ÈÏÎª5Ãëºó´Ý»Ù

    void Start()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    private System.Collections.IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeDestroy);
        Destroy(gameObject);
    }
}
