using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private Slider slider;
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private AudioClip explosionSound;

    private int healthReductionAmount = 5;
    private void Awake()
    {
        health = 100;
    }

    private void Update()
    {
        slider.value = health;

        if (health <= 0)
        {
            gameObject.SetActive(false);
            SoundManager.Instance.PlaySound(explosionSound);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            GameManager.Instance.GameOver();
        }
    }

    public void ReduceHealth()
    {
        health-= healthReductionAmount;
    }


}
