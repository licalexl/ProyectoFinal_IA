using UnityEngine;

public class RotateTowardsPlayer : MonoBehaviour
{
    public Transform player; 
    public float rotationSpeed = 5f; 

    void Update()
    {
        if (player != null)
        {
          
            Vector3 direction = player.position - transform.position;
            direction.y = 0; 

            // Rotación hacia la dirección
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
