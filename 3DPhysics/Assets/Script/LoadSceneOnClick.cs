using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    public int sceneIndexToLoad; // 您可以在Unity编辑器中设置此值

    private void OnMouseDown()
    {
        LoadScene();
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneIndexToLoad);
    }
}
