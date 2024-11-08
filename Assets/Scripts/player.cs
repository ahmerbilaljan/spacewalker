using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool isGrounded = true;

    public GameObject RestartButton;

    // Health properties
    public int maxHealth = 30;
    private int currentHealth;

    // Optional: UI Text to display health
    public Text healthText;

    private Vector3 respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;

        // Initialize health
        currentHealth = maxHealth;
        
        UpdateHealthUI();
    }

    // Update is called once per frame
    void Update()
    {
        MoveRight();
        MoveLeft();
        Jump();
    }

    void MoveRight()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
    }

    void MoveLeft()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
        {
            isGrounded = true; 
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
            transform.position = respawnPoint;
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
        jumpForce = 0; }

    // Update the UI to show current health (optional)
    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString();
        }
    }
      public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
