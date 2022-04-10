using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Text text2;
    public Slider slider2;
    public Text TopText;
    public int blood;
    float TopTextStartTime;
 
    bool pause;
    public bool CanNotMove;
    Vector3 targetPos;

    public int magicPoint;
    Spells SpellScript;
    PlayerController PlayerScript;
    GameObject p;
    int num_of_spells = 3;

    public GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        blood = 1400;
        TopText.text = "";
        pause = true;

        GameObject s = GameObject.Find("SpellManager");
        SpellScript = s.GetComponent<Spells>();
        p = GameObject.Find("Player");
        PlayerScript = p.GetComponent<PlayerController>();

        InvokeRepeating("ChangePauseState", 2f, 2.5f);
        InvokeRepeating("ChangeTarget", 0f, 3.5f);
        InvokeRepeating("increaseMagicPoint", 0f, 2f);
        InvokeRepeating("Attack", 0f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        text2.text = "" + blood;
        slider2.value = blood;

        if (Time.time - TopTextStartTime > 0.5f)
        {
            TopText.text = "";
        }

        if (!pause && !CanNotMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 8*Time.deltaTime);
        }
    }

    void increaseMagicPoint()
    {
        if (magicPoint < 10)
            magicPoint++;
    }

    void ChangePauseState()
    {
        pause = !pause;
    }

    void ChangeTarget()
    {
        float x = Random.Range(1, 49);
        float z = Random.Range(-24, 24);
        targetPos = new Vector3(x, transform.position.y, z);
    }

    public void setTopText(string s)
    {
        TopText.text = s;
        TopTextStartTime = Time.time;
    }

    void Attack()
    {
        if (magicPoint < 4) return;
        int x = Random.Range(0, num_of_spells);
        Vector3 v = p.transform.position;
        SpellScript.useSpell(v, x, 0, -1);
        magicPoint -= SpellScript.getMagicValue(x);
        var o = Instantiate(cube, transform.position, Quaternion.identity);
        o.GetComponent<CubeScript>().target_pos = v;

    }



}

