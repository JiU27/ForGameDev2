using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewSceneLoader : MonoBehaviour
{
    public int sceneIndex; 

    private void OnMouseDown()
    {
        LoadSceneByIndex();
    }

    private void LoadSceneByIndex()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
