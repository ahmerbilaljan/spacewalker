using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // This ensures the object persists across scenes
        }
        else
        {
            Destroy(gameObject); // Prevents duplicates
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene(string SceneName)
    {
        SceneManager.LoadSceneAsync(SceneName);
    }
}
