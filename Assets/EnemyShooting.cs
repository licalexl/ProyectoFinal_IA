using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform player; // Referencia al Transform del jugador
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde se disparará el proyectil
    public float shootingRange = 10f; // Rango de disparo
    public float projectileSpeed = 10f; // Velocidad del proyectil
    public float fireRate = 1f; // Cadencia de disparo
    private float nextFireTime = 0f; // Tiempo para el próximo disparo

    void Update()
    {
        if (player != null && Time.time >= nextFireTime)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= shootingRange)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = (player.position - firePoint.position).normalized * projectileSpeed;
        Projectile projScript = projectile.GetComponent<Projectile>();
        if (projScript != null)
        {
            projScript.SetShooter(gameObject); // Pasamos referencia del objeto que dispara
        }
    }
}
