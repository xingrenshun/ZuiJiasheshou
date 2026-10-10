using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class WinMenu : MonoBehaviour
{
    private Manage Manage;
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Level;
    public TextMeshProUGUI Coin;

    public GameObject NextLevel;
    public GameObject Finalpost;
    // Start is called before the first frame update
    void Start()
    {
       // PlayerPrefs.DeleteAll();
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
        Score.text = "x" + (int)Manage.Score;
        Level.text = Manage.ChangeNum(Manage.Level);
        Coin.text = "x" + (int)Manage.Coin;
        StartCoroutine(GuangGao.Instance.WaitShowXP());        
        if (Manage.Level>=10)
        {
            GuangGao.Instance.UploadScore(10);
            Destroy(NextLevel);
            Finalpost.SetActive(true);
        }
        else
        {
            GuangGao.Instance.UploadScore(PlayerPrefs.GetInt("Lock") - 1);
        }
    }

    public void NextLevelButton()
    {
        Manage.Win = false;
        Manage.ResumeGame = true;
        Manage.Level += 1;
        if (Manage.Level >= 10) { Manage.Level = 10; }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
    public void HomeButton()
    {
        Manage.ResumeGame = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
    public void AgainButton()
    {
        Manage.ResumeGame = true;      
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
