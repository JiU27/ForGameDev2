using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 10; // 设置倒计时时间（单位：秒）
    public TextMeshProUGUI timeText; // 使用 TextMeshProUGUI 替换 Text
    public bool timerIsRunning = false; // 控制计时器的开关
    public int sceneIndexToLoad; // 您可以在Unity编辑器中设置此值作为要加载的场景索引

    private void Start()
    {
        // 初始化，开始倒计时
        timerIsRunning = true;
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                // 减去已经过去的时间
                timeRemaining -= Time.deltaTime;
                // 更新 UI 文本
                DisplayTime(timeRemaining);
            }
            else
            {
                // 时间到，结束计时
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;

                // 加载特定索引的场景
                SceneManager.LoadScene(sceneIndexToLoad);
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        // 将剩余时间从秒转换为分钟:秒的格式
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // 更新 UI 文本内容
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
