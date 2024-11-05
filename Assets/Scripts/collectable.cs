using UnityEngine;

public class collectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.IncrementScore();
            Destroy(gameObject);
        }
    }
}
