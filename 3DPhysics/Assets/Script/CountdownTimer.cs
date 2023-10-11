using UnityEngine;
using TMPro;  // ��Ӷ� TextMeshPro ������
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 10; // ���õ���ʱʱ�䣨��λ���룩
    public TextMeshProUGUI timeText; // ʹ�� TextMeshProUGUI �滻 Text
    public bool timerIsRunning = false; // ���Ƽ�ʱ���Ŀ���

    private void Start()
    {
        // ��ʼ������ʼ����ʱ
        timerIsRunning = true;
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                // ��ȥ�Ѿ���ȥ��ʱ��
                timeRemaining -= Time.deltaTime;
                // ���� UI �ı�
                DisplayTime(timeRemaining);
            }
            else
            {
                // ʱ�䵽��������ʱ
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;

                // ���¼��س���
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        // ��ʣ��ʱ�����ת��Ϊ����:��ĸ�ʽ
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // ���� UI �ı�����
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
