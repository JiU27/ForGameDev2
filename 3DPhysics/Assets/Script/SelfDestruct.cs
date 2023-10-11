using UnityEngine;
using TMPro;

public class SelfDestruct : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // [Will be removed]
    public float delayBeforeDestroy = 2f;

    private int score = 0;
    private bool hasScored = false;

    public bool isActive = true;

    void Start()
    {
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        if (scoreText == null)
        {
            Debug.LogError("ScoreText not found in the scene.");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Check if collided object has the "Bowl" or "Bowl-1" tag and has not scored yet.
        if ((collision.gameObject.tag == "Bowl") && !hasScored)
        {
            
            AddScore(1); // Add score.
            DestroyAfterDelay(); // Begin destroy sequence.
        }
        else if ((collision.gameObject.tag == "Bowl-2") && !hasScored)
        {
            
            AddScore(1); // Add score.
            DestroyAfterDelay(); // Begin destroy sequence.
        }
        // Check if collided object has the "Ground" tag.
        else if (collision.gameObject.tag == "Ground")
        {
            DestroyAfterDelay(); // Begin destroy sequence.
        }
    }

    private void AddScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = "Score: " + score.ToString();
    }

    private void DestroyAfterDelay()
    {
        isActive = false; // Set isActive to false when destruction is triggered
        // Destroys the object after specified delay.
        Destroy(gameObject, delayBeforeDestroy);
    }
}
