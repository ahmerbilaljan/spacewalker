using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool isGrounded = true;
    private Animator animator;
    private Vector3 originalScale; // Store the original scale for flipping
    private Camera mainCamera;

    enum PlayerState
    {
        Idle = 0,
        Running = 1,
        Jumping = 2
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Set the sprite size to 0.18 for X, Y, and Z and store it for flipping
        transform.localScale = new Vector3(0.18f, 0.18f, 0.18f);
        originalScale = transform.localScale;
        
        // Reference to the main camera to check bounds
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        UpdateAnimationState();
        CheckOutOfBounds(); // Check if the player goes out of canvas
    }

    void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        // Move the player based on input
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Flip character direction if moving left or right
        if (moveInput > 0)
            transform.localScale = originalScale; // Face right
        else if (moveInput < 0)
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z); // Face left
    }

    void Jump()
    {
        // Jump if the player presses the up arrow and is grounded
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false; // Set isGrounded to false to prevent double-jumping
        }
    }

    private void UpdateAnimationState()
    {
        PlayerState state;

        // Determine the player's state based on movement
        if (!isGrounded)
        {
            state = PlayerState.Jumping;
        }
        else if (Mathf.Abs(rb.velocity.x) > 0.1f)
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

    private void CheckOutOfBounds()
    {
        // Get the screen bounds based on the main camera
        Vector3 screenPosition = mainCamera.WorldToViewportPoint(transform.position);

        // Check if the player is outside the bounds (out of the canvas)
        if (screenPosition.x < 0 || screenPosition.x > 1 || screenPosition.y < 0 || screenPosition.y > 1)
        {
            Debug.Log("Player went out of bounds!");
            Destroy(gameObject); // Destroy the player object
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
            Debug.Log("Player collided with spike!"); 
            Destroy(gameObject); 
        }
    }
}
