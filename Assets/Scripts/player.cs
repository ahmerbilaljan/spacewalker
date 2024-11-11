using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool isGrounded = true;
    private Animator animator; // Reference to Animator

    public GameObject RestartButton;

    // Health properties
    public int maxHealth = 30;
    private int currentHealth;

    // UI Text to display health
    public Text healthText;

    private Vector3 respawnPoint;

    // Pause menu UI
    public GameObject pauseMenu;  // Assign the PausePanel GameObject here
    private bool isPaused = false;

    // Enum for Player Animation States
    enum PlayerState
    {
        Idle = 0,
        Running = 1,
        Jumping = 2
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Get the Animator component
        respawnPoint = transform.position;

        // Initialize health
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    void Update()
    {
        if (!isPaused) // Only allow movement if the game is not paused
        {
            Move();
            Jump();
        }

        // Check for pause button press (Escape key or P)
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        // Move the player based on input
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Flip character direction if moving left or right
        if (moveInput > 0)
            transform.localScale = new Vector3(0.18f, 0.18f, 0.18f); // Face right
        else if (moveInput < 0)
            transform.localScale = new Vector3(-0.18f, 0.18f, 0.18f); // Face left

        // Update the animation state based on movement
        UpdateAnimationState(moveInput);
    }

    void Jump()
    {
        // Jump if the player presses the up arrow and is grounded
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Apply jump force
            isGrounded = false; // Set isGrounded to false to prevent double-jumping
            animator.SetInteger("AnimationState", (int)PlayerState.Jumping); // Set to Jumping animation
        }
    }

    private void UpdateAnimationState(float moveInput)
    {
        PlayerState state;

        // Determine the player's state based on movement
        if (!isGrounded)
        {
            state = PlayerState.Jumping;
        }
        else if (Mathf.Abs(moveInput) > 0.1f)
        {
            state = PlayerState.Running;
        }
        else
        {
            state = PlayerState.Idle;
        }

        // Set the animator parameter to switch animations
        animator.SetInteger("AnimationState", (int)state);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Reset isGrounded when touching the ground
            animator.SetInteger("AnimationState", (int)PlayerState.Idle); // Set back to Idle when grounded
        }
        else if (collision.gameObject.CompareTag("Spike"))
        {
            TakeDamage(10);  // Reduce health by 10 on spike collision
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FallDetector"))
        {
            transform.position = respawnPoint; // Respawn the player
        }
    }

    // Method to handle taking damage
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    // Handle game-over functionality
    void GameOver()
    {
        Debug.Log("Game Over");
        healthText.text = "Game Over";
        moveSpeed = 0; // Stop player movement
        jumpForce = 0; // Stop jumping
        animator.SetInteger("AnimationState", (int)PlayerState.Idle); // Set to Idle animation
    }

    // Update the UI to show current health
    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString();
        }
    }

    // Restart the game
    public void RestartGame()
    {
        Time.timeScale = 1f; // Make sure to reset time scale
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Pause and resume game
    public void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1f; // Resume game
            pauseMenu.SetActive(false);
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0f; // Pause game
            pauseMenu.SetActive(true);
            isPaused = true;
        }
    }

    public void ResumeGame()
    {
        // This can be used as a separate button function to resume
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        isPaused = false;
    }
}
