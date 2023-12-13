using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 协程动画 : MonoBehaviour
{
    public GameObject gameObject1, gameObject2, gameObject3;
    public float xSeconds, ySeconds, zSeconds, mSeconds, nSeconds, oSeconds;

    public GameObject dialogue;

    public delegate void MovementCompletedAction();
    public event MovementCompletedAction OnAllMovementsCompleted;

    private void Start()
    {
        StartCoroutine(MoveAndTransformGameObjects());
        dialogue.SetActive(false);
    }

    private IEnumerator MoveAndTransformGameObjects()
    {
        // GameObject1 移动
        yield return StartCoroutine(MoveGameObject(gameObject1, new Vector3(3.9f, -2.5f, 0), xSeconds));

        // GameObject2 第一次移动
        yield return StartCoroutine(MoveGameObject(gameObject2, new Vector3(1.8f, -2.5f, 0), ySeconds));

        // GameObject2 停顿
        yield return new WaitForSeconds(zSeconds);

        // GameObject2 和 GameObject3 同时变化
        StartCoroutine(MoveGameObject(gameObject2, new Vector3(1.5f, -2.5f, 0), mSeconds));
        StartCoroutine(TransformGameObject(gameObject3, new Vector3(2.3f, -1.8f, 0), new Vector3(1.8f, 2.4f, 1), mSeconds));
        yield return new WaitForSeconds(mSeconds);

        // GameObject2 和 GameObject3 第二次同时变化
        StartCoroutine(MoveGameObject(gameObject2, new Vector3(2.3f, -2.5f, 0), nSeconds));
        StartCoroutine(TransformGameObject(gameObject3, new Vector3(2.8f, -1.8f, 0), new Vector3(0.5f, 2.4f, 1), nSeconds));
        yield return new WaitForSeconds(nSeconds);

        // GameObject2 最后移动
        yield return StartCoroutine(MoveGameObject(gameObject2, new Vector3(0.6f, -2.5f, 0), oSeconds));

        OnAllMovementsCompleted?.Invoke();
        dialogue.SetActive(true);
    }

    private IEnumerator MoveGameObject(GameObject obj, Vector3 targetPos, float duration)
    {
        Vector3 startPos = obj.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            obj.transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = targetPos;
    }

    private IEnumerator TransformGameObject(GameObject obj, Vector3 targetPos, Vector3 targetScale, float duration)
    {
        Vector3 startPos = obj.transform.position;
        Vector3 startScale = obj.transform.localScale;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            obj.transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            obj.transform.localScale = Vector3.Lerp(startScale, targetScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = targetPos;
        obj.transform.localScale = targetScale;
    }

}
