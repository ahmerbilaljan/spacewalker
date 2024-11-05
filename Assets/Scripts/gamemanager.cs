using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text scoreText;
    private int score = 0;

    private void Awake()
    {
        // Singleton pattern to ensure there's only one GameManager instance
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncrementScore()
    {
        score += 1;
        scoreText.text = "Score: " + score.ToString();
    }
}
