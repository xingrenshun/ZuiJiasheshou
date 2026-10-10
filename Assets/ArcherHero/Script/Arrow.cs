using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private bool hasHit;
    public float power = 10f;
    private Rigidbody2D rb;
    private Vector2 force;
    private Manage Manage;
    public GameObject BloodParticle;
    public GameObject BloodPoint;

    private AudioSource HitAudio;
    private AudioSource RockAudio;
    private AudioSource ArrowAudio;

    public bool ThreeArrows;
    public bool IntenseShooting;

    private  GameObject[] Output=new GameObject [2];
    public GameObject AnotherOne;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
        HitAudio = GameObject.Find("Hit-Audio-"+(int)Random.Range(2,5)).GetComponent<AudioSource>();
        ArrowAudio = GameObject.Find("Arrow-Audio").GetComponent<AudioSource>();
        RockAudio = GameObject.Find("Rock-Audio").GetComponent<AudioSource>();
        if (IntenseShooting == false&& ThreeArrows==false)
        {
            if (Manage.Sound == 0) { 
            ArrowAudio.Play();
            }
            force = Manage.force;

            float Move = (force.x + force.y) / 2;
            if (Move < 1)
            {
                Move += 4;
            }
            else if (Move >= 1 && Move <= 3)
            {
                Move += 8;
            }
            else if (Move > 3)
            {
                Move += 10;
            }

            rb.velocity = transform.right * Move;
        }else if (IntenseShooting == true&& ThreeArrows==false)
        {
            if (Manage.Sound == 0)
            {
                ArrowAudio.Play();
            }
            force.x = Random.Range(1, 12f);
            force.y = Random.Range(1, 12f);
            float Move = (force.x + force.y) ;

            rb.velocity = transform.right * Move;
        }
        else if (IntenseShooting == false && ThreeArrows == true)
        {
            if (Manage.Sound == 0)
            {
                ArrowAudio.Play();
            }
            force = Manage.force;
            float Move = (force.x + force.y) / 2;
            if (Move < 1)
            {
                Move += 4;
            }
            else if (Move >= 1 && Move <= 3)
            {
                Move += 8;
            }
            else if (Move > 3)
            {
                Move += 10;
            }
            Output[0]= Instantiate(AnotherOne, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity)as GameObject;
            Output[1] = Instantiate(AnotherOne, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
            Output[0].transform.eulerAngles = new Vector3 (0,0,Manage.AngleZ+10);
            Output[1].transform.eulerAngles = new Vector3(0, 0, Manage.AngleZ - 10);
            Output[0].GetComponent<ThreeArrow>().Force = Move;
            Output[1].GetComponent<ThreeArrow>().Force = Move;
            Output[0].GetComponent<ThreeArrow>().Up = true;
         
            rb.velocity = transform.right * Move;


        }



    }

    // Update is called once per frame
    void Update()
    {

        if (hasHit == false)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.parent = collision.transform;
        GetComponent<TrailRenderer>().enabled = false;
        hasHit = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        GetComponent<CapsuleCollider2D>().enabled = false;
        if (collision.gameObject.tag == "Enemy") {
            if (Manage.Sound == 0)
            {
                HitAudio.Play();
            }
               
            Instantiate(BloodParticle, new Vector3(BloodPoint.transform.position.x, BloodPoint.transform.position.y, transform.position.z), Quaternion.identity);
        }
        if (collision.gameObject.tag == "Ground")
        {
            if (Manage.Sound == 0)
            {
                RockAudio.Play();
            }
        }
    }
}