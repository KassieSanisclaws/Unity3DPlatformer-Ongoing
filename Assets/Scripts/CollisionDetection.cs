using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CollisionDetection : MonoBehaviour
{
     void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            Debug.Log("Player collided with capsule");
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                Debug.Log("Respawning player");
                playerController.Respawn();
            }
            else
            {
                Debug.Log("PlayerController component not found");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            Debug.Log("Player is colliding with capsule");
        }
    }
}
