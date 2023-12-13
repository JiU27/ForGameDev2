using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    public GameObject Win, Lose, Draw;
    public Button Restart1, Restart2, Restart3;
    public MainControl maincontrol;
    public OpponentAIControl opcontrol;

    void Start()
    {
        Win.SetActive(false);
        Lose.SetActive(false);
        Draw.SetActive(false);

        // 添加按钮点击事件处理程序
        Restart1.onClick.AddListener(RestartGame);
        Restart2.onClick.AddListener(RestartGame);
        Restart3.onClick.AddListener(RestartGame);
    }

    void Update()
    {
        // 获取主控制器中的玩家和对手的HP文本值
        int playerHP = maincontrol.currentHP;
        int opponentHP = maincontrol.OpcurrentHP;

        // 检查玩家是否赢了
        if (playerHP > 0 && opponentHP <= 0)
        {
            maincontrol.InfluenceUI.SetActive(false);
            maincontrol.TacticUI.SetActive(false);
            maincontrol.playerAnimator.SetBool("R&D", false);
            opcontrol.opponentAnimator.SetBool("OpR&B", false);
            opcontrol.opponentAnimator.SetBool("OpLose", true);
            Win.SetActive(true);
        }
        // 检查对手是否赢了
        else if (playerHP <= 0 && opponentHP > 0)
        {
            maincontrol.InfluenceUI.SetActive(false);
            maincontrol.TacticUI.SetActive(false);
            maincontrol.playerAnimator.SetBool("R&D", false);
            opcontrol.opponentAnimator.SetBool("OpR&B", true);
            maincontrol.playerAnimator.SetBool("Lose", true);
            Lose.SetActive(true);
        }
        // 检查是否平局
        else if (playerHP <= 0 && opponentHP <= 0)
        {
            maincontrol.InfluenceUI.SetActive(false);
            maincontrol.TacticUI.SetActive(false);
            maincontrol.playerAnimator.SetBool("R&D", true);
            opcontrol.opponentAnimator.SetBool("OpR&B", true);
            Draw.SetActive(true);
        }
    }

    // 重启游戏的方法
    void RestartGame()
    {
        // 重新加载当前场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
