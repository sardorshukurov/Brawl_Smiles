using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinInShop : MonoBehaviour
{
    public SSkinInfo skinInfo;
    public Image skinImage;
    public bool isSkinUnlocked;

    public TextMeshProUGUI buttonText;

    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject price;

    private void Awake()
    {
        skinImage.sprite = skinInfo.skinSprite;
        IsSkinUnlocked();
        IsSkinEquiped();
    }

    private void Update()
    {
        IsSkinUnlocked();
        IsSkinEquiped();
        if (isSkinUnlocked)
        {
            panel.SetActive(false);
            price.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
            price.SetActive(true);
            price.GetComponent<TextMeshProUGUI>().text = skinInfo.skinPrice.ToString();
        }
    }
    public void OnButtonPress()
    {
        if (isSkinUnlocked)
        {
            FindObjectOfType<SkinManager>().EquipSkin(skinInfo); 
        }
        else
        {
            if (TryRemoveMoney(skinInfo.skinPrice))
            {
                isSkinUnlocked = true;
                PlayerPrefs.SetInt(skinInfo.skinID.ToString(), 1);
                buttonText.text = "Equip";
            }
        }
    }

    private bool TryRemoveMoney(int moneyToRemove)
    {
        if (moneyToRemove <= PlayerPrefs.GetInt(GameManager.Instance.coinPref, 0))
        {
            PlayerPrefs.SetInt(GameManager.Instance.coinPref, PlayerPrefs.GetInt(GameManager.Instance.coinPref, 0) - moneyToRemove);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void IsSkinUnlocked()
    {
        if (PlayerPrefs.GetInt(skinInfo.skinID.ToString(), 0) == 1 || skinInfo.skinPrice == 0)
        {
            isSkinUnlocked = true;
            buttonText.text = "Equip";
        }
    }

    private void IsSkinEquiped()
    {
        if (PlayerPrefs.GetString("skinPref", SSkinInfo.SkinIDs.Default.ToString()) == skinInfo.skinID.ToString())
        {
            buttonText.text = "Equiped";
        }
    }
}
