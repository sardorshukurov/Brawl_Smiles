using System.Runtime.InteropServices;
using UnityEngine;

public class Yandex : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [DllImport("__Internal")]
    private static extern void RateGame();


    public void RateGameButton()
    {
        SoundManager.Instance.PlaySound(clip);
        RateGame();
    }
}
