using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public Button restartButton; // Set this in the inspector using your Restart Button

    void Start()
    {
        // Make sure to add the RestartScene function to the button's "onClick" event.
        restartButton.onClick.AddListener(RestartScene);
    }

    void RestartScene()
    {
        // This line reloads the current scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
