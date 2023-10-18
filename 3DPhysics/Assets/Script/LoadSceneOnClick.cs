using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    public int sceneIndexToLoad; // ��������Unity�༭�������ô�ֵ

    private void OnMouseDown()
    {
        LoadScene();
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneIndexToLoad);
    }
}
