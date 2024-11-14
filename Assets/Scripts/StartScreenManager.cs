using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    // Set the first level name or index
    private string firstLevel = "Level1"; 

    void Start()
    {
        // Optional: Load last saved level from PlayerPrefs (for "Continue")
        if (!PlayerPrefs.HasKey("LastLevel"))
        {
            PlayerPrefs.SetString("LastLevel", firstLevel);
        }
    }

    public void OnNewGame()
    {
        // Set the level to the first level and load it
        PlayerPrefs.SetString("LastLevel", firstLevel); // Reset to the first level
        SceneManager.LoadScene(firstLevel);
    }

    public void OnContinue()
    {
        // Load the saved level
        string lastLevel = PlayerPrefs.GetString("LastLevel", firstLevel);
        SceneManager.LoadScene(lastLevel);
    }

    public void OnExit()
    {
        // Exit the game
        Application.Quit();
    }
}
