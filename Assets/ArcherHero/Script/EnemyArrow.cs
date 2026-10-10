using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    private bool hasHit;
    public GameObject Fire;
    public GameObject FireParticle;
    private Rigidbody2D rb;
    public Vector2 force;
  
    public GameObject BloodParticle;
    public GameObject BloodPoint;
    private Animator Anim;
    private Manage Manage;
    private AudioSource HitAudio;
    private AudioSource RockAudio;
    private AudioSource ArrowAudio;
    private AudioSource ShieldAudio;
     public bool SecondaryArrow;
    public GameObject SecondaryArrowPrefab;


    public float RotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        HitAudio = GameObject.Find("Hit-Audio-1").GetComponent<AudioSource>();

        RockAudio = GameObject.Find("Rock-Audio").GetComponent<AudioSource>();
        ShieldAudio = GameObject.Find("Shield-Audio").GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();

        Manage = GameObject.Find("Manage").GetComponent<Manage>();
        ArrowAudio = GameObject.Find("Arrow-Audio").GetComponent<AudioSource>();
        if(GetComponent<Animator>() != null)
        {
            Anim = GetComponent<Animator>();
        }
        if (SecondaryArrow == false) {
            if (Manage.Sound == 0)
            {
                ArrowAudio.Play();
            }
        force.x = Manage.EnemyForceX;
        force.y = Manage.EnemyForceY;
        rb.AddForce(force * 2, ForceMode2D.Impulse);
        }else
        {
            if (Manage.Sound == 0)
            {
                ShieldAudio.Play();
            }
            rb.velocity = new Vector3(0.5f, 2f);
        }


    }

    // Update is called once per frame
    void Update()
    {

        if (hasHit == false && SecondaryArrow==false)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            print("a");
        }else
        {
            transform.eulerAngles += new Vector3(0, 0, RotateSpeed * Time.deltaTime);
            print("b");
        }


    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (Manage.Sound == 0)
            {
                HitAudio.Play();
            }
            if (Anim != null)
            {
                Anim.enabled = false;

            }
            if (Fire != null&&hasHit==false)
            {
                Instantiate(FireParticle, new Vector3(BloodPoint.transform.position.x, BloodPoint.transform.position.y, transform.position.z), Quaternion.identity);
                print("c");
            }
            GetComponent<CapsuleCollider2D>().enabled = false;
        hasHit = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
       
            Instantiate(BloodParticle, new Vector3(BloodPoint.transform.position.x, BloodPoint.transform.position.y, transform.position.z), Quaternion.identity);
            print("d");
        }
        if (coll.gameObject.name == "Ground")
        {
            if (Manage.Sound == 0)
            {
                RockAudio.Play();
            }

            if (Anim != null)
            {
                Anim.enabled = false;

            }
            if (Fire != null && hasHit == false)
            {
                Instantiate(FireParticle, new Vector3(BloodPoint.transform.position.x, BloodPoint.transform.position.y, transform.position.z), Quaternion.identity);
                print("e");
            }
            GetComponent<CapsuleCollider2D>().enabled = false;
            hasHit = true;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }
        if (coll.gameObject.name == "Shield")
        {

            Instantiate(SecondaryArrowPrefab, new Vector3(BloodPoint.transform.position.x, BloodPoint.transform.position.y, transform.position.z), Quaternion.identity);
            if (Fire != null )
            {

                Instantiate(FireParticle, new Vector3(BloodPoint.transform.position.x, BloodPoint.transform.position.y, transform.position.z), Quaternion.identity);
                print("f");
            }
            print("g");
            Destroy(gameObject);
        }
    }
}