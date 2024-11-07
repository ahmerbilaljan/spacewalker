// using UnityEngine;

// public class HorizontalMover : MonoBehaviour
// {
//     public float speed = 2f;        // Speed of movement
//     public float height = 2f;       // Height of movement

//     private Vector3 startPos;

//     void Start()
//     {
//         // Record the initial position of the box
//         startPos = transform.position;
//     }

//     void Update()
//     {
//         // Calculate the new Y position using PingPong to create an oscillating effect
//         float newX = startPos.x + Mathf.PingPong(Time.time * speed, height) - height / 2;
        
//         // Update the box's position
//         transform.position = new Vector3(newX, transform.position.y, transform.position.z);
//     }
// }
using UnityEngine;

public class HorizontalMover : MonoBehaviour
{
    public float speed = 2f;    // Speed of movement
    public float distance = 2f; // Distance for back-and-forth movement

    private Vector3 startPos;

    void Start()
    {
        // Record the initial position of the object
        startPos = transform.position;
    }

    void Update()
    {
        // Calculate the new X position using Sin for a smooth oscillation
        float newX = startPos.x + Mathf.Sin(Time.time * speed) * distance / 2;

        // Update the object's position
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
