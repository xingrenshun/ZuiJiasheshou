using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragNShoot : MonoBehaviour
{
    public GameObject barlife;
    public GameObject Mylife;
    //private Animator BowAnim;
    private Animator PlayerAnim;
    //public GameObject Bow;
    public GameObject DeadPlayer;
 
    public Rigidbody2D rb;
    Line tl;
    public Vector2 minPower;
    public Vector2 maxPower;
    Camera Cam;
    Vector2 force;
    Vector3 startPoint;
    Vector3 endPoint;
  

    public GameObject ThreeShotSprite;
    private Manage Manage;

    private bool Click;
    private int HitNumber;

    private bool RunBow;
    private float HitTimer;

    public GameObject ShotPoint;
    // Start is called before the first frame update
    void Start()
    {
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
        //BowAnim = Bow.GetComponent<Animator>();
        PlayerAnim = GameObject.Find("Player").GetComponent<Animator>();
        Cam = Camera.main;
        tl = GetComponent<Line>();
        Manage.shotpoint = ShotPoint;
        Manage.SelectLevel = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Manage.Win == false)
        {
            if (Manage.Three == true)
            {
                ThreeShotSprite.SetActive(true);
            }
            else
            {
                ThreeShotSprite.SetActive(false);
            }
            if (Manage.hit == false)
            {
                if (RunBow == false && Click == false)
                {
                    //Bow.SetActive(false);
                    PlayerAnim.SetInteger("Player", 0);
                }
                else if (RunBow == true && Click == false)
                {
                    PlayerAnim.SetInteger("Player", 1);
                   // Bow.SetActive(true);
                    //BowAnim.SetInteger("Bow", 1);

                }
                else if (RunBow == true && Click == true)
                {
                    PlayerAnim.SetInteger("Player", 1);
                    //Bow.SetActive(true);
                    //BowAnim.SetInteger("Bow", 0);
                }
                if (GetInputDown())
                {
                    Vector3 inputPos = GetInputPosition();
                    startPoint = Cam.ScreenToWorldPoint(inputPos);
                    if (startPoint.y > -3.4f && IsPointerOverUIObject())
                    {
                        Click = true;
                        RunBow = true;
                        force.x = 0;
                        force.y = 0;
                        startPoint.z = 15;
                        Manage.Drag = true;
                    }
                }


                if (GetInputPressed())
                {
                    if (Click == true && Manage.Win == false)
                    {
                        Vector3 currentPoint = Cam.ScreenToWorldPoint(GetInputPosition());
                        currentPoint.z = 15;

                        tl.RenderLine(startPoint, currentPoint);
                    }
                }
                if (GetInputUp())
                {
                    if (Manage.hit == false && Click == true && Manage.Win == false)
                    {
                        //BowAnim.SetInteger("Bow", 1);
                        endPoint = Cam.ScreenToWorldPoint(GetInputPosition());
                        endPoint.z = 15;
                        force = new Vector2(Mathf.Clamp(startPoint.x + endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y + endPoint.y, minPower.y, maxPower.y));

                        force.x = Mathf.Abs(force.x);
                        force.y = Mathf.Abs(force.y);

                        tl.EndLine();

                        Manage.newArrow();
                        Manage.Drag = false;
                        StartCoroutine(idel());
                        Click = false;
                    }
                }

                if (Click == true)
                {
                    endPoint = Cam.ScreenToWorldPoint(GetInputPosition());
                    endPoint.z = 15;
                    force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
                    if (force.y > 1.5f && force.x < 0.5f)
                    {
                        force.x = 1 + force.x;
                    }
                    force.x = Mathf.Abs(force.x);
                    force.y = Mathf.Abs(force.y);

                    Manage.force = force;
                }
            }
            else if (Manage.hit == true)
            {
                Click = false;
                RunBow = false;
                tl.EndLine();
                if (HitNumber == 0)
                {
                    //PlayerAnim.SetInteger("Player", 2);
                }
                else if (HitNumber == 1)
                {
                    //PlayerAnim.SetInteger("Player", 3);
                }
                else if (HitNumber == 2)
                {
                    //PlayerAnim.SetInteger("Player", 4);
                }

                HitTimer += 1 * Time.deltaTime;

                if (Manage.Level >= 0 || Manage.Level <= 10)
                {
                    if (HitTimer >= 0.4f)
                    {
                        Manage.hit = false;
                    }
                }
                else
                {
                    if (HitTimer >= 0.05f)
                    {
                        Manage.hit = false;
                    }
                }

                /*if (HitTimer >= 0.05f)
                {
                    Manage.hit = false;
                    print("0.5");
                }*/
            }
        }else if (Manage.Win == true)
        {
            Click = false;
            RunBow = false;
            tl.EndLine();
            PlayerAnim.SetInteger("Player", 0);
        }
    }
    IEnumerator idel()
    {
        yield return new WaitForSeconds(0.05f);
        if (Click == false)
            RunBow = false;
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "EnemyArrow")
        {
            HitTimer = 0;
            if (coll.gameObject.transform.position.y >= transform.position.y + 0.13f)
            {
              Manage.Life -= 70;
                HitNumber = 2;
            }
            else if (coll.gameObject.transform.position.y >= transform.position.y - 0.3f && coll.gameObject.transform.position.y < transform.position.y + 0.13f)
            {
                Manage.Life -= 50;
                HitNumber = 0;
            }
            else if (coll.gameObject.transform.position.y < transform.position.y - 0.3f)
            {
                Manage.Life -= 35;
                HitNumber = 1;
            }

            Manage.hit = true;
            if (Manage.Life <= 0)
            {
               // Destroy(coll.gameObject);
                //GameObject Output = Instantiate(DeadPlayer, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
                //Output.GetComponent<Rigidbody2D>().velocity = new Vector3(-1, 3f);
                //Manage.Lose = true;
                //Manage.Losegame();
                //Destroy(GameObject.Find("Player"));
                Manage.LoseChanceGame();
                GameObject.Find("Player").SetActive(false);
            }
            ShowBarlife();
        }
        if (coll.gameObject.tag == "BossArrow")
        {
            HitTimer = 0;
            if (coll.gameObject.transform.position.y >= transform.position.y + 0.13f)
            {
                Manage.Life -= 90;
                HitNumber = 2;
            }
            else if (coll.gameObject.transform.position.y >= transform.position.y - 0.3f && coll.gameObject.transform.position.y < transform.position.y + 0.13f)
            {
                Manage.Life -= 70;
                HitNumber = 0;
            }
            else if (coll.gameObject.transform.position.y < transform.position.y - 0.3f)
            {
                Manage.Life -= 45;
                HitNumber = 1;
            }

            Manage.hit = true;
            if (Manage.Life <= 0)
            {
               // Destroy(coll.gameObject);
                //GameObject Output = Instantiate(DeadPlayer, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
                //Output.GetComponent<Rigidbody2D>().velocity = new Vector3(-1, 3f);
                // Manage.Lose = true;
                // Manage.Losegame();
                //Destroy(GameObject.Find("Player"));
                Manage.LoseChanceGame();
                GameObject.Find("Player").SetActive(false);
            }
            ShowBarlife();
        }
    }
    public void ShowBarlife()
    {
        float mylife = Manage.Life / 100;
        Mylife.transform.localScale = new Vector3(mylife, 1, 1);
        barlife.SetActive(true);
        StartCoroutine(Endshowbarlife());
    }
    IEnumerator Endshowbarlife()
    {
        yield return new WaitForSeconds(0.75f);
       if (Manage.hit == false)
            barlife.SetActive(false);
    }
    public void ShowBarAgainlife()
    {
        Manage.Life = 80;
        //float mylife = Manage.Life / 100;
        Mylife.transform.localScale = new Vector3(1, 1, 1);
        barlife.SetActive(true);
        StartCoroutine(Endshowbarlife());
    }
    
    // 统一的输入处理方法（支持鼠标和触摸）
    bool GetInputDown()
    {
        // 优先检查触摸输入
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
                return true;
        }
        
        // 如果没有触摸输入，检查鼠标输入
        return Input.GetMouseButtonDown(0);
    }
    
    bool GetInputPressed()
    {
        // 优先检查触摸输入
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                return true;
        }
        
        // 如果没有触摸输入，检查鼠标输入
        return Input.GetMouseButton(0);
    }
    
    bool GetInputUp()
    {
        // 优先检查触摸输入
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                return true;
        }
        
        // 如果没有触摸输入，检查鼠标输入
        return Input.GetMouseButtonUp(0);
    }
    
    Vector3 GetInputPosition()
    {
        // 优先检查触摸输入
        if (Input.touchCount > 0)
        {
            return Input.GetTouch(0).position;
        }
        
        // 如果没有触摸输入，返回鼠标位置
        return Input.mousePosition;
    }
    
    // 检查指针是否在UI对象上
    bool IsPointerOverUIObject()
    {
        if (EventSystem.current == null) return false;
        
        // 优先检查触摸输入
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    return true;
            }
            return false;
        }
        
        // 如果没有触摸输入，检查鼠标
        return EventSystem.current.currentSelectedGameObject==null;
    }
}
       