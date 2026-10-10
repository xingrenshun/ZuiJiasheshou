using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Map : MonoBehaviour
{
    private int Number;
    public GameObject[] Target;
    public GameObject MoveBackground;
    private Manage Manage;

    public GameObject Right;
    public GameObject Left;

    private AudioSource ArrowAudio;
    public GameObject []Menu;
    public GameObject Exit;

    // Start is called before the first frame update
    void Start()
    {
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
        ArrowAudio = GameObject.Find("Arrow-Audio").GetComponent<AudioSource>();
       
        if (Manage.Level > 0 && Manage.Level <= 10)
        {
            Number = 0;
          
        }
        else if (Manage.Level > 10 && Manage.Level <= 20)
        {
            Number = 1;
        }
        else if (Manage.Level > 20 && Manage.Level <= 30)
        {
            Number = 2;
        }
        else if (Manage.Level > 30 && Manage.Level <= 40)
        {
            Number = 3;
            Right.SetActive(false);
        }
        if (Number == 0)
        {
            Left.SetActive(false);
        }
        if (Manage.ResumeGame == true)
        {
          
            Right.SetActive(false);
            Left.SetActive(false);
            Exit.SetActive(false);
            

            
        }



    }
   


    // Update is called once per frame
    void Update()
    {
        MoveBackground.transform.position = Vector3.Lerp(MoveBackground.transform.position, Target[Number].transform.position , Time.deltaTime * 3.5f);
    }
    public void RightButton()
    {
        Number += 1;
        if (Manage.Sound <= 0)
        {
            ArrowAudio.Play();
        }
        Left.SetActive(true);
        if (Number >= 3)
        {
            Number = 3;
            Right.SetActive(false);
        }
        else { Right.SetActive(true);  }
    }
    public void LeftButton()
    {
        if (Manage.Sound <= 0)
        {
            ArrowAudio.Play();
        }
        Number -= 1;
        Right.SetActive(true);
        if (Number <= 0)
        {
            Number = 0;
            Left.SetActive(false);
        }
        else
        {
            Left.SetActive(true);
        }
    }
    public void ExitButton()
    {
        Menu[0].SetActive(true);
        Menu[1].SetActive(false);
    }
}
