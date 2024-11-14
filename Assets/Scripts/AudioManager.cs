using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic;  // Reference to your background music
    private AudioSource audioSource;    // Reference to AudioSource component

    void Awake()
    {
        // If there's already an AudioManager in the scene, destroy this duplicate
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject); // Destroy this instance if another exists
        }

        // Don't destroy this object when loading new scenes
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Set the background music to play
        if (backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic; // Assign the background music clip
            audioSource.loop = true; // Loop the music
            audioSource.Play(); // Play the music
        }
    }

    // Method to adjust the volume
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;  // Volume range between 0 and 1
    }

    // Method to stop the music
    public void StopMusic()
    {
        audioSource.Stop();
    }

    // Method to resume the music
    public void ResumeMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
