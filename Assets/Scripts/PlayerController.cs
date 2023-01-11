using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : RoleController
{




    public Text MagicPointText;
    public Slider MagicPointslider;
    public Text MoveTimesText;
    public int MoveTimes;

    public bool ChoosingShootDir;

    // Regular Attack
    public float attackRange = 10;
    public GameObject bullet;
    private GameObject target;
    private GameObject enemy;
    private bool hasTarget;
    private EnemyController EnemyScript;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = transform.position;
        ChoosingShootDir = false;
        CanNotMove = false;
        CanNotGetDamage = false;
        maxHealth = 1400;
        health = 1400;
        magicPoint = 0;
        MoveTimesText.text = "" + MoveTimes;
        topText.text = "";
        enemy = GameObject.Find("Enemy");
        EnemyScript = enemy.GetComponent<EnemyController>();
        c = GetComponent<MeshRenderer>().material.color;
        InvokeRepeating("increaseMagicPoint", 0f, 2f);
        InvokeRepeating("RegularAttack", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanNotMove)
        {
            Move();
        }
       
        bloodText.text = "" + health;
        bloodSlider.value = health;
        MagicPointText.text = "" + magicPoint;
        MagicPointslider.value = magicPoint;

        if (Time.time - topTextStartTime > 0.5f)
        {
            topText.text = "";
        }

        SearchRegularAttckTarget();
        SetColor();
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
        if (!ChoosingShootDir && MoveTimes>0)
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
                        MoveTimes--;
                        MoveTimesText.text = ""+MoveTimes;
                    }
                }

            }
        }

        /*

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
       */

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
          
    }

    protected void SearchRegularAttckTarget()
    {
        hasTarget = false;
        List<GameObject> tkTeam = EnemyScript.team;
        if (Distance(enemy) < attackRange)
        {
            target = enemy;
            hasTarget = true;
            return;
        }
        for (int i = 0; i < tkTeam.Count; i++)
        {
            if (Distance(tkTeam[i]) < attackRange)
            {
                target = tkTeam[i];
                hasTarget = true;
                return;
            }
        }
    }

    protected void RegularAttack()
    {
        if (hasTarget)
        {
            var o = Instantiate(bullet, transform.position, Quaternion.identity);
            o.GetComponent<BulletScript>().target_pos = target.transform.position;
            o.GetComponent<BulletScript>().target = target;
        }
    }

    public float Distance(GameObject obj)
    {
        float dx = obj.transform.position.x - transform.position.x;
        float dy = obj.transform.position.z - transform.position.z;
        float d = Mathf.Sqrt(dx * dx + dy * dy);
        return d;
    }

    public override void OnGameEnd()
    {
        GameObject.Find("EndGameManager").GetComponent<EndGame>().winOrLose = "Lose.";
        SceneManager.LoadScene("EndScene");
    }
}
