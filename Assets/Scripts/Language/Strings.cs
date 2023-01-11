using System.Collections.Generic;
using UnityEngine;

public class Strings : MonoBehaviour
{
    public static Strings Instance;

    // Main Menu Scene
    public string menuPlayButton;
    public string menuSettingsButton;
    public string menuQuitButton;
    public string changeSkinButton;
    public string backButton;
    public string buyButton;
    public string equipButton;
    public string equipedText;

    // Gameplay Scene
    public string freezeText;
    public string increasedDamageText;
    public string gameOverText;
    public string recordsText;
    public string pausedText;

    public string rateText;

    private List<string> stringList = new List<string>();
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

    private void Update()
    {
        if (PlayerPrefs.GetString("Language", "English") == "English")
        {
            menuPlayButton = "Play";
            menuSettingsButton = "Settings";
            menuQuitButton = "Quit";
            changeSkinButton = "Change Skin";
            backButton = "Back";
            buyButton = "Buy";
            equipButton = "Equip";
            equipedText = "Equiped";

            freezeText = "Freeze: ";
            increasedDamageText = "Increased Damage: ";
            gameOverText = "Game Over";
            recordsText = "Records:";
            pausedText = "Paused";

            rateText = "Rate Game";
        }
        else if (PlayerPrefs.GetString("Language", "English") == "Russian")
        {
            menuPlayButton = "Играть";
            menuSettingsButton = "Настройки";
            menuQuitButton = "Выйти";
            changeSkinButton = "Поменять Скин";
            backButton = "Назад";
            buyButton = "Купить";
            equipButton = "Надеть";
            equipedText = "Надето";

            freezeText = "Заморозка: ";
            increasedDamageText = "Дополнительный Урон: ";
            gameOverText = "Конец Игры";
            recordsText = "Рекорды:";
            pausedText = "Пауза";

            rateText = "Оценить Игру";
        }

        AddAllStringsToList();
    }

    private void AddAllStringsToList()
    {
        stringList.Clear();
        stringList.Add(menuPlayButton); // 0
        stringList.Add(menuSettingsButton); // 1
        stringList.Add(menuQuitButton); // 2
        stringList.Add(changeSkinButton); // 3
        stringList.Add(backButton); // 4
        stringList.Add(buyButton); // 5
        stringList.Add(equipButton); // 6
        stringList.Add(equipedText); // 7
        stringList.Add(freezeText); // 8
        stringList.Add(increasedDamageText); // 9
        stringList.Add(gameOverText); // 10
        stringList.Add(recordsText); // 11
        stringList.Add(pausedText); // 12
        stringList.Add(rateText); // 13
    }

    public string GetString(int stringID)
    {
        return stringList[stringID];
    }
}
