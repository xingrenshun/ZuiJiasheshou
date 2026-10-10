using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Manage : MonoBehaviour
{
    public GameObject Shield;
    public GameObject shotpoint;
    public GameObject Enemyshotpoint;
    public GameObject []Arrow;
    public GameObject EnemyArrow;
    public int Level;
    public Vector2 force;
    public float Life;
    public bool Drag;
    public float AngleZ;
    public float EnemyBowAngleZ;
    public bool hit;
    public int Enemy;
    public GameObject AgainMenu, ChancePanel;
    public int Score;
   
    public bool ResumeGame;
    public bool Lose;
    public float EnemyForceX;
    public float EnemyForceY;
    public TextMeshProUGUI ShowLevel;
    public TextMeshProUGUI ShowScore;
    public Text ShowKillScore;
    public TextMeshProUGUI ShowCoin, MainMenuCoinn;
    public GameObject IntenseShootingPosition;
    public bool Shoting;
    public bool Three;
    public int Coin;
    public int NumberOfThreeArrows;
    public int NumberOfShield;
    public int NumberOfShootings;
    public int NumberOfLife;
    public bool Win;
    public int Sound;
    private float Timer;
    private AudioSource HeadShot;
    private int Count;
    public int Lock;
    public GameObject WinMenu;
    public bool SelectLevel;

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        DontDestroyOnLoad(gameObject);
        HeadShot = GameObject.Find("HeadShot-Audio").GetComponent<AudioSource>();
       ResumeGame = false;
        //StartCoroutine(Logo());

        NumberOfThreeArrows = PlayerPrefs.GetInt("NumberOfThreeArrows", NumberOfThreeArrows);
        NumberOfShield = PlayerPrefs.GetInt("NumberOfShield", NumberOfShield);
        NumberOfShootings = PlayerPrefs.GetInt("NumberOfShootings", NumberOfShootings);
        NumberOfLife = PlayerPrefs.GetInt("NumberOfLife", NumberOfLife);

        Lock = PlayerPrefs.GetInt("Lock", Lock);
        Coin = PlayerPrefs.GetInt("Coin", Coin);

        if (Lock == 0)
        {
            Lock = 1;
        }
        Level = Lock;
        if (Level>=10 )
        {
            Level = 10;
        }
    }
    public void PlayHeadShot()
    {
        StartCoroutine(Playnow());
    }
    IEnumerator Playnow()
    {
        yield return new WaitForSeconds(0.55f);
        HeadShot.Play();
    }
    IEnumerator Logo()
    {
        yield return new WaitForSeconds(3f);
      
        SceneManager.LoadScene("Play", LoadSceneMode.Single);
    }
    public void ShowScores()
    {
        ShowScore.text = "" + (int)Score;
        ShowLevel.text = ChangeNum((int)Level);
        ShowLevel.text = ChangeNum((int)Level);
        ShowCoin.text = "" + (int)Coin;
        MainMenuCoinn.text = "" + (int)Coin;
    }
    public string ChangeNum(int num)
    {
        string Num = "";
        switch (num)
        {
            case 1:
                Num = "一";
                break;
            case 2:
                Num = "二";
                break;
            case 3:
                Num = "三";
                break;
            case 4:
                Num = "四";
                break;
            case 5:
                Num = "五";
                break;
            case 6:
                Num = "六";
                break;
            case 7:
                Num = "七";
                break;
            case 8:
                Num = "八";
                break;
            case 9:
                Num = "九";
                break;
            case 10:
                Num = "十";
                break;
            default:
                break;
        }
        return "第"+Num+"关";
    }
    public void SaveLevel()
    {
        if (Lock <= Level)
        {
            Lock = Level + 1;
        }
       
        PlayerPrefs.SetInt("Lock", Lock);
        StartCoroutine(EndLevel());
    }
    IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(3f);
        WinMenu.SetActive(true);

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            //AdsManager._instance.ShowUnityAds();
            print("ads play");
        }
    }
    public void SaveScore()
    {
        PlayerPrefs.SetInt("NumberOfThreeArrows", NumberOfThreeArrows);
        PlayerPrefs.SetInt("NumberOfShield", NumberOfShield);
        PlayerPrefs.SetInt("NumberOfShootings", NumberOfShootings);
        PlayerPrefs.SetInt("NumberOfLife", NumberOfLife);
        PlayerPrefs.SetInt("Coin", Coin);
    }
    public void MoreChance() {
        PlayerPrefs.SetInt("NumberOfLife", NumberOfLife);
    }
    // Update is called once per frame
    void Update()
    {
        //PlayerPrefs.DeleteAll();
        //Lock = PlayerPrefs.GetInt("Lock");
       // MainMenuCoinn.text = "" + PlayerPrefs.GetInt("MCoin",0);

        if (Shoting == true && Lose == false)
        {
           
            Timer += 1 * Time.deltaTime;
            if (Timer >= 0.2f)
            {
                Instantiate(Arrow[1], new Vector3(IntenseShootingPosition.transform.position.x+UnityEngine.Random.Range(-2,2f),
               IntenseShootingPosition.transform.position.y, IntenseShootingPosition.transform.position.z), Quaternion.identity);
                Timer = 0;
            }
        }
    }
    public void newArrow()
    {
        if (Three == false)
        {
            GameObject MyArrow = Instantiate(Arrow[0], new Vector3(shotpoint.transform.position.x, shotpoint.transform.position.y, shotpoint.transform.position.z), Quaternion.identity) as GameObject;
            MyArrow.transform.eulerAngles = new Vector3(0, 0, AngleZ);
            if (Count > 0)
            {
                Count = 0;
            }
            
        }
        if (Three == true)
        {
            GameObject MyArrow = Instantiate(Arrow[2], new Vector3(shotpoint.transform.position.x, shotpoint.transform.position.y, shotpoint.transform.position.z), Quaternion.identity) as GameObject;
            MyArrow.transform.eulerAngles = new Vector3(0, 0, AngleZ);
             Count += 1;
            if (Count >= 3)
            {
                Three = false;
            }
            
        }

    }
   public void LoseChanceGame()
    {        
        SaveScore();
        StartCoroutine(isChance());
    }
    IEnumerator isChance()
    {
        yield return new WaitForSeconds(3f);
        //ChancePanel.SetActive(true);
        //Time.timeScale = 0f;
        // StopCoroutine(isChance());
        Lose = true;
        Losegame();
    }
    public void Losegame()
    {
        Lose = true;
        SaveScore();
        StartCoroutine(isGameover());
    }
    IEnumerator isGameover()
    {
        yield return new WaitForSeconds(2f);
        AgainMenu.SetActive(true);

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
   

    public void defend()
    {
        if (Shield != null)
        {
            Shield.SetActive(true);
        }
    }
    public void NoDefense() {

        if (Shield != null)
        {
            Shield.SetActive(false);
        }

    }

}
