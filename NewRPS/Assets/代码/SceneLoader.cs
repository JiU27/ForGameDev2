using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Button button1; // 指向 Button1 的引用
    public Button button2; // 指向 Button2 的引用

    void Start()
    {
        // 为 Button1 添加点击事件监听
        button1.onClick.AddListener(delegate { LoadScene(1); });

        // 为 Button2 添加点击事件监听
        button2.onClick.AddListener(delegate { LoadScene(2); });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0); // 加载场景索引为 0 的场景
        }
    }

    void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex); // 加载指定索引的场景
    }
}

