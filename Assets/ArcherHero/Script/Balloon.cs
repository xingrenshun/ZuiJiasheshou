using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    private float Speed;
    public GameObject Particle;
    public GameObject Coin;
    private Manage Manage;
    public GameObject Target;
    private AudioSource BalloonAudio;
    void Start()
    {
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
        Speed = Random.Range(1.5f, 2.5f);
        BalloonAudio = GameObject.Find("Balloon-Audio").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, Speed*Time.deltaTime, 0);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Arrow") {
            if (Manage.Sound == 0)
            {
                BalloonAudio.Play();
            }
            Instantiate(Coin, new Vector3(Target.transform.position.x, Target.transform.position.y, transform.position.z), Quaternion.identity) ;
            Instantiate(Particle, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Manage.Coin += 1;
            Manage.SaveScore();
            Manage.ShowScores();
            Destroy(gameObject);
        }
    }
}
