using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    private int damageMultiplyer = 1;
    private PlayerMovement player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();    
    }

    private void Start()
    {
        Destroy(gameObject, 1f);
    }
    private void Update()
    {
        if (player.hasPowerups[0])
        {
            damageMultiplyer = 4;
        }
        if (!player.hasPowerups[0])
        {
            damageMultiplyer = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && player.hasPowerups[1])
        {
            collision.gameObject.GetComponent<Enemy>().RestrictMovement();
            collision.gameObject.GetComponent<Enemy>().ReduceHealth(damageMultiplyer);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy") && !player.hasPowerups[1])
        {
            collision.gameObject.GetComponent<Enemy>().ReduceHealth(damageMultiplyer);
            Destroy(gameObject);
        }
    }
}
