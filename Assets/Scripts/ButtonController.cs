using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;

    GameObject e;
    int[] curr = new int[4];

    Spells SpellScript;
    PlayerController PlayerScript;

    // Start is called before the first frame update
    void Start()
    {
        GameObject s = GameObject.Find("SpellManager");
        SpellScript = s.GetComponent<Spells>();
        GameObject p = GameObject.Find("Player");
        PlayerScript = p.GetComponent<PlayerController>();

        curr[0] = 0;
        curr[1] = 1;
        curr[2] = 2;
        curr[3] = 1;
        setButtonText();
    }

    // Update is called once per frame
    void Update()
    {
        setColors();
        setButtonText();
    }

    void OnEnable()
    {
        //Register Button Events
        button1.onClick.AddListener(() => buttonCallBack(button1));
        button2.onClick.AddListener(() => buttonCallBack(button2));
        button3.onClick.AddListener(() => buttonCallBack(button3));
        button4.onClick.AddListener(() => buttonCallBack(button4));
    }

    public void setCurrSpells(int x)
    {
        
        int n = curr.Length;
        for(int i=x; i<n-1; i++)
        {
            curr[i] = curr[i + 1];
        }
        curr[n - 1] = Random.Range(0, 3);
        setButtonText();


    }
    public void setButtonText()
    {
        button1.GetComponentInChildren<Text>().text = SpellScript.getName(curr[0]) + " (" +
           SpellScript.getMagicValue(curr[0])+")";
        button2.GetComponentInChildren<Text>().text = SpellScript.getName(curr[1]) + " (" +
            SpellScript.getMagicValue(curr[1]) + ")";
        button3.GetComponentInChildren<Text>().text = SpellScript.getName(curr[2]) + " (" +
            SpellScript.getMagicValue(curr[2]) + ")";
        button4.GetComponentInChildren<Text>().text = SpellScript.getName(curr[3]) + " (" +
            SpellScript.getMagicValue(curr[3]) + ")";
    }
    void setColors()
    {
        int m = PlayerScript.magicPoint;
        Color a = new Color(0.72f,0.71f,0.88f);
        Color b = new Color(0.5f, 0.5f, 0.5f);
           
        if (SpellScript.getMagicValue(curr[0]) <= m)
            button1.GetComponent<Image>().color = a;
        else
            button1.GetComponent<Image>().color = b;

        if (SpellScript.getMagicValue(curr[1]) <= m)
            button2.GetComponent<Image>().color = a;
        else
            button2.GetComponent<Image>().color = b;

        if (SpellScript.getMagicValue(curr[2]) <= m)
            button3.GetComponent<Image>().color = a;
        else
            button3.GetComponent<Image>().color = b;

        if (SpellScript.getMagicValue(curr[3]) <= m)
            button4.GetComponent<Image>().color = a;
        else
            button4.GetComponent<Image>().color = b;

    }
    private void buttonCallBack(Button buttonPressed)
    {
        int m = PlayerScript.magicPoint;
        Vector3 buttonPos;
        if (buttonPressed == button1)
        {                      
            if(SpellScript.getMagicValue(curr[0]) <= m)
            {
                buttonPos = new Vector3(-15f, 4f, -27f);
                SpellScript.useSpell(buttonPos, curr[0], 1, 0);
            }                     
        }
        if (buttonPressed == button2)
        {
            if (SpellScript.getMagicValue(curr[1]) <= m)
            {
                buttonPos = new Vector3(-5f, 4f, -27f);
                SpellScript.useSpell(buttonPos, curr[1], 1, 1);
            }
        }

        if (buttonPressed == button3)
        {
            if (SpellScript.getMagicValue(curr[2]) <= m)
            {
                buttonPos = new Vector3(5f, 4f, -27f);
                SpellScript.useSpell(buttonPos, curr[2], 1, 2);
            }               
        }

        if (buttonPressed == button4)
        {
            if (SpellScript.getMagicValue(curr[3]) <= m)
            {
                buttonPos = new Vector3(15f, 4f, -27f);
                SpellScript.useSpell(buttonPos, curr[3], 1, 3);
            }               
        }
    }
}
