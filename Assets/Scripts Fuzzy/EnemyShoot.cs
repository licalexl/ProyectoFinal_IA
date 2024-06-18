using System.Collections;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform player;
    public float shootingRange = 10f;
    public float bulletSpeed = 5f;
    public float bulletLifetime = 10f;
    public float timeBetweenShots = 1f;
    public int maxBullets = 10; 
    public int currentBullets; 
    private float timeSinceLastShot = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentBullets = maxBullets; 
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (Vector3.Distance(transform.position, player.position) <= shootingRange && timeSinceLastShot >= timeBetweenShots && currentBullets > 0)
        {
            ShootAtPlayer();
            timeSinceLastShot = 0f;
            currentBullets--; 
        }
    }

    void ShootAtPlayer()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector3 direction = (player.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
        Destroy(bullet, bulletLifetime);
    }

    public void Recarga() 
    {        
       currentBullets = maxBullets;       
    }

    void OnTriggerEnter(Collider other)
    {

    }
}
