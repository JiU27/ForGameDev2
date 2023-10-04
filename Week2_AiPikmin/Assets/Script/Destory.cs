using System.Collections;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    public string[] targetTags = { "x", "y", "z" };
    public float delay = 2f; // x ��ĵȴ�ʱ������������

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
        if (target != null) // �������Ƿ��ڵȴ��ڼ��Ѿ�������
        {
            Destroy(target);
        }
    }
}

