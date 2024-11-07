using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float offset;
    public float offsetSmoothing;
    private Vector3 playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //getting player's x postion
        playerPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

        if(player.transform.localScale.x > 0f) 
        //cehcking where player is facing
        //local scale represents scaling across speicific axis
        //a negative local scale value means player is flipped, facing left

        {
            playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y, playerPosition.z);
        }
        else
        {
            playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y, playerPosition.z);
        }
        //.lerp in linear interpolation to make the cam movement smooth
        //start is camera's current position
        //end is playerPosition
        //time is product of speed factor that controls how quickly the camera goes up to the player position
        //and time elapsed since the last frame
        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
    }
}