using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject[] powerupIndicators;
    [SerializeField] private AudioClip pickupSound;

    private Rigidbody2D rb;
    private PowerupUI powerupUI;

    [SerializeField] private float moveSpeed = 5.0f;

    private Vector2 movement;
    private Vector2 mousePos;

    public bool[] hasPowerups;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        powerupUI = GameObject.Find("Powerup UI").GetComponent<PowerupUI>();

        hasPowerups = new bool[powerupIndicators.Length];
        TurnOffPowerupIndicators();
        PowerupsToFalse();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            Destroy(collision.gameObject);
            PowerupLogic(0);
        }
        if (collision.gameObject.CompareTag("PowerUp2"))
        {
            Destroy(collision.gameObject);
            PowerupLogic(1);
        }
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            SoundManager.Instance.PlaySound(pickupSound);
            GameManager.Instance.AddCoin();
            //Debug.Log(PlayerPrefs.GetInt(GameManager.Instance.coinPref, 0));
        }
    }

    private IEnumerator PowerupCountdownRoutine(int powerupNumber)
    {
        powerupUI.UpdatePowerupCountdown(powerupNumber, 10f);
        yield return new WaitForSeconds(10);
        powerupIndicators[powerupNumber].SetActive(false);
        hasPowerups[powerupNumber] = false;
    }

    private void TurnOffPowerupIndicators()
    {
        foreach (GameObject powerupIndicator in powerupIndicators)
        {
            powerupIndicator.SetActive(false);
        }
    }

    private void PowerupsToFalse()
    {
        for (int i = 0; i < hasPowerups.Length; i++)
        {
            hasPowerups[i] = false;
        }
    }

    private void PowerupLogic(int powerupNumber)
    {
        hasPowerups[powerupNumber] = true;
        powerupIndicators[powerupNumber].SetActive(true);
        StartCoroutine(PowerupCountdownRoutine(powerupNumber));
        SoundManager.Instance.PlaySound(pickupSound);
    }
}
