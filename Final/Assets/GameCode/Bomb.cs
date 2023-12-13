using UnityEngine;
using TMPro; // ����TextMeshPro�����ռ�

public class Bomb : MonoBehaviour
{
    public float countdownTime = 5f; // ����ʱʱ�����룩
    public float explosionForce = 10f; // ��ը����
    public TextMeshPro countdownText; // �����е�TextMeshPro�ı����
    public GameObject countdown;

    private bool isTriggered = false;

    private void Start()
    {
        countdown.SetActive(false);
    }

    void Update()
    {
        if (isTriggered)
        {
            countdownTime -= Time.deltaTime;

            if (countdownText != null)
            {
                countdownText.text = countdownTime.ToString("F2");
            }

            if (countdownTime <= 0)
            {
                Explode();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Thing") && !isTriggered)
        {
            countdown.SetActive(true);
            isTriggered = true;
        }
    }

    private void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 5f);
        foreach (Collider2D nearbyObject in colliders)
        {
            if (nearbyObject.gameObject.tag == "Thing")
            {
                Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 direction = nearbyObject.transform.position - transform.position;
                    direction.Normalize();
                    rb.AddForce(direction * explosionForce, ForceMode2D.Impulse);
                }
            }
        }

        Destroy(gameObject);
    }
}
