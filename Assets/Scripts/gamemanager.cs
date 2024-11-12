using UnityEngine;
using UnityEngine.SceneManagement;
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
            DontDestroyOnLoad(gameObject); // Ensures this GameObject persists across scenes
            SceneManager.sceneLoaded += OnSceneLoaded; // Add the OnSceneLoaded event
        }
        else
        {
            Destroy(gameObject); // Prevents duplicates
        }
    }

    private void OnDestroy()
    {
        // Unregister the event when the GameManager is destroyed
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reassign scoreText when a new scene is loaded
        scoreText = GameObject.FindWithTag("scoreText")?.GetComponent<Text>();
    }

    // Method to increment score
    public void IncrementScore()
    {
        score += 1;
        scoreText.text = "Score: " + score.ToString();

    }


    // Method to load the next level
    public void NextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Method to load a specific scene by name
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
