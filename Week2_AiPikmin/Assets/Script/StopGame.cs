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
        UnityEditor.EditorApplication.isPlaying = false; // 如果在Unity编辑器中运行，这将停止游戏播放模式
#else
        Application.Quit(); // 如果在实际的游戏应用中运行，这将关闭游戏
#endif
    }
}
