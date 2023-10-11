using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public Material originalMaterial; // 这是“Bowl”对象最初的材质

    private void Update()
    {
        if (AllBowlsMaterialChanged())
        {
            // 如果所有“Bowl”的材质都已更改，则重新加载场景
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private bool AllBowlsMaterialChanged()
    {
        // 找到场景中所有标签为“Bowl”的对象
        GameObject[] bowls = GameObject.FindGameObjectsWithTag("Bowl");

        // 遍历每一个“Bowl”对象
        foreach (GameObject bowl in bowls)
        {
            // 对于每一个对象，检查它的材质是否已更改
            if (bowl.GetComponent<Renderer>().sharedMaterial == originalMaterial)
            {
                // 如果有至少一个对象的材质未更改，则返回 false
                return false;
            }
        }

        // 如果循环结束都没有返回 false，则说明所有“Bowl”对象的材质都已更改，返回 true
        return true;
    }
}
