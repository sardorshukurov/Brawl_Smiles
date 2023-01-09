using UnityEngine;

public class ChangeLanguage : MonoBehaviour
{
    [SerializeField] private AudioClip clip;

    public void ChangeLanguageToEn()
    {
        PlayerPrefs.SetString("Language", "English");
        SoundManager.Instance.PlaySound(clip);
    }

    public void ChangeLanguageToRu()
    {
        PlayerPrefs.SetString("Language", "Russian");
        SoundManager.Instance.PlaySound(clip);
    }
}
