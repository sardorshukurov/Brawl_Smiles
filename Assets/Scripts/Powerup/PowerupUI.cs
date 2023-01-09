using TMPro;
using UnityEngine;

public class PowerupUI : MonoBehaviour
{
    public float powerup1, powerup2;

    [SerializeField] private TextMeshProUGUI powerup1Text, powerup2Text;
    [SerializeField] private GameObject powerup1GameObject, powerup2GameObject;

    private string powerup1String;
    private string powerup2String; 

    private void Start()
    {
        powerup1 = 0;
        powerup2 = 0;
        powerup1Text.text = powerup1String;
        powerup2Text.text = powerup2String;
    }

    private void Update()
    {
        powerup1String = Strings.Instance.GetString(9);
        powerup2String = Strings.Instance.GetString(8);

        if (powerup1 > 0)
        {
            powerup1GameObject.SetActive(true);
            powerup2GameObject.SetActive(true);
            powerup1Text.text = powerup1String + Mathf.Round(powerup1);
            powerup1 -= Time.deltaTime;
        }
        else if(powerup2 > 0)
        {
            powerup1GameObject.SetActive(true);
            powerup2GameObject.SetActive(true);
            powerup2Text.text = powerup2String + Mathf.Round(powerup2);
            powerup2 -= Time.deltaTime;
        }
        else
        {
            powerup1GameObject.SetActive(false);
            powerup2GameObject.SetActive(false);
        }
    }

    public void UpdatePowerupCountdown(int powerupNumber, float powerupCountdown)
    {
        if (powerupNumber == 0)
        {
            powerup1 += powerupCountdown;
        }
        else if (powerupNumber == 1)
        {
            powerup2 += powerupCountdown;
        }
    }
}
