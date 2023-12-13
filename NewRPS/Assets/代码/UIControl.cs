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

        // ��Ӱ�ť����¼��������
        Restart1.onClick.AddListener(RestartGame);
        Restart2.onClick.AddListener(RestartGame);
        Restart3.onClick.AddListener(RestartGame);
    }

    void Update()
    {
        // ��ȡ���������е���ҺͶ��ֵ�HP�ı�ֵ
        int playerHP = maincontrol.currentHP;
        int opponentHP = maincontrol.OpcurrentHP;

        // �������Ƿ�Ӯ��
        if (playerHP > 0 && opponentHP <= 0)
        {
            maincontrol.InfluenceUI.SetActive(false);
            maincontrol.TacticUI.SetActive(false);
            maincontrol.playerAnimator.SetBool("R&D", false);
            opcontrol.opponentAnimator.SetBool("OpR&B", false);
            opcontrol.opponentAnimator.SetBool("OpLose", true);
            Win.SetActive(true);
        }
        // �������Ƿ�Ӯ��
        else if (playerHP <= 0 && opponentHP > 0)
        {
            maincontrol.InfluenceUI.SetActive(false);
            maincontrol.TacticUI.SetActive(false);
            maincontrol.playerAnimator.SetBool("R&D", false);
            opcontrol.opponentAnimator.SetBool("OpR&B", true);
            maincontrol.playerAnimator.SetBool("Lose", true);
            Lose.SetActive(true);
        }
        // ����Ƿ�ƽ��
        else if (playerHP <= 0 && opponentHP <= 0)
        {
            maincontrol.InfluenceUI.SetActive(false);
            maincontrol.TacticUI.SetActive(false);
            maincontrol.playerAnimator.SetBool("R&D", true);
            opcontrol.opponentAnimator.SetBool("OpR&B", true);
            Draw.SetActive(true);
        }
    }

    // ������Ϸ�ķ���
    void RestartGame()
    {
        // ���¼��ص�ǰ����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
