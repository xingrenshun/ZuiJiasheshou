using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bow : MonoBehaviour
{
  
    public float launchForce;
    public Transform shotPoint;
    public  Vector2 Direction;

  
    public GameObject point;
    GameObject[] points;
    public int numberOfPoints;
    public float spaceBetweenPoints;


  
    private Manage Manage;
    public GameObject Parent;
    private Vector2 bowPosition;
  
    
    // Start is called before the first frame update
    void Start()
    {
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
      
        points = new GameObject[numberOfPoints];
        Parent = GameObject.Find("Point-Target");
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, shotPoint.position, Quaternion.identity);
            points[i].transform.parent = Parent.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Manage.hit == false && Manage.Win == false) {
        if (Input.GetMouseButtonDown(0))
        {
             bowPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Manage.Drag == true && Manage.Win == false && EventSystem.current.currentSelectedGameObject == null)
        {

            Parent.SetActive(true);
            for (int i = 0; i < numberOfPoints; i++)
            {
                spaceBetweenPoints=((Manage.force.x + Manage.force.x)) / 2000;
                points[i].transform.position = PointPosition(i * spaceBetweenPoints);
            }
          
               

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Direction = (mousePosition - bowPosition);
           if (Direction.x <= 3)
            {
                Direction.x = 3;
            }
            transform.right = Direction;
            Manage.AngleZ = transform.eulerAngles.z;
        }
        else { Parent.SetActive(false); }

        }else if (Manage.hit == true)
        {
            Parent.SetActive(false);
           
        }
    }


    Vector2 PointPosition(float t)
    {
       launchForce = ((Manage.force.x+ Manage.force.x)+100)/1.5f;

        Vector2 position=(Vector2)shotPoint.position+(Direction.normalized*launchForce*t)+(0.5f )* Physics2D.gravity * (t * t);
        
        return position;
    } 
}
