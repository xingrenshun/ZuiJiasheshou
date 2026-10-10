using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class StartGame : MonoBehaviour
{
    public GameObject WinMenu;
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Level;
    public TextMeshProUGUI Coin;
    public TextMeshProUGUI MainMenuCoin;
    private Manage Manage;
    public GameObject Again,Chance;
    public GameObject []Items;
    public GameObject IntenseShootingPosition;
    public GameObject Shield;


    public GameObject[] Menu;


    public TextMeshProUGUI CoinScore;
    public TextMeshProUGUI ThreeShotScore;
    public TextMeshProUGUI LifeScore;
    public TextMeshProUGUI ShieldScore;
    public TextMeshProUGUI IntenseShootingScore;
    public Sprite []SoundSprite;
    public Image Sound;

    private AudioSource BuyAudio;

    public Sprite[] LevelBG;
    public Image LevelBg;
    public GameObject[] Background;
    public GameObject[] GameplayBackground;
    public GameObject particle;


    public GameObject StartMenu,ExitPanel,WatchVideoPanel,OfferPanel;
    public GameObject Map;
    public Button[] _RemoveAds;
    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        if (Input.GetKeyDown(KeyCode.Escape)) { Exit(); }

        BuyAudio = GameObject.Find("Buy-Audio").GetComponent<AudioSource>();
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
        Background[0].SetActive(true);
        //if (Manage.Level >= 0 && Manage.Level <= 10)
        //{
        //    Background[0].SetActive(true);
        //}
        //else if (Manage.Level >= 11 && Manage.Level <= 20)
        //{
        //    Background[1].SetActive(true);
        //}
        //else if (Manage.Level >= 21 && Manage.Level <= 30)
        //{
        //    Background[2].SetActive(true);
        //}
        //else if (Manage.Level > 30)
        //{
        //    Background[3].SetActive(true);
        //}
        //if (Manage.Level >= 40)
        //{
        //    Manage.Level = 40;
        //}
        if (Manage.Level >= 10)
        {
            Manage.Level = 10;
        }
        Manage.Win = false;
        Manage.ShowCoin = Coin;
        Manage.MainMenuCoinn = MainMenuCoin;
        Manage.Enemy = 0;
        Manage.WinMenu = WinMenu;
        Manage.Shoting = false;
        Manage.Lose = false;
        Manage.AgainMenu = Again;
        Manage.ChancePanel = Chance;
        Manage.Score = 0;
       
        Manage.ShowScore = Score;
        Manage.ShowLevel  = Level;
        Manage.ShowScores();
        Manage.Three=false;
        Manage.hit = false;
        Manage.Life = 100;
        Manage.Shield = Shield;
        Manage.IntenseShootingPosition = IntenseShootingPosition;
        ShowScore();
        ShowSoundImage();
        LevelBg.sprite = LevelBG[Manage.Level - 1];

        if (Manage.ResumeGame == true)
        {
            if (Manage.SelectLevel == false)
            {
                StartMenu.SetActive(false);
                Map.SetActive(true);
                StartCoroutine(ShowMap());
            }
         else   if (Manage.SelectLevel == true)
            {
                Manage.ShowScores();
                Items[0].SetActive(true);
                Items[1].SetActive(true);
                Items[2].SetActive(true);
                Items[3].SetActive(true);
                Items[4].SetActive(true);
                Items[5].SetActive(true);
                Items[6].SetActive(true);
                // Destroy(gameObject);
                gameObject.SetActive(false);
            }
        }
    }
    void Update()
    {
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.GetInt("RemoveAds")==1)
        {
            _RemoveAds[0].interactable = false;
        }

        if (PlayerPrefs.GetInt("rewardvdo") == 1)
        {
            if (PlayerPrefs.GetInt("WV", 1) == 1)
            {
                Manage.Coin += 5;
                Manage.SaveScore();
                Manage.ShowScores();
                PlayerPrefs.SetInt("rewardvdo", 0);
                PlayerPrefs.SetInt("WV", 0);
            }
        }
        else if (PlayerPrefs.GetInt("rewardvdo", 1) == 2)
        {
            //Video Failed
            PlayerPrefs.SetInt("rewardvdo", 0);
        }
    }
    IEnumerator ShowMap()
    {
        yield return new WaitForSeconds(3f);

        Manage.ShowScores();
        Items[0].SetActive(true);
        Items[1].SetActive(true);
        Items[2].SetActive(true);
        Items[3].SetActive(true);
        Items[4].SetActive(true);
        Items[5].SetActive(true);
        Items[6].SetActive(true);
        Destroy(Map);
        //Destroy(gameObject);
        gameObject.SetActive(false);

        //gameplay bg active
        //int a = Random.Range(0, GameplayBackground.Length);
        //GameplayBackground[a].SetActive(true);
        particle.SetActive(true);
    }
    public void SoundButton()
    {
        Manage.Sound += 1;
        if (Manage.Sound >= 2)
        {
            Manage.Sound = 0;
        }
        ShowSoundImage();
    }
    // Update is called once per frame
   
    public void ShowSoundImage()
    {
        if(Manage.Sound==0)
        {
            Sound.GetComponent<Image>().sprite = SoundSprite[0];
            if (!GameObject.Find("Menu-Audio").GetComponent<AudioSource>().isPlaying)
            {
                GameObject.Find("Menu-Audio").GetComponent<AudioSource>().Play();
            }
           
        }
       else if (Manage.Sound == 1)
        {
            Sound.GetComponent<Image>().sprite = SoundSprite[1];
            GameObject.Find("Menu-Audio").GetComponent<AudioSource>().Stop();
        }

    }
    //public void MoreGame()
    //{
    //    Application.OpenURL("https://play.google.com/store/apps/developer?id=divesole");
    //}
    //public void Rateus()
    //{
    //    Application.OpenURL("https://play.google.com/store/apps/details?id=com.divesole.archeryhit.stickmanbowfight");
    //}
    //public void PrivacyPolicy()
    //{
    //    Application.OpenURL("");
    //}
    public void Exit()
    {
        ExitPanel.SetActive(true);
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            //AdsManager._instance.ShowUnityAds();
        }
    }
    public void ExitYes() {
        Application.Quit();
    }
    public void ExitNo()
    {
        ExitPanel.SetActive(false);
    }
    public void plus()
    {
        WatchVideoPanel.SetActive(true);
    }
    public void plusNo()
    {
        WatchVideoPanel.SetActive(false);
    }
    public void Offer()
    {
        OfferPanel.SetActive(true);
    }
    public void OfferNo()
    {
        OfferPanel.SetActive(false);
    }
    public void plusYes()
    {
#if UNITY_EDITOR
        Debug.Log("观看的广告");
        Manage.Coin += 50;
        Manage.SaveScore();
        Manage.ShowScores();
        WatchVideoPanel.SetActive(false);
#else
        GuangGao.Instance.ShowReward(() =>
        {
            Manage.Coin += 50;
            Manage.SaveScore();
            Manage.ShowScores();
            WatchVideoPanel.SetActive(false);
        });
#endif

    }
    public void WVfn()
    {
        Manage.Coin += 10;
        Manage.SaveScore();
        Manage.ShowScores();
        PlayerPrefs.SetInt("WV", 0);
    }
    public void PlayButton()
    {
        Manage.Level = Manage.Lock;
        if (Manage.Level >= 10)
        {
            Manage.Level = 10;
        }
        Manage.ShowScores();
        Items[0].SetActive(true);
        Items[1].SetActive(true);
        Items[2].SetActive(true);
        Items[3].SetActive(true);
        Items[4].SetActive(true);
        Items[5].SetActive(true);
        Items[6].SetActive(true);
        //Destroy(gameObject);
        gameObject.SetActive(false);

        //int a = Random.Range(0, GameplayBackground.Length);
        //GameplayBackground[a].SetActive(true);
        particle.SetActive(true);       
    }
    public void ShowScore()
    {
        CoinScore.text = "" + (int)Manage.Coin;

        ThreeShotScore.text = "" + (int)Manage.NumberOfThreeArrows;
        LifeScore.text = "" + (int)Manage.NumberOfLife;
        ShieldScore.text = "" + (int)Manage.NumberOfShield;
        IntenseShootingScore.text = "" + (int)Manage.NumberOfShootings;
    }
    public void ShopButton()
    {
        Menu[1].SetActive(true);
        Menu[2].SetActive(false);
        Menu[0].SetActive(false);
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            //int a = Random.Range(1, 3);
            //if (a == 1)
            //{
            //    AdsManager._instance.ShowUnityAds();
            //}
            //else
            //{
            //    AdsManager._instance.ShowUnityAds();
            //}
        }
    }
    public void LevelsButton()
    {
        Manage.Level = Manage.Lock;
        if (Manage.Level >= 10)
        {
            Manage.Level = 10;
        }
        Menu[2].SetActive(true);
        Menu[1].SetActive(false);
        Menu[0].SetActive(false);

        //if (Application.internetReachability != NetworkReachability.NotReachable)
        //{
        //    AdsManager._instance.ShowUnityAds();
        //}
    }
    public void ExitMenu()
    {
        Menu[0].SetActive(true);
        Menu[2].SetActive(false);
        Menu[1].SetActive(false);
      
    }


    public void BuyShield()
    {
        if (Manage.Coin >= 100)
        {
            if (Manage.Sound == 0)
            {
                BuyAudio.Play();
            }
            Manage.NumberOfShield += 1;
        
            Manage.Coin -= 100;
            ShowScore();
        }
        Manage.SaveScore();
    }
    public void BuyLife()
    {
        if (Manage.Coin >= 100)
        {
            if (Manage.Sound == 0)
            {
                BuyAudio.Play();
            }
            Manage.NumberOfLife += 1;
         
            Manage.Coin -= 100;
            ShowScore();
        }
        Manage.SaveScore();
    }
    public void BuyIntenseShooting()
    {
        if (Manage.Coin >= 100)
        {
            if (Manage.Sound == 0)
            {
                BuyAudio.Play();
            }
            Manage.NumberOfShootings += 1;
           
            Manage.Coin -= 100;
            ShowScore();
        }
        Manage.SaveScore();
    }

    public void ByThreeShot()
    {
        if (Manage.Coin >= 100)
        {
            if (Manage.Sound == 0)
            {
                BuyAudio.Play();
            }
            Manage.NumberOfThreeArrows += 1;

            Manage.Coin -= 100;
            ShowScore();
        }
        Manage.SaveScore();
    }
}
