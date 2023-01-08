using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "New Skin", menuName = "Create New Skin")]
public class SSkinInfo : ScriptableObject
{
    public enum SkinIDs { Default, Blue, Pirate, Prince, Bat }
    public SkinIDs skinID;
    public Sprite skinSprite;
    public int skinPrice;
}
