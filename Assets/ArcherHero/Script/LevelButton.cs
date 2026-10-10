using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelButton : MonoBehaviour
{
    public int ButtonNumber;
    private Manage Manage;
    public GameObject Lock;
    public GameObject RedArrow;
    void Start()
    {
        Manage = GameObject.Find("Manage").GetComponent<Manage>();

        if (ButtonNumber <= Manage.Lock&&Lock!=null)
        {
            Destroy(Lock);
        }


        if (Manage.Level == ButtonNumber)
        {

            //RedArrow.SetActive(true);
        }
    }
    public void IAPUnlock() {
        Manage = GameObject.Find("Manage").GetComponent<Manage>();

        if (ButtonNumber <= Manage.Lock && Lock != null)
        {
            Destroy(Lock);
        }


        if (Manage.Level == ButtonNumber)
        {

            //RedArrow.SetActive(true);
        }
    }
    public void SelectLevel()
    {
        if (Lock == null&&Manage.ResumeGame==false)
        {
            Manage.Level = ButtonNumber;
            Manage.ResumeGame = true;
            Manage.SelectLevel = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
    }
}
