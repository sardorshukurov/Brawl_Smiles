using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    
    [DllImport("__Internal")]    
    private static extern void ShowAdv();
    
    [DllImport("__Internal")]
    private static extern void AddCoins();
    
    [DllImport("__Internal")]
    private static extern void SetToLeaderboard_Enemies(int value);
    [DllImport("__Internal")]
    private static extern void SetToLeaderboard_Time(TimeSpan value);
    
    [SerializeField] private TextMeshProUGUI currentTimeText;
    private float currentTime;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI gameOverTimeText;
    [SerializeField] private TextMeshProUGUI gameOverEnemyKilledText;
    [SerializeField] private TextMeshProUGUI gameOverRecordTimeText;
    [SerializeField] private TextMeshProUGUI gameOverRecrodEnemyKilledText;
    public bool pause = false;

    [SerializeField] private TextMeshProUGUI currentEnemyKilledText;
    public int enemyKilledCount;

    /*
    [SerializeField] private GameObject firstPage;
    [SerializeField] private GameObject secondPage;
    */

    [SerializeField] private GameObject[] mainMenuPages;
    [SerializeField] private GameObject mainMenuTitle;

    [SerializeField] private AudioClip clip;

    [SerializeField] private GameObject pauseUI;

    [SerializeField] private TextMeshProUGUI mainMenuCoinText;
    private string recordTimePref = "RecordTime";
    private string recordKilledPref = "RecordKilled";
    public string coinPref = "Coin";
    
    [SerializeField] private GameObject rateButton;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        transform.parent = null;
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            ChangePage(0);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            currentTime = 0;
            enemyKilledCount = 0;

            gameOverRecordTimeText.text = PlayerPrefs.GetString(recordTimePref, "00:00");
            gameOverRecrodEnemyKilledText.text = PlayerPrefs.GetInt(recordKilledPref, 0).ToString();
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            mainMenuCoinText.text = PlayerPrefs.GetInt(coinPref, 0).ToString();
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            currentTime += Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            currentTimeText.text = time.ToString(@"mm\:ss");
            currentEnemyKilledText.text = enemyKilledCount.ToString();
            if (pause)
            {
                Time.timeScale = 0;
            }
            else if (!pause)
            {
                Time.timeScale = 1;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pause = !pause;
                pauseUI.SetActive(pause);
            }
            if (CompareTimeSpan(time, PlayerPrefs.GetString(recordTimePref, "00:00")))
            {
                PlayerPrefs.SetString(recordTimePref, time.ToString(@"mm\:ss"));
                SetToLeaderboard_Time(time);
            }
            //PlayerPrefs.SetString(recordTimePref, time.ToString(@"mm\:ss"));

            if (enemyKilledCount > PlayerPrefs.GetInt(recordKilledPref, 0))
            {
                PlayerPrefs.SetInt(recordKilledPref, enemyKilledCount);
                SetToLeaderboard_Enemies(PlayerPrefs.GetInt(recordKilledPref, 0));
            }
            gameOverRecordTimeText.text = PlayerPrefs.GetString(recordTimePref, "00:00");
            gameOverRecrodEnemyKilledText.text = PlayerPrefs.GetInt(recordKilledPref, 0).ToString();
        }
    }

    public void GameOver()
    {
        ShowAdv();
        StartCoroutine(GameOverCountDown());    
    }

    public void RestartGame()
    {
        SoundManager.Instance.PlaySound(clip);
        gameOverUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        pause = false;
    }

    private IEnumerator GameOverCountDown()
    {
        yield return new WaitForSeconds(1);
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        gameOverTimeText.text = time.ToString(@"mm\:ss");
        gameOverEnemyKilledText.text = enemyKilledCount.ToString();
        gameOverUI.SetActive(true);
        pause = true;
    }

    public void EnemyKilled()
    {
        enemyKilledCount++;
    }

    public void Play()
    {
        ShowAdv();
        SoundManager.Instance.PlaySound(clip);
        SceneManager.LoadScene(1);
    }

    public void Home()
    {
        ShowAdv();
        SoundManager.Instance.PlaySound(clip);
        SceneManager.LoadScene(0);
        pause = false;
    }

    public void Quit()
    {
        SoundManager.Instance.PlaySound(clip);
        Application.Quit();
    }

    public void ChangePage(int pageID)
    {
        SoundManager.Instance.PlaySound(clip);
        // firstPage.SetActive(!firstPage.activeInHierarchy);
        // secondPage.SetActive(!secondPage.activeInHierarchy);
        for (int i = 0; i < mainMenuPages.Length; i++)
        {
            if (mainMenuPages[i] != mainMenuPages[pageID])
            {
                mainMenuPages[i].SetActive(false);
            }
            else
            {
                mainMenuPages[i].SetActive(true);
            }
        }
        if (pageID == 2)
        {
            mainMenuTitle.SetActive(false);
        }
        else
        {
            mainMenuTitle.SetActive(true);
        }
    }

    private bool CompareTimeSpan(TimeSpan firstTimeSpan, string secondTimeSpan)
    {
        
        TimeSpan interval;
        if (TimeSpan.TryParseExact(secondTimeSpan, @"mm\:ss", null, TimeSpanStyles.AssumeNegative, out interval))
        {
            //Debug.Log(interval);
        }
        else
        {
            return false;
        }
        
        int result = TimeSpan.Compare(firstTimeSpan, -interval);

        if (result == 1)
        {
            return true;
        }return false;
    }

    public void AddCoin()
    {
        PlayerPrefs.SetInt(coinPref, PlayerPrefs.GetInt(coinPref, 0) + 1);
    }
    
    public void ShowAdvButton()
    {
        OnAdSeen();
        AddCoins();
    }

    public void AddCoinsForAd()
    {
        OnAdSeen();
        PlayerPrefs.SetInt(coinPref, PlayerPrefs.GetInt(coinPref, 0) + 100);
    }

    public void OnAdSeen()
    {
        SoundManager.Instance.ChangePauseStateOfMusic();
    }

    public void TurnOffRateButton()
    {
        rateButton.SetActive(false);
    }
}
