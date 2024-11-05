using UnityEngine;

public class collectable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player touched the diamond!");
            GameManager.instance.IncrementScore();
            Destroy(gameObject);
        }
    }
}
