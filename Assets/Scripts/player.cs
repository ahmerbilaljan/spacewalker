using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        // Move the player to the right when the right arrow key is pressed
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
    }

       void MoveLeft()
    {
        // Move the player to the right when the right arrow key is pressed
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("left key pressed");
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
    }

    void Jump()
    {
        // Jump when the up arrow key is pressed, but only if the player is grounded
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false; // Prevent jumping again until the player lands
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
