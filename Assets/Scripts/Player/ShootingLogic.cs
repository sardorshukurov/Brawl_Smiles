using UnityEngine;

public class ShootingLogic : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileForce;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !GameManager.Instance.pause)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject projectile =Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * projectileForce, ForceMode2D.Impulse);
    }
}
