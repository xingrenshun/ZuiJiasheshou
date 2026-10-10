using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject []Targets;
    private Manage Manage;
    private GameObject Player;
    public int Level;
    public GameObject []Boss;
    public GameObject[]OutPut1;
    public GameObject[] OutPut2;
    public GameObject[] OutPut3;
    public GameObject[] OutPut4;
    public GameObject []Enemy;
    private GameObject Target;
    public GameObject[] Balloons;
    private bool Balloon;
    private int Count;
    private float BallonsTimer;
    private int random;

    public bool Once;
    // Start is called before the first frame update
    void Start()
    {
        Level = 0;

        Manage = GameObject.Find("Manage").GetComponent<Manage>();     
        Player = GameObject.Find("Player");
        Manage.Win = false;
        Targets[1] = GameObject.Find("Targer-position-1");
        Targets[2] = GameObject.Find("Targer-position-2");
        Targets[3] = GameObject.Find("Targer-position-3");
        Targets[4] = GameObject.Find("Targer-position-4");

        Targets[5] = GameObject.Find("Targer-position-5");
        Targets[6] = GameObject.Find("Targer-position-6");
        Targets[7] = GameObject.Find("Targer-position-7");
        Targets[8] = GameObject.Find("Targer-position-8");

        Targets[9] = GameObject.Find("Targer-position-9");
        Targets[10] = GameObject.Find("Targer-position-10");
        Targets[11] = GameObject.Find("Targer-position-11");
        Targets[12] = GameObject.Find("Targer-position-12");

        Targets[13] = GameObject.Find("Targer-position-13");
        Targets[14] = GameObject.Find("Targer-position-14");
        Targets[15] = GameObject.Find("Targer-position-15");
        Targets[16] = GameObject.Find("Targer-position-16");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null&&Manage.Win==false)
        {
            if (Balloon == false)
            {
                if (Manage.Enemy <= 0)
                {
                    newEnemy();
                }
            }
            else if (Balloon == true)
            {
                if (Count < 20) { 
                BallonsTimer += 1 * Time.deltaTime;
                    if (BallonsTimer >= 0.25f)
                    {
                        Instantiate(Balloons[Random.Range(0, 3)], new Vector3(Random.Range(0, 7f), Random.Range(-7, -8f), 0), Quaternion.identity);
                        Count += 1;
                        BallonsTimer = 0;
                    }
                }
                else
                {
                 if (Manage.Win==false)
                    {
                        if (Once == false) {
                            print("first time--");
                            StartCoroutine(EndLevel());
                            Once = true;                            
                        }
                    }
                }
            }
        }
    }
    private void newEnemy()
    {
        if (Level < 5)
        {
            //    Level 1-5
            if (Manage.Level == 1)
            {
                if (Level == 0 || Level == 1 )
                {
                    Target = Targets[Random.Range(1, 17)];
                    OutPut1[0] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Manage.Enemy += 1;
                    print("Level Part 1");
                }
                else if (Level == 2)
                {
                    Target = Targets[Random.Range(1, 4)];
                    OutPut1[0] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                   
                    Manage.Enemy += 1;
                    Level = 5;
                    print("Level Part 2");
                }
            }
          else  if (Manage.Level == 2)
            {
                if (Level == 0 )
                {
                    Target = Targets[Random.Range(1, 17)];
                    OutPut1[0] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Manage.Enemy += 1;
                }
                else if (Level == 1)
                {
                    Target = Targets[Random.Range(1, 4)];
                    OutPut1[0] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(5, 9)];
                    OutPut2[Random.Range(0, 4)] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Manage.Enemy += 2;
                }
                else if (Level == 2 )
                {
                    Target = Targets[Random.Range(1, 4)];
                    OutPut1[0] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(5, 9)];
                    OutPut2[Random.Range(0, 4)] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Manage.Enemy += 2;
                    Level = 5;
                }

            }
            else if (Manage.Level == 3)
            {

                if (Level == 0|| Level == 1)
                {
                    Target = Targets[Random.Range(1, 17)];
                    OutPut1[0] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Manage.Enemy += 1;
                }
                else if (Level == 2||Level == 3)
                {
                    Target = Targets[Random.Range(1, 4)];
                    OutPut1[0] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(5, 9)];
                    OutPut2[Random.Range(0, 4)] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Manage.Enemy += 2;
                }
                else if (Level == 4)
                {
                    Target = Targets[Random.Range(1, 4)];
                    OutPut1[0] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(5, 9)];
                    OutPut2[Random.Range(0, 4)] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;


                    Manage.Enemy += 2;
                   
                }

            }
            else if (Manage.Level == 4)
            {

                if (Level == 0 || Level == 1)
                {
                    Target = Targets[Random.Range(1, 17)];
                    OutPut1[0] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(5, 9)];
                    OutPut2[Random.Range(0, 4)] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Manage.Enemy += 2;
                }
                else if (Level == 2)
                {
                    Target = Targets[Random.Range(1, 4)];
                    OutPut1[0] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(5, 9)];
                    OutPut2[Random.Range(0, 4)] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(9, 13)];
                    OutPut3[Random.Range(0, 4)] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Manage.Enemy += 3;
                }
                else if (Level == 3)
                {
                    Target = Targets[Random.Range(1, 4)];
                    OutPut1[0] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(5, 9)];
                    OutPut2[Random.Range(0, 4)] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(9, 13)];
                    OutPut3[Random.Range(0, 4)] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;

                    Manage.Enemy += 3;
                    Level = 5;
                }

            }
            else if (Manage.Level == 5)
            {
                if (Level == 0 || Level == 1)
                {
                    Target = Targets[Random.Range(1, 4)];
                    OutPut1[0] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(5, 9)];
                    OutPut2[Random.Range(0, 4)] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Manage.Enemy += 2;
                }
                else if (Level == 2 || Level == 3)
                {
                    Target = Targets[Random.Range(1, 5)];
                    OutPut1[0] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(5, 9)];
                    OutPut2[Random.Range(0, 4)] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(9, 13)];
                    OutPut3[Random.Range(0, 4)] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;

                    Manage.Enemy += 3;
                }
                else if (Level == 4)
                {
                    if (Manage.Level == 5)
                    {
                        Balloon = true;




                    }



                }
            }

            /////  level   6-10



            else if (Manage.Level >= 6 && Manage.Level <= 10)
            {
                if (Level == 0 || Level == 1)
                {
                    Target = Targets[Random.Range(1, 4)];
                    OutPut1[0] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(5, 9)];
                    OutPut2[Random.Range(0, 4)] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Manage.Enemy += 2;
                }
                else if (Level == 2 || Level == 3)
                {
                    Target = Targets[Random.Range(1, 5)];
                    OutPut1[0] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(5, 9)];
                    OutPut2[Random.Range(0, 4)] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(9, 13)];
                    OutPut3[Random.Range(0, 4)] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(13, 17)];
                    OutPut4[Random.Range(0, 4)] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;

                    Manage.Enemy += 4;
                }
                else if (Level == 4)
                {
                    if (Manage.Level == 10)
                    {
                        Target = Targets[Random.Range(1, 13)];
                        OutPut1[0] = Instantiate(Boss[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                        Manage.Enemy += 1;
                    }

                }
            }




            /////  level   11-15



            else if (Manage.Level >= 11 && Manage.Level <= 15)
            {
                if (Level == 0 || Level == 1)
                {
                    Target = Targets[Random.Range(1, 4)];
                    OutPut1[1] = Instantiate(Enemy[1], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;


                    Manage.Enemy += 1;
                }
                else if (Level == 2 || Level == 3)
                {
                    Target = Targets[Random.Range(1, 5)];
                    OutPut1[0] = Instantiate(Enemy[1], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(5, 9)];
                    OutPut2[Random.Range(0, 4)] = Instantiate(Enemy[0], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;


                    Manage.Enemy += 2;
                }
                else if (Level == 4)
                {
                    if (Manage.Level == 15)
                    {
                        Balloon = true;
                    }

                }
            }



            /////  level   16-20
            else if (Manage.Level >= 16 && Manage.Level <= 20)
            {
                if (Level == 0 || Level == 1)
                {
                    Target = Targets[Random.Range(1, 4)];
                    OutPut1[0] = Instantiate(Enemy[1], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(5, 9)];
                    OutPut2[Random.Range(0, 4)] = Instantiate(Enemy[1], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Manage.Enemy += 2;
                }
                else if (Level == 2 || Level == 3)
                {
                    Target = Targets[Random.Range(1, 5)];
                    OutPut1[0] = Instantiate(Enemy[1], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(5, 9)];
                    OutPut2[Random.Range(0, 4)] = Instantiate(Enemy[1], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(9, 13)];
                    OutPut3[Random.Range(0, 4)] = Instantiate(Enemy[1], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;


                    Manage.Enemy += 3;
                }
                else if (Level == 4)
                {
                    if (Manage.Level == 20)
                    {
                        Target = Targets[Random.Range(1, 13)];


                        OutPut1[0] = Instantiate(Boss[1], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                        Manage.Enemy += 1;
                    }

                }
            }



            /////  level   21-25
            else if (Manage.Level >= 21 && Manage.Level <= 25)
            {
                if (Level == 0 || Level == 1)
                {
                    Target = Targets[Random.Range(1, 5)];
                    OutPut1[0] = Instantiate(Enemy[2], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(5, 9)];
                    OutPut2[Random.Range(0, 4)] = Instantiate(Enemy[2], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Manage.Enemy += 2;
                }
                else if (Level == 2 || Level == 3)
                {
                    Target = Targets[Random.Range(1, 5)];
                    OutPut1[0] = Instantiate(Enemy[2], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(5, 9)];
                    OutPut2[Random.Range(0, 4)] = Instantiate(Enemy[2], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(9, 13)];
                    OutPut3[Random.Range(0, 4)] = Instantiate(Enemy[2], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;


                    Manage.Enemy += 3;
                }
                else if (Level == 4)
                {
                    if (Manage.Level == 25)
                    {
                        Balloon = true;
                    }

                }
            }



            /////  level   26-30
            else if (Manage.Level >= 26 && Manage.Level <= 30)
            {
                if (Level == 0 || Level == 1)
                {
                    Target = Targets[Random.Range(1, 5)];
                    OutPut1[0] = Instantiate(Enemy[2], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(5, 9)];
                    OutPut2[Random.Range(0, 4)] = Instantiate(Enemy[2], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(9, 13)];
                    OutPut3[Random.Range(0, 4)] = Instantiate(Enemy[2], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;


                    Manage.Enemy += 3;
                }
                else if (Level == 2 || Level == 3)
                {
                    Target = Targets[Random.Range(1, 5)];
                    OutPut1[0] = Instantiate(Enemy[2], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(5, 9)];
                    OutPut2[Random.Range(0, 4)] = Instantiate(Enemy[2], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(9, 13)];
                    OutPut3[Random.Range(0, 4)] = Instantiate(Enemy[2], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(13, 17)];
                    OutPut4[Random.Range(0, 4)] = Instantiate(Enemy[2], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;

                    Manage.Enemy += 4;
                }
                else if (Level == 4)
                {
                    if (Manage.Level == 30)
                    {
                        Target = Targets[Random.Range(1, 13)];


                        OutPut1[0] = Instantiate(Boss[2], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                        Manage.Enemy += 1;
                    }

                }
            }


            /////  level   31-35
            else if (Manage.Level >= 31 && Manage.Level <= 35)
            {
                if (Level == 0 || Level == 1)
                {
                    Target = Targets[Random.Range(1, 5)];
                    OutPut1[0] = Instantiate(Enemy[3], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(5, 9)];
                    OutPut2[Random.Range(0, 4)] = Instantiate(Enemy[3], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;


                    Manage.Enemy += 2;
                }
                else if (Level == 2 || Level == 3)
                {
                    Target = Targets[Random.Range(1, 5)];
                    OutPut1[0] = Instantiate(Enemy[3], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(5, 9)];
                    OutPut2[Random.Range(0, 4)] = Instantiate(Enemy[3], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(9, 13)];
                    OutPut3[Random.Range(0, 4)] = Instantiate(Enemy[3], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(13, 17)];
                    OutPut4[Random.Range(0, 4)] = Instantiate(Enemy[3], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;

                    Manage.Enemy += 4;
                }
                else if (Level == 4)
                {
                    if (Manage.Level == 35)
                    {
                        Balloon = true;
                    }

                }
            }

            /////  level   36-40
            else if (Manage.Level >= 36 && Manage.Level <= 40)
            {
                if (Level == 0 || Level == 1)
                {
                    Target = Targets[Random.Range(1, 5)];
                    OutPut1[0] = Instantiate(Enemy[3], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(5, 9)];
                    OutPut2[Random.Range(0, 4)] = Instantiate(Enemy[3], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(9, 13)];
                    OutPut3[Random.Range(0, 4)] = Instantiate(Enemy[3], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;

                    Manage.Enemy += 3;
                }
                else if (Level == 2 || Level == 3)
                {
                    Target = Targets[Random.Range(1, 5)];
                    OutPut1[0] = Instantiate(Enemy[3], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(5, 9)];
                    OutPut2[Random.Range(0, 4)] = Instantiate(Enemy[3], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(9, 13)];
                    OutPut3[Random.Range(0, 4)] = Instantiate(Enemy[3], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                    Target = Targets[Random.Range(13, 17)];
                    OutPut4[Random.Range(0, 4)] = Instantiate(Enemy[3], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;

                    Manage.Enemy += 4;
                }
                else if (Level == 4)
                {
                    if (Manage.Level == 40)
                    {
                        Target = Targets[Random.Range(1, 13)];

                        OutPut1[0] = Instantiate(Boss[3], new Vector3(Target.transform.position.x, Target.transform.position.y, 0), Quaternion.identity) as GameObject;
                        Manage.Enemy += 1;
                    }
                }
            }
            Level += 1;
        }
        else if (Level >= 5)
        {
            if (Manage.Lose == false&&Manage.Win==false)
            {
                if (Once == false) { 
                    Manage.SaveLevel();
                Manage.SaveScore();
                Manage.Win = true;
                    Once = true;
            }
            }
        }
       

    }


    IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(8f);
        if (Manage.Lose == false&&Manage.Win==false)
        {
            Manage.SaveScore();
          
            Manage.SaveLevel();
            Manage.Win = true;
            print("Second time--");
        }
       
      
    }
}
