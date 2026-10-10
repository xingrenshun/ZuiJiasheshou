using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public float ShootingTime;
    private float Life;
    public float Damage1;
    public float Damage2;
    public GameObject Shootpoint;
    private Animator Anim;
    private bool hit;
    public float HitPosition;
    
    private float ShootTimer;
    private float rotate;


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
    
   

    private AudioSource CoinAudio;


    public GameObject HeadShotPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
       
      
        Manage = GameObject.Find("Manage").GetComponent<Manage>();

        CoinAudio = GameObject.Find("Coin-Audio").GetComponent<AudioSource>();
     
        Life = 100;
        
            ShootTime = Random.Range(2.75f, 3.5f);
       
     
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
                   
                    Anim.SetInteger("Enemy", 0);
                    ShootTimer += Time.deltaTime;
                    if (ShootTimer >= ShootTime)
                    {
                        Shooting = true;

                    }
                }
                else if (Shooting == true)
                {
                    ShootTimer = 0;
                    Anim.SetInteger("Enemy", 1);
                    if (Once == false)
                    {                    
                        StartCoroutine(ShootArrowNow());
                        rotate = Random.Range(360, 300f);
                        Manage.EnemyBowAngleZ = rotate;  
                        Once = true;
                    }

                }

            }
            else if (hit == true)
            {

              
               
                Shooting = false;
                //ShootTimer = 0;
                Once = false;
               
                    //Anim.SetInteger("Enemy", 2);
               

                HitTimer += 1 * Time.deltaTime;

                if (Manage.Level >= 0 || Manage.Level <= 10) {
                    if (HitTimer >= 0.5f)
                    {
                        hit = false;
                    }
                } else {
                    if (HitTimer >= 0.05f)
                    {
                        hit = false;
                    }
                }
               /* if (HitTimer >= 0.05f)
                {
                    hit = false;
                    print("0.7");
                }*/
            }
        }
        else { Anim.SetInteger("Enemy", 0); }
    }
    IEnumerator ShootArrowNow()
    {
        yield return new WaitForSeconds(ShootingTime);
        if (hit == true)
        {
            hit = false;
        }
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

        StartCoroutine(idelNow());
        newEnemyArrow();

    }
    IEnumerator idelNow()
    {
        yield return new WaitForSeconds(0.01f);
        if (hit == false)
        {
            Anim.SetInteger("Enemy", 0);
        }
       
    
        Shooting = false;
        ShootTimer = 0;
    }


    void OnCollisionEnter2D(Collision2D coll)
    {


        if (coll.gameObject.tag == "Arrow")
        {


            HitTimer = 0;
            hit = true;
            if (coll.gameObject.transform.position.y >= transform.position.y + HitPosition)
            {
                if (Manage.Sound == 0) { Manage.PlayHeadShot(); }
                Instantiate(HeadShotPrefab, new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z), Quaternion.identity);
              //  Instantiate(HeadShotPrefab, new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z), Quaternion.identity);

                Life -= Damage1;
            }
            else if (coll.gameObject.transform.position.y < transform.position.y + HitPosition)
            {
                Life -= Damage2;
            }

            if (Life <= 0)
            {
                OutPut = Instantiate(Coin, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
                OutPut.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 6f);
                Manage.Coin += 1;
                Manage.SaveScore();
                if (Manage.Sound == 0)
                {
                    CoinAudio.Play();
                }
                GameObject Output2 = Instantiate(DeadEnemy, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
                Output2.GetComponent<Rigidbody2D>().velocity = new Vector3(1, 3f);
                Manage.Score += 100;
              
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
        if (hit == false)
            barlife.SetActive(false);
    }

    public void newEnemyArrow()
    {
        if (hit == false)
        {
            GameObject MyEnemyArrow = Instantiate(Arrow, new Vector3(Shootpoint.transform.position.x, Shootpoint.transform.position.y, Shootpoint.transform.position.z), Quaternion.identity) as GameObject;
            MyEnemyArrow.transform.eulerAngles = new Vector3(0, 0, rotate);
        }
    }

}
