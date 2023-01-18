using UnityEngine;

public class ChangeLanguage : MonoBehaviour
{
    [SerializeField] private AudioClip clip;

    /*public void ChangeLanguageToEn()
    {
        PlayerPrefs.SetString("Language", "English");
        Strings.Instance.OnLanguageChange();
        SoundManager.Instance.PlaySound(clip);
    }

    public void ChangeLanguageToRu()
    {
        PlayerPrefs.SetString("Language", "Russian");
        Strings.Instance.OnLanguageChange();
        SoundManager.Instance.PlaySound(clip);
    }*/

    public void ChangeLanguageTo(string language)
    {
        PlayerPrefs.SetString("Language", language);
        Strings.Instance.OnLanguageChange();
        SoundManager.Instance.PlaySound(clip);
    }
}
