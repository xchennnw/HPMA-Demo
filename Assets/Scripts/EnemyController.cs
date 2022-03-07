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

    // Start is called before the first frame update
    void Start()
    {
        blood = 2400;
        TopText.text = "";
        pause = true;

        InvokeRepeating("ChangePauseState", 2f, 2.5f);
        InvokeRepeating("ChangeTarget", 0f, 3.5f);
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

  
}

