using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ButtonController : MonoBehaviour
{
    public GameObject GreenParticle;
    public GameObject BlueParticle;
    private Manage Manage;
    private bool Defensively;

    public Button Shield;
    public Button ThreeArrow;
    public Button IntenseShooting;
    public Button Life;

    private DragNShoot Player;

    private AudioSource LifeAudio;
    private AudioSource ShieldAudio;

    private Image ShieldImage;
    private Image ThreeArrowImage;
    private Image IntenseShootingImage;
    private Image LifeImage;
    private float Timer;
    private float IntenseShootingTimer;
    public Text ShieldText;
    public Text ThreeArrowText;
    public Text IntenseShootingText;
    public Text LifeText;

    public GameObject []DisplayTheNumberOfItems;

    public GameObject PausePanel, ChancePanel, GameOverPanel;
    public Button[] _RemoveAds;



    void Start()
    {
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
        LifeAudio = GameObject.Find("Life-Audio").GetComponent<AudioSource>();
        ShieldAudio = GameObject.Find("Shield-Audio2").GetComponent<AudioSource>();
        Player = GameObject.Find("BowController").GetComponent<DragNShoot>();
        ShieldImage = Shield.GetComponent<Image>();
        ThreeArrowImage = ThreeArrow.GetComponent<Image>();
        IntenseShootingImage = IntenseShooting.GetComponent<Image>();
        LifeImage = Life.GetComponent<Image>();

        ShowItems();

    }

    // Update is called once per frame
    void Update()
    {
        if (Manage.Lose == false && Manage.Win == false) { 
        if(Defensively==true)
        {
            Timer += 1 * Time.deltaTime;
            if (Timer >= 10)
            {
                Manage.NoDefense();
                Defensively = false;
            }
        }
        if (Manage.Shoting == true)
        {
            IntenseShootingTimer += 1 * Time.deltaTime;
            if (IntenseShootingTimer >= 10)
            {
                Manage.Shoting = false;
            }
        }
    }
        if (PlayerPrefs.GetInt("RemoveAds") == 1)
        {
            _RemoveAds[0].interactable = false;
        }

        if (PlayerPrefs.GetInt("rewardvdo") == 1)
        {
            if (PlayerPrefs.GetInt("Chance", 1) == 1)
            {
                Manage.Coin += 5;
                Manage.SaveScore();
                Manage.ShowScores();
                PlayerPrefs.SetInt("rewardvdo", 0);
                PlayerPrefs.SetInt("Chance", 0);
            }
        }
        else if (PlayerPrefs.GetInt("rewardvdo", 1) == 2)
        {
            //Video Failed
            PlayerPrefs.SetInt("rewardvdo", 0);
        }
    }
    public void ShieldButton()
    {
        if (Defensively == false && Manage.NumberOfShield > 0&&Manage.Lose==false && Manage.Win == false)
        {

            Timer = 0;
            Instantiate(BlueParticle, new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z), Quaternion.identity);
            Manage.defend();
            Manage.NumberOfShield -= 1;
            if (Manage.Sound == 0)
            {
                ShieldAudio.Play();
            }
            if (Manage.NumberOfShield <= 0)
            {
                Manage.NumberOfShield = 0;
            }
            ShowItems();
            Defensively = true;
        }
    }
    public void ShowItems()
    {
        ShieldText.text = "" + (int)Manage.NumberOfShield;
        ThreeArrowText.text = "" + (int)Manage.NumberOfThreeArrows;
        IntenseShootingText.text = "" + (int)Manage.NumberOfShootings;
        LifeText.text = "" + (int)Manage.NumberOfLife;

        if (Manage.NumberOfLife <= 0)
        {
            LifeImage.color = new Color32(255, 255, 255, 100);
            DisplayTheNumberOfItems[0].SetActive(false);
        }
        else 
        {
            LifeImage.color = new Color32(255, 255, 255, 255);
            DisplayTheNumberOfItems[0].SetActive(true);
        }


        if (Manage.NumberOfShootings <= 0)
        {
            IntenseShootingImage.color = new Color32(255, 255, 255, 100);
            DisplayTheNumberOfItems[1].SetActive(false);
        }
        else
        {
            IntenseShootingImage.color = new Color32(255, 255, 255, 255);
            DisplayTheNumberOfItems[1].SetActive(true);
        }


        if (Manage.NumberOfThreeArrows <= 0)
        {
            ThreeArrowImage.color = new Color32(255, 255, 255, 100);
            DisplayTheNumberOfItems[2].SetActive(false);
        }
        else
        {
            ThreeArrowImage.color = new Color32(255, 255, 255, 255);
            DisplayTheNumberOfItems[2].SetActive(true);
        }

        if (Manage.NumberOfShield <= 0)
        {
            ShieldImage.color = new Color32(255, 255, 255, 100);
            DisplayTheNumberOfItems[3].SetActive(false);
        }
        else
        {
            ShieldImage.color = new Color32(255, 255, 255, 255);
            DisplayTheNumberOfItems[3].SetActive(true);
        }

    }
    public  void LifeButton()
    {
        if (Manage.NumberOfLife > 0&&Player!=null&&Manage.Life<100 && Manage.Lose == false && Manage.Win == false)
        {
            Instantiate(GreenParticle, new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z), Quaternion.identity);
            Manage.Life = 100;
            Manage.NumberOfLife -= 1;
            if (Manage.Sound == 0)
            {
                LifeAudio.Play();
            }
            if (Manage.NumberOfLife <= 0)
            {
                Manage.NumberOfLife = 0;
            }
            Player.ShowBarlife();
            ShowItems();
        }
    }


    public void IntenseShootingButton()
    {

        if (Manage.Shoting == false&&Manage.NumberOfShootings>0 && Manage.Lose == false && Manage.Win == false)
        {
            Manage.NumberOfShootings -= 1;
          
            IntenseShootingTimer = 0;
            if (Manage.NumberOfShootings <= 0)
            {
                Manage.NumberOfShootings = 0;
            }
            ShowItems();
            Manage.Shoting = true;
        }
    }
    public void ThreeShotButton()
    {

        if (Manage.Three == false && Manage.NumberOfThreeArrows > 0 && Manage.Lose == false && Manage.Win == false)
        {
            Manage.NumberOfThreeArrows -= 1;

          
            if (Manage.NumberOfThreeArrows <= 0)
            {
                Manage.NumberOfThreeArrows = 0;
            }
            Manage.Three = true;
            ShowItems();
           
        }
    }
    public void HomeButton()
    {
        Time.timeScale = 1f;
        if (Manage.Win == false && Manage.Lose == false)
        {
            Manage.ResumeGame = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        } 
    }
    public void AgainButton()
    {
        Time.timeScale = 1f;
        Manage.ResumeGame = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
    public void Pause() {
        Time.timeScale = 0f;
        PausePanel.SetActive(true);

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            //int a = Random.Range(1, 3);
            //if (a == 1)
            //{
            //    AdsManager._instance.ShowUnityAds();
            //}
            //else
            //{
            //    AdsManager._instance.ShowUnityRewardedvideo();
            //}
        }
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
    }
    public void WatchVideo()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            //AdsManager._instance.ShowUnityAds();
            PlayerPrefs.SetInt("Chance", 1);
        }
    }
    public void ChanceFn()
    {
        Manage.SaveScore();
        Manage.ShowScores();
        PlayerPrefs.SetInt("Chance", 0);
    }
    public void WatchVideoNo()
    {
        ChancePanel.SetActive(false);
        GameOverPanel.SetActive(true);
    }
}
