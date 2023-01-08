using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static Sprite equippedSkin;
    [SerializeField] private SSkinInfo[] allSkins;

    private void Awake()
    {
        CheckEquipedSkin();
    }

    public void EquipSkin(SSkinInfo skinInfo)
    {
        equippedSkin = skinInfo.skinSprite;
        PlayerPrefs.SetString("skinPref", skinInfo.skinID.ToString());
    }

    private void CheckEquipedSkin()
    {
        string lastSkinUsed = PlayerPrefs.GetString("skinPref", SSkinInfo.SkinIDs.Default.ToString());
        foreach (SSkinInfo skinInfo in allSkins)
        {
            if (skinInfo.skinID.ToString() == lastSkinUsed)
            {
                EquipSkin(skinInfo);
            }
        }
    }
}
