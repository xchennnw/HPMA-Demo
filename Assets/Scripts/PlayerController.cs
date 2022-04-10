using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public Text text1;
    public Text MagicPointText;
    public Text TopText;

    public Slider slider1;
    public Slider MagicPointslider;
    Vector3 targetPos;
    Plane plane = new Plane(Vector3.up, 0);
    public int blood;
    public int magicPoint;
    public bool ChoosingShootDir;
    public bool CanNotMove;
    float speed = 0.04f;
    float TopTextStartTime;


    // Start is called before the first frame update
    void Start()
    {
        targetPos = transform.position;
        ChoosingShootDir = false;
        CanNotMove = false;
        blood = 1400;
        magicPoint = 0;
        TopText.text = "";
        InvokeRepeating("increaseMagicPoint", 0f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanNotMove)
        {
            Move();
        }
       
        text1.text = ""+blood;
        slider1.value = blood;
        MagicPointText.text = "" + magicPoint;
        MagicPointslider.value = magicPoint;

        if (Time.time - TopTextStartTime > 0.5f)
        {
            TopText.text = "";
        }
    }

    void increaseMagicPoint()
    {
        if(magicPoint<10)
            magicPoint++;       
    }

    public void setTopText(string s)
    {
        TopText.text = s;
        TopTextStartTime = Time.time;
    }

    bool onLeftGround(Vector3 v)
    {
        if (v.x <= -1 && v.x >=-49 && v.z<=24 && v.z>=-24)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void Move()
    {
        if (!ChoosingShootDir)
        {
            float distance;
            Vector3 temp;
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (plane.Raycast(ray, out distance))
                {
                    temp = ray.GetPoint(distance);
                    if (onLeftGround(temp))
                    {
                        targetPos = new Vector3(temp.x, transform.position.y, temp.z);
                    }
                }

            }
        }

        if (Input.GetKey("up") || Input.GetKey("w"))
        {           
            if(transform.position.z < 24)
                transform.Translate(Vector3.forward * speed);
            targetPos = transform.position;
        }
        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            if(transform.position.z > -24)
                transform.Translate(Vector3.back * speed);
            targetPos = transform.position;
        }
        if (Input.GetKey("left") || Input.GetKey("a"))
        {          
            if (transform.position.x > -49)
                transform.Translate(Vector3.left * speed);
            targetPos = transform.position;
        }
        if (Input.GetKey("right") || Input.GetKey("d"))
        {                       
            if(transform.position.x < -1)
                transform.Translate(Vector3.right * speed);
            targetPos = transform.position;
        }
       
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed);
          
    }
}
