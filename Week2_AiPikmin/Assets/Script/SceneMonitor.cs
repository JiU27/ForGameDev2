using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;  // 引入TextMeshPro的命名空间

public class SceneMonitor : MonoBehaviour
{
    public string[] tagsToMonitor = { "x", "y", "z" };
    public float waitTime = 5f;
    public int sceneToLoad = 1;
    public TextMeshProUGUI countText;

    void Update()
    {
        UpdateCountText();

        if (AreAllTagsAbsent())
        {
            StartCoroutine(WaitAndChangeScene(waitTime));
        }
    }

    private bool AreAllTagsAbsent()
    {
        foreach (string tag in tagsToMonitor)
        {
            if (GameObject.FindGameObjectWithTag(tag) != null)
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator WaitAndChangeScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (AreAllTagsAbsent())
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void UpdateCountText()
    {
        int count = 0;
        foreach (string tag in tagsToMonitor)
        {
            count += GameObject.FindGameObjectsWithTag(tag).Length;
        }

        if (count == 0)
        {
            StartCoroutine(DisplayWinMessageWithCountdown(waitTime));
        }
        else
        {
            countText.text = "Treasure Remaining: " + count;
        }
    }

    private IEnumerator DisplayWinMessageWithCountdown(float seconds)
    {
        while (seconds > 0)
        {
            countText.text = "You Win! Back to Main Menu in: " + Mathf.Ceil(seconds) + "s";
            seconds -= Time.deltaTime;
            yield return null;
        }
    }
}
