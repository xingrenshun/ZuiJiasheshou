using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chance : MonoBehaviour
{
    public GameObject GameOverPanel, ChancePanel;
    public Text errortxt;
    public GameObject Player;
    private Manage Manage;
    // Start is called before the first frame update
    void Start()
    {
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("rewardvdo") == 1)
        {
            if (PlayerPrefs.GetInt("Relife", 1) == 1)
            {
                Time.timeScale = 1f;
                ChancePanel.SetActive(false);
                Player.SetActive(true);
                //Manage.NumberOfLife += 2;
                GameObject.FindObjectOfType<DragNShoot>().ShowBarAgainlife();
                Manage.MoreChance();
                PlayerPrefs.SetInt("rewardvdo", 0);
                PlayerPrefs.SetInt("Relife", 0);
            }
        }
        else if (PlayerPrefs.GetInt("rewardvdo", 1) == 2)
        {
            //Video Failed
            PlayerPrefs.SetInt("rewardvdo", 0);
        }
    }
    public void WatchVideo() {
#if UNITY_EDITOR
        Relifefn();
#else
        if (AdManager.Instance != null)
        {
            AdManager.Instance.ShowRewardedVideoAd(() =>
            {
                Relifefn();
            }, (error) =>
            {
                Debug.Log("Ad Failed: " + error);
            });
        }
        else
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                errortxt.gameObject.SetActive(true);
            }
        }
#endif
    }
    public void Relifefn()
    {
        Time.timeScale = 1f;
        ChancePanel.SetActive(false);
        Player.SetActive(true);
        //Manage.NumberOfLife += 2;
        GameObject.FindObjectOfType<DragNShoot>().ShowBarAgainlife();
        Manage.MoreChance();
        PlayerPrefs.SetInt("Relife", 0);
    }
    public void WatchVideoNo() {
        Manage.Lose = true;
        Manage.Losegame();
        ChancePanel.SetActive(false);
        // GameOverPanel.SetActive(true);
        Time.timeScale = 1f;
    }
}
