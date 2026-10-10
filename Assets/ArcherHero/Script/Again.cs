using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Again : MonoBehaviour
{
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Level;
    private Manage Manage;
    public TextMeshProUGUI Coin;
    
    
    // Start is called before the first frame update
    void Start()
    {

        Manage = GameObject.Find("Manage").GetComponent<Manage>();
        Score.text = "x" + (int)Manage.Score;
        Level.text = Manage.ChangeNum(Manage.Level);
        Coin.text = "x" + (int)Manage.Coin;
        StartCoroutine(GuangGao.Instance.WaitShowXP());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AgainButton()
    {
        Manage.ResumeGame = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
