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
    public Button buttonFriend;

    int[] cards = new int[8];
    int[] curr = new int[4];
    int[] notInUse = new int[4];

    Spells SpellScript;
    PlayerController PlayerScript;

    private float lastFriendTime;
    int[] friendCards = new int[3];
    int currFriend;
    // Start is called before the first frame update
    void Start()
    {
        GameObject s = GameObject.Find("SpellManager");
        SpellScript = s.GetComponent<Spells>();
        GameObject p = GameObject.Find("Player");
        PlayerScript = p.GetComponent<PlayerController>();

        cards[0] = 0;
        cards[1] = 1;
        cards[2] = 2;
        cards[3] = 3;
        cards[4] = 4;
        cards[5] = 5;
        cards[6] = 6;
        cards[7] = 7;

        friendCards[0] = 8;
        friendCards[1] = 9;
        friendCards[2] = 10;
        currFriend = 0;

        lastFriendTime = Time.time;
        SpellScript.setSpells();
        setFirstFourSpells();
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
        buttonFriend.onClick.AddListener(() => buttonCallBack(buttonFriend));
    }

    public void setFirstFourSpells()
    {
        List<int> arr = new List<int>();
        for (int i = 0; i < 8; i++)
        {
            arr.Add(cards[i]);
        }
        int l = 0;
        for (int i = 0; i < 4; i++)
        {
            l = arr.Count;
            int x = Random.Range(0, l);
            curr[i] = arr[x];
            arr.RemoveAt(x);
        }
        for (int i = 0; i < 4; i++)
        {
            notInUse[i] = arr[i];
        }
        Debug.Log("curr:" + curr[0] + "," + curr[1] + "," + curr[2] + "," + curr[3] + ",");
    }

    public void setNextFriend()
    {
        currFriend++;
    }

    public void setLastTime(float time)
    {
        lastFriendTime = time;
    }

    public void setNextSpell(int x)
    {
        int cardUsed = curr[x];
        for (int i = x; i < 3; i++)
        {
            curr[i] = curr[i + 1];
        }
        int w = Random.Range(0, 4);
        curr[3] = notInUse[w];
        notInUse[w] = cardUsed;
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
        if(currFriend < 3)
        {
            buttonFriend.GetComponentInChildren<Text>().text = SpellScript.getName(friendCards[currFriend]);
        }
        
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

        if (Time.time - lastFriendTime > 10 && currFriend < 3)
            buttonFriend.GetComponent<Image>().color = a;
        else
            buttonFriend.GetComponent<Image>().color = b;

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

        if (buttonPressed == buttonFriend)
        {
            if(Time.time - lastFriendTime > 10 && currFriend < 3)
            {
                buttonPos = new Vector3(-42f, 4f, -27f);
                SpellScript.useSpell(buttonPos, friendCards[currFriend], 1, 4);
            }                       
        }
    }
}
