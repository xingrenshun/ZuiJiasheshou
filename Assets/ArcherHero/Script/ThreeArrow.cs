using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeArrow : MonoBehaviour
{
    public float Force;
    private bool hasHit;
    public float power = 10f;
    private Rigidbody2D rb;
 
    private Manage Manage;
    public GameObject BloodParticle;
    public GameObject BloodPoint;

    private AudioSource HitAudio;
    private AudioSource RockAudio;
    private AudioSource ArrowAudio;

    public bool Up;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
        HitAudio = GameObject.Find("Hit-Audio-" + (int)Random.Range(2, 5)).GetComponent<AudioSource>();
        ArrowAudio = GameObject.Find("Arrow-Audio").GetComponent<AudioSource>();
        RockAudio = GameObject.Find("Rock-Audio").GetComponent<AudioSource>();


        ArrowAudio.Play();



        if (Up == false)
        {
          
          
            rb.velocity = transform.right * Force;

        }
        else
        if (Up == true)
        {
         
          
            rb.velocity = transform.right *Force;

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
        if (collision.gameObject.tag == "Enemy")
        {
            HitAudio.Play();
            Instantiate(BloodParticle, new Vector3(BloodPoint.transform.position.x, BloodPoint.transform.position.y, transform.position.z), Quaternion.identity);
        }
        if (collision.gameObject.tag == "Ground")
        {
            RockAudio.Play();

        }
    }
}