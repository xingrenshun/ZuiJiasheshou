using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public bool Loop;
    private bool up;
    public GameObject Enemy;
    private float Timer;
    public GameObject Particle;
    private BoxCollider2D Collider;
    private Manage Manage;
    private bool ChangeScale;

    void Start()
    {
        transform.localScale = new Vector3(0, 0, 0);
        Collider = GetComponent<BoxCollider2D>();
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
        StartCoroutine(ActiveEnemy());

     
    }
    IEnumerator ActiveEnemy()
    {
        yield return new WaitForSeconds(0.5f);
        ChangeScale = true;
        Instantiate(Particle, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        if (ChangeScale == true)
        {
            transform.localScale += new Vector3(1f * Time.deltaTime, 1f * Time.deltaTime, 0);
            if (transform.localScale.x >= 0.8f)
            {
                transform.localScale = new Vector3(0.8f, 0.8f, 1);
                ChangeScale = false;
                //changed
            }
           /* if (transform.localScale.x >= 0.6f)
            {
                transform.localScale = new Vector3(0.6f, 0.6f, 1);
                ChangeScale = false;
                //changed
            }*/
        }
        if (Loop == true)
        {
            if (up == false)
            {
                transform.position -= new Vector3(0, 1f * Time.deltaTime, 0);
                if (transform.position.y <= -3)
                {
                    up = true;
                }
            }
            else if (up == true)
            {
                transform.position += new Vector3(0, 1f * Time.deltaTime, 0);
                if (transform.position.y >= 3)
                {
                    up = false;
                }
            }
        
        }



        if (Enemy == null)
        {
            Collider.enabled = false;
            Timer += 1 * Time.deltaTime;
            if (Timer >= 3)
            {
                Manage.Enemy -= 1;
                Instantiate(Particle, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                Destroy(gameObject);
                print("enemy -1");
            }
        }
    }
}
