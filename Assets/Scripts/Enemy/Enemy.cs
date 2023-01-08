using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int health = 100;
    private int damageIntake = 25;

    private Tower tower;
    private SpawnManager spawnManager;

    [SerializeField] private float speed = 10f;
    [SerializeField] private ParticleSystem bloodParticle;
    [SerializeField] private AudioClip deathSound;

    private bool canMove = true;

    private void Awake()
    {
        tower = GameObject.Find("Tower").GetComponent<Tower>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    private void Start()
    {
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    private void Update()
    {
        if (health <= 0)
        {
            Death();
            spawnManager.SpawnCoin(gameObject.transform);
            GameManager.Instance.EnemyKilled();
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, tower.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tower"))
        {
            tower.ReduceHealth();
            Death();
        }
    }

    private void Death()
    {
        Instantiate(bloodParticle, transform.position, bloodParticle.transform.rotation);
        Destroy(gameObject);
        SoundManager.Instance.PlaySound(deathSound); 
    }

    public void ReduceHealth(int damageMultiplyer)
    {
        health -= damageIntake * damageMultiplyer;
    }

    public void RestrictMovement()
    {
        StartCoroutine(MovementRestrictionCoundown());
    }

    private IEnumerator MovementRestrictionCoundown()
    {
        canMove = false;
        yield return new WaitForSeconds(5);
        canMove = true;
    }
}
