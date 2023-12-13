using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Button button1; // ָ�� Button1 ������
    public Button button2; // ָ�� Button2 ������

    void Start()
    {
        // Ϊ Button1 ��ӵ���¼�����
        button1.onClick.AddListener(delegate { LoadScene(1); });

        // Ϊ Button2 ��ӵ���¼�����
        button2.onClick.AddListener(delegate { LoadScene(2); });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0); // ���س�������Ϊ 0 �ĳ���
        }
    }

    void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex); // ����ָ�������ĳ���
    }
}

