using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public Material targetMaterial; // 这是您希望所有“Bowl”对象都变成的材质
    public int sceneIndexToLoad; // 您希望加载的场景的索引

    private void Update()
    {
        if (AllBowlsHaveTargetMaterial())
        {
            // 如果所有“Bowl”的材质都已更改为目标材质，则加载指定索引的场景
            SceneManager.LoadScene(sceneIndexToLoad);
        }
    }

    private bool AllBowlsHaveTargetMaterial()
    {
        // 找到场景中所有标签为“Bowl”的对象
        GameObject[] bowls = GameObject.FindGameObjectsWithTag("Bowl");

        // 遍历每一个“Bowl”对象
        foreach (GameObject bowl in bowls)
        {
            // 对于每一个对象，检查它的材质是否与目标材质匹配
            if (bowl.GetComponent<Renderer>().sharedMaterial != targetMaterial)
            {
                // 如果有至少一个对象的材质不匹配目标材质，则返回 false
                return false;
            }
        }

        // 如果循环结束都没有返回 false，则说明所有“Bowl”对象的材质都与目标材质匹配，返回 true
        return true;
        //11
    }
}
