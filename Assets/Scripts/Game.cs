using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Game : MonoBehaviour
{
    // Скрипт для объявления чисел рекорда
    [SerializeField] long Score;
    public long[] CostInt;
    private long ClickScore = 1;
    private long ClickBonus = 0;
    public long[] CostBonus;
    // Скрипт для создания магазинов
    public GameObject ShopPan;
    public GameObject BonusPan;
    public GameObject SettingsPan;
    public GameObject AchievementsPan;
    


    public Text[] CostText;
    public Text[] CostBonusText;
    public Text ScoreText;
    public Text BalanceShop;
    public Text BalanceBonus;


    private Save sv = new Save();

    // Скрипт для достижении
    [Header("Достижения")]
    private int Achievement1Max;

    private bool isAcievement1 = true;
    private bool isAcievement2 = true;
    private bool isAcievement3 = true;
    private bool isAchievement2Get = false;
    private bool isAchievement3Get = false;

    public Text[] AchievementsText;
    public Text[] AchievementsCost;
    public Text Achievement1NameText;
    public Text Achievement2NameText;
    public Text Achievement3NameText;
    // Скрипт для преобразования больших чисел
    public string GetSuffixValue(float value)
    {
        int zero = 0;
        string suffix = "";

        if (value >= 1000000000000000000)
        {
            zero = 6;
            value /= 1000000000000000000;
        }
        else if (value >= 1000000000000000)
        {
            zero = 5;
            value /= 1000000000000000;
        }
        else if (value >= 1000000000000)
        {
            zero = 4;
            value /= 1000000000000;
        }
        else if (value >= 1000000000)
        {
            zero = 3;
            value /= 1000000000;
        }
        else if (value >= 1000000)
        {
            zero = 2;
            value /= 1000000;
        }
        else if (value >= 100000)
        {
            zero = 1;
            value /= 1000;
        }

        switch (zero)
        {
            case 1: suffix = "K"; break;
            case 2: suffix = "M"; break;
            case 3: suffix = "B"; break;
            case 4: suffix = "T"; break;
            case 5: suffix = "Qd"; break;
            case 6: suffix = "Qb"; break;
        }

        return $"{value:0.##}{suffix}";
    }


    

    // Скрипт для проверки сохранения при запуске игры
    private void Awake()
    {
        if (PlayerPrefs.HasKey("SV"))
        {
            sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("SV"));
            Score = sv.Score;
            ClickScore = sv.ClickScore;
            Achievement1Max = sv.Achievement1Max;
            isAcievement1 = sv.isAcievement1;
            isAcievement2 = sv.isAcievement2;
            isAcievement3 = sv.isAcievement3;
            ClickBonus = sv.ClickBonus;
            sv.isAchievement2Get = isAchievement2Get;
            sv.isAchievement3Get = isAchievement3Get;


            isAchievement2Get = sv.isAchievement2Get;


            for (long i = 0; i < 1; i++)
            {
                CostBonusText[i].text = GetSuffixValue(sv.CostBonus[i]) + "$";
                CostBonus[i] = sv.CostBonus[i];
               
            }

            for (long i = 0; i < 3; i++)
            {
                CostInt[i] = sv.CostInt[i];
                CostText[i].text = GetSuffixValue(sv.CostInt[i]) + "$";

            }
        }
    }

    private void Start()
    {
        StartCoroutine(BonusShop());


        DateTime dt = new DateTime(sv.Date[0], sv.Date[1], sv.Date[2], sv.Date[3], sv.Date[4], sv.Date[5]);
        TimeSpan ts = DateTime.Now - dt;
        Score += (int)ts.TotalSeconds * ClickBonus;
        print("Вы заработали " + (int)ts.TotalSeconds * ClickBonus + "$");

    }

    // Скрипт нажатия на кнопку
    public void OnClickButton()
    {
       
        Score += ClickScore;

        if (isAcievement1 == true && Achievement1Max < 500)
        {
            Achievement1Max++;
        }

    }
    // Скрипт для проверки выполнения достижения и начисление награды
    public void OnClickAchievement1Button()
    {
        if(isAcievement1 == true && Achievement1Max == 500)
        {
            Score += 3000;
            isAcievement1 = false; 

        }
    }
    public void OnClickAchievement2Button()
    {
        if (isAcievement2 == false)
        {
            Score += 10000;
            isAcievement2 = false;
            isAchievement2Get = true;
        }
    }
    public void OnClickAchievement3Button()
    {
        if (isAcievement3 == false)
        {
            Score += 100000;
            isAcievement3 = false;
            isAchievement3Get = true;
        }
    }

    private void Update()
    {
        ScoreText.text = GetSuffixValue(Score) + " $";
        BalanceShop.text = GetSuffixValue(Score) + " $";
        BalanceBonus.text = GetSuffixValue(Score) + " $";

        Achievement1NameText.text = "Нажмите " + Achievement1Max + "/500 раз";

        if (isAcievement1 == false)
        {
            AchievementsCost[0].text = "Получено";
        }
        if (Achievement1Max == 500)
        {
            AchievementsText[0].text = "Выполнено";
        }

        if (isAchievement2Get == true)
        {
            AchievementsCost[1].text = "Получено";
        }

        if (isAcievement2 == false)
        {
            AchievementsText[1].text = "Выполнено";

        }

        if (isAchievement3Get == true)
        {
            AchievementsCost[2].text = "Получено";
        }

        if (isAcievement3 == false)
        {
            AchievementsText[2].text = "Выполнено";

        }
    }

    // Скрипт для открытия и закрытия магазина
    public void ShowAndHideShopPan()
    {
        ShopPan.SetActive(!ShopPan.activeSelf);
    }
    // Скрипт для открытия и закрытия бонус магазина
    public void ShowAndHideBonusPan()
    {
        BonusPan.SetActive(!BonusPan.activeSelf);
    }
    // Скрипт для открытия и закрытия настроек
    public void ShowAndHideSettingsPan()
    {
        SettingsPan.SetActive(!SettingsPan.activeSelf);
    }
    // Скрипт для открытия и закрытия достижений
    public void ShowAndHideCupPan()
    {
        AchievementsPan.SetActive(!AchievementsPan.activeSelf);
    }
    // Скрипт для кнопок покупок в магазине
    public void OnClickBuyLevel()
    {
        if (Score >= CostInt[0])
        {
            Score -= CostInt[0];
            CostInt[0] *= 2;
            ClickScore *= 2;
            CostText[0].text = GetSuffixValue(CostInt[0]) + "$";
            isAcievement2 = false;
        }
    }

    public void OnClickBuyLevel2()
    {
        if (Score >= CostInt[1])
        {
            Score -= CostInt[1];
            CostInt[1] *= 3;
            ClickScore *= 3;
            CostText[1].text = GetSuffixValue(CostInt[1]) + "$";
            isAcievement2 = false;
        }
    }

    public void OnClickBuyLevel3()
    {
        if (Score >= CostInt[2])
        {
            Score -= CostInt[2];
            CostInt[2] *= 4;
            ClickScore *= 4;
            CostText[2].text = GetSuffixValue(CostInt[2]) + "$";
            isAcievement2 = false;                                 
        }
    }

    // Скрипт покупки товара в бонус магазине
    public void OnClickBuyBonusShop()
    {
        if (Score >= CostBonus[0])
        {
            Score -= CostBonus[0];
            CostBonus[0] *= 2;
            ClickBonus += 2;
            CostBonusText[0].text = GetSuffixValue(CostBonus[0]) + "$";

            
        }
    }
    public void OnClickBuyBonusShop2()
    {
        if (Score >= CostBonus[1])
        {
            Score -= CostBonus[1];
            CostBonus[1] *= 4;
            ClickBonus += 4;
            CostBonusText[1].text = GetSuffixValue(CostBonus[1]) + "$";
        }
    }
    public void OnClickBuyBonusShop3()
    {
        if (Score >= CostBonus[2])
        {
            Score -= CostBonus[2];
            CostBonus[2] *= 8;
            ClickBonus += 8;
            CostBonusText[2].text = GetSuffixValue(CostBonus[2]) + "$";
            isAcievement3 = false;
        }
    }
    

    IEnumerator BonusShop()
    {
        while (true)
        {
            Score += ClickBonus;
            yield return new WaitForSeconds(1);
        }
    }
    // Скрипт для сохранения при выходе из игры
