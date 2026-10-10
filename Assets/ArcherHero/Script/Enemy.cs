using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int EnemyLevel;
    private float Life;
    //private Animator BowAnim;
    //public GameObject Bow;
    public GameObject Shootpoint;
    private Animator Anim;
    private bool hit;
    public int hitNumber;
 
    private float ShootTimer;
    public GameObject BowRotate;
    public float rotate;
    public float ShootTime;
    private bool Shooting;
    private bool Once;
    private float HitTimer;
    public GameObject Arrow;
    public GameObject DeadEnemy;
    public float EnemyForceX;

    public float EnemyForceY;
    private Manage Manage;

    public GameObject barlife;
    public GameObject Mylife;
    public GameObject Coin;
    private GameObject OutPut;
    private int RandomMoveUp;
    public GameObject Ground;
    public GameObject Fire;
    private AudioSource CoinAudio;

    private bool HeadShot;
    public GameObject HeadShotPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        //BowAnim = Bow.GetComponent<Animator>();
      //  Player = GameObject.Find("Player");
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
       
        CoinAudio = GameObject.Find("Coin-Audio").GetComponent<AudioSource>();
        if (Fire == null) { 
        Arrow = Manage.EnemyArrow;
        }
        else
        {
            Fire.SetActive(false);
        }
        Life = 100;
        if (EnemyLevel == 1)
        {
            if (Manage.Level >= 0 || Manage.Level <= 10)
            {
                ShootTime = Random.Range(2, 4);
            }
            else
            {
                ShootTime = Random.Range(1, 1.1f);
            }
            print("4 6 "+ ShootTime );
        }
        else if (EnemyLevel == 2)
        {
            if (Manage.Level >= 0 || Manage.Level <= 10)
            {
                ShootTime = Random.Range(2, 4);
            }
            else
            {
                ShootTime = Random.Range(1, 3f);
            }
            print("3 5");

        }
        else if (EnemyLevel == 3|| EnemyLevel == 4)
        {
            RandomMoveUp = Random.Range(0, 2);
            if (RandomMoveUp == 1)
            {
                Ground.GetComponent<Move>().Loop = true;
            }
            if (Manage.Level >= 0 || Manage.Level <= 10)
            {
                ShootTime = Random.Range(2, 4f);
            }
            else {
                ShootTime = Random.Range(1, 3f);
            }
            print("2 4");


        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Manage.Lose == false)
        {
            if (hit == false)
            {
                if (Shooting == false)
                {
                    Once = false;
                    //Bow.SetActive(false);
                    Anim.SetInteger("Enemy", 0);
                    ShootTimer += Time.deltaTime;
                    if (ShootTimer >= ShootTime)
                    {
                        Shooting = true;
                        print("ShootTimer--" + ShootTimer);
                        print("ShootTime--" + ShootTime);

                    }
                }
                else if (Shooting == true)
                {
                    ShootTimer = 0;

                    Anim.SetInteger("Enemy", 1);

                    //Bow.SetActive(true);

                    if (Once == false)
                    {
                        //BowAnim.SetInteger("Bow", 0);
                        StartCoroutine(ShootArrowNow());
                        print("shoot Arrow--");
                        if (Fire != null)
                        {
                            Fire.SetActive(true);
                        }
                        rotate = Random.Range(360, 300f);
                        Manage.EnemyBowAngleZ = rotate;
                        BowRotate.transform.eulerAngles = new Vector3(0, 0, rotate);
                        Once = true;
                    }

                }

            }
            else if (hit == true)
            {
                if (Fire != null)
                {
                    Fire.SetActive(false);
                }
                //Bow.SetActive(false);
                //BowAnim.SetInteger("Bow", 0);
                Shooting = false;
                ShootTimer = 0;
                Once = false;
                if (hitNumber == 0)
                {
                    //Anim.SetInteger("Enemy", 3);
                }
                else if (hitNumber == 1)
                {
                   // Anim.SetInteger("Enemy", 2);
                }
                else if (hitNumber == 2)
                {
                    //Anim.SetInteger("Enemy", 4);
                }

                HitTimer += 1 * Time.deltaTime;

                if (Manage.Level >= 0 || Manage.Level <= 10)
                {
                    if (HitTimer >= 0.5f)
                    {
                        hit = false;
                    }
                }
                else {
                    if (HitTimer >= 0.05f)
                    {
                        hit = false;
                    }
                }

               /* if (HitTimer >= 0.05f)
                {
                    hit = false;
                    print("0.7 " +HitTimer );
                }*/
            }
        }
        else { /*Anim.SetInteger("Enemy", 5);*/ }
    }
    IEnumerator ShootArrowNow()
    {
        if (Manage.Level >= 0 || Manage.Level <= 10)
        {
            yield return new WaitForSeconds(0.7f);
            print("1 to 10");
        }
        else 
        { 
            yield return new WaitForSeconds(0.03f);
            print("10 to 40");
        }
        if (hit == false)
          if (rotate <= 360 && rotate > 340)
            {
                EnemyForceX = Random.Range(-7f, -3f);
                EnemyForceY = Random.Range(0.5f, 1.5f);
            }
            else if (rotate <= 340 && rotate > 320)
            {
                EnemyForceX = Random.Range(-7f, -3f);
                EnemyForceY = Random.Range(1.5f, 2.5f);
            }
            else if (rotate <= 320 && rotate > 300)
            {
                EnemyForceX = Random.Range(-7f, -4f);
                EnemyForceY = Random.Range(2f, 4f);
            }
        Manage.EnemyForceX = EnemyForceX;
        Manage.EnemyForceY = EnemyForceY;

      //BowAnim.SetInteger("Bow", 1);
      StartCoroutine(idelNow());

        newEnemyArrow();
       
       

    }
    IEnumerator idelNow()
    {
        if (Manage.Level >= 0 || Manage.Level <= 10)
        {
            yield return new WaitForSeconds(0.01f);
            print("if Idle");
        }
        else
        {
            yield return new WaitForSeconds(0.01f);
            print("else Idle");
        }
        if (hit == false)
       Anim.SetInteger("Enemy", 0);
        //Bow.SetActive(false);
        Shooting = false;
        ShootTimer = 0;
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Arrow")
        {
            HitTimer = 0;
            hit = true;
            if (coll.gameObject.transform.position.y >= transform.position.y + 0.3f)
            {
                if (Manage.Sound == 0) { Manage.PlayHeadShot(); }
                //Instantiate(HeadShotPrefab, new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z), Quaternion.identity);
              //  Instantiate(HeadShotPrefab, new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z), Quaternion.identity);
                HeadShot = true;
                if (EnemyLevel == 1)
                {
                    Life -= 101;
                }
                else if (EnemyLevel == 2)
                {
                    Life -= 101;
                }
                else if (EnemyLevel == 3)
                {
                    Life -= 75;
                }
                else if (EnemyLevel == 4)
                {
                    Life -= 60;
                }

                hitNumber = 2;
            }

            else  if (coll.gameObject.transform.position.y >= transform.position.y - 0.1f&& coll.gameObject.transform.position.y < transform.position.y + 0.3f)
            {
                if (EnemyLevel == 1)
                {
                    Life -= 50;
                }
                else if (EnemyLevel == 2)
                {
                    Life -= 45;
                }
                else if (EnemyLevel == 3)
                {
                    Life -= 40;
                }
                else if (EnemyLevel == 4)
                {
                    Life -= 35;
                }

                hitNumber = 1;
            }
            else if (coll.gameObject.transform.position.y < transform.position.y - 0.1f )
            {
                if (EnemyLevel == 1)
                {
                    Life -= 30;
                }
                else if (EnemyLevel == 2)
                {
                    Life -= 25;
                }
                else if (EnemyLevel == 3)
                {
                    Life -= 20;
                }
                else if (EnemyLevel == 4)
                {
                    Life -= 15;
                }
                hitNumber = 0;
            }
           
            if (Life <= 0)
            {
                if (HeadShot == false)
                {
                    OutPut = Instantiate(Coin, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
                    OutPut.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 6f);
                    Manage.Coin += 1;
                }
                else if (HeadShot == true)
                {
                    OutPut = Instantiate(Coin, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
                    OutPut.GetComponent<Rigidbody2D>().velocity = new Vector3(1, 6f);
                    OutPut = Instantiate(Coin, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
                    OutPut.GetComponent<Rigidbody2D>().velocity = new Vector3(-1, 6f);
                    Manage.Coin += 2;
                }


                Manage.SaveScore();
                if (Manage.Sound == 0)
                {
                    CoinAudio.Play();
                }
                //GameObject Output2 = Instantiate(DeadEnemy, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
                //Output2.GetComponent<Rigidbody2D>().velocity = new Vector3(1, 3f);
                Manage.Score += 25;
                //score
             
                Manage.ShowScores();
                Destroy(gameObject);
            }
            ShowBarlife();
            
        }
    }

    private void ShowBarlife()
    {

        float mylife = Life / 100;
        Mylife.transform.localScale = new Vector3(mylife, 1, 1);
        barlife.SetActive(true);
        StartCoroutine(Endshowbarlife());

       
    }
    IEnumerator Endshowbarlife()
    {
        yield return new WaitForSeconds(0.75f);
      if(hit==false)
        barlife.SetActive(false);
    }

    public void newEnemyArrow()
    {
        if (hit == false) {
            if (Fire != null)
            {
                Fire.SetActive(false);
            }
            GameObject MyEnemyArrow = Instantiate(Arrow, new Vector3(Shootpoint.transform.position.x, Shootpoint.transform.position.y, Shootpoint.transform.position.z), Quaternion.identity) as GameObject;
        MyEnemyArrow.transform.eulerAngles = new Vector3(0, 0, rotate);
        }
    }

}
