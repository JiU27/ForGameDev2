using UnityEngine;

public class ExitOnClick : MonoBehaviour
{
    private void OnMouseDown()
    {
        QuitGame();
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
