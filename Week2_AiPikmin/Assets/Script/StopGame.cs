using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopGame : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // �����Unity�༭�������У��⽫ֹͣ��Ϸ����ģʽ
#else
        Application.Quit(); // �����ʵ�ʵ���ϷӦ�������У��⽫�ر���Ϸ
#endif
    }
}
