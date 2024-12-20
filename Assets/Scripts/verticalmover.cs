// using UnityEngine;

// public class VerticalMover : MonoBehaviour
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
//         float newY = startPos.y + Mathf.PingPong(Time.time * speed, height) - height / 2;
        
//         // Update the box's position
//         transform.position = new Vector3(transform.position.x, newY, transform.position.z);
//     }
// }
using UnityEngine;

public class VerticalMover : MonoBehaviour
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
        float newY = startPos.y + Mathf.Sin(Time.time * speed) * distance / 2;

        // Update the object's position
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
