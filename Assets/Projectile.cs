using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 5f; 
    private GameObject shooter; 

    void Start()
    {
        Destroy(gameObject, lifeTime); 
    }

    public void SetShooter(GameObject shooter)
    {
        this.shooter = shooter;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != shooter)
        {            
            Destroy(gameObject);
        }
    }
}