#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            sv.Score = Score;
            sv.ClickScore = ClickScore;
            sv.CostBonus = new long[1];
            sv.CostInt = new long[3];
            sv.Achievement1Max = Achievement1Max;
            sv.isAcievement1 = isAcievement1;
            sv.isAchievement2Get = isAchievement2Get;
            sv.isAcievement2 = isAcievement2;
            sv.isAchievement3Get = isAchievement3Get;
            sv.isAcievement3 = isAcievement3;
        
            sv.ClickBonus = ClickBonus;

            



            for (long i = 0; i < 1; i++)
            {
                sv.CostBonus[i] = CostBonus[i];
            }

            for (long i = 0; i < 3; i++)
            {
                sv.CostInt[i] = CostInt[i];
            }

            sv.Date[0] = DateTime.Now.Year; sv.Date[1] = DateTime.Now.Month; sv.Date[2] = DateTime.Now.Day; sv.Date[3] = DateTime.Now.Hour; sv.Date[4] = DateTime.Now.Minute; sv.Date[5] = DateTime.Now.Second;


            PlayerPrefs.SetString("SV", JsonUtility.ToJson(sv));
        }
    }


#else
    private void OnApplicationQuit()
    {
        sv.Score = Score;
        sv.ClickScore = ClickScore;
        sv.CostBonus = new long[1];
        sv.CostInt = new long[3];
        sv.Achievement1Max = Achievement1Max;
        sv.isAcievement1 = isAcievement1;
        sv.isAcievement2 = isAcievement2;
        sv.isAcievement3 = isAcievement3;
        sv.isAchievement2Get = isAchievement2Get;
        sv.isAcievement2 = isAcievement2;
        sv.isAchievement3Get = isAcievement3;
        sv.isAcievement3 = isAcievement3;

        sv.ClickBonus = ClickBonus;


        for (long i = 0; i < 1; i++)
        {
            sv.CostBonus[i] = CostBonus[i];
        }

        for (long i = 0; i < 3; i++)
        {
            sv.CostInt[i] = CostInt[i];
        }

        sv.Date[0] = DateTime.Now.Year; sv.Date[1] = DateTime.Now.Month; sv.Date[2] = DateTime.Now.Day; sv.Date[3] = DateTime.Now.Hour; sv.Date[4] = DateTime.Now.Minute; sv.Date[5] = DateTime.Now.Second;


        PlayerPrefs.SetString("SV", JsonUtility.ToJson(sv));
    }
#endif
}
    // Скрипт для сохранения прогресса рекорда,бонусов.
    [Serializable]
    public class Save
    {
        public long ClickBonus;
        public long Score;
        public long ClickScore;
        public long[] CostInt;
        public long[] CostBonus;
        public int[] Date = new int[6];
        
        
        public int Achievement1Max;
        public bool isAcievement1;
        public bool isAchievement2Get;
        public bool isAcievement2;
        public bool isAchievement3Get;
        public bool isAcievement3;
        

}       


  




