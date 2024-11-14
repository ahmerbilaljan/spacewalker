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
        
        // Save the current level name whenever a scene loads
        PlayerPrefs.SetString("CurrentLevel", scene.name);
    }

    // Method to increment score
    public void IncrementScore()
    {
        score += 1;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    // Method to load the next level
public void NextLevel()
{
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadSceneAsync(currentSceneIndex + 1);

    // Save the new level as "LastLevel"
    PlayerPrefs.SetString("LastLevel", SceneManager.GetSceneByBuildIndex(currentSceneIndex + 1).name);
    PlayerPrefs.Save();
}

public void LoadScene(string sceneName)
{
    // Save the scene name as "LastLevel" for the Continue option
    PlayerPrefs.SetString("LastLevel", sceneName);
    PlayerPrefs.Save();

    // Load the specified scene
    SceneManager.LoadSceneAsync(sceneName);
}

}
