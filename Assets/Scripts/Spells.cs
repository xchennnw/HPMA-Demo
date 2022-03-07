using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    public int[] mList = new int[5];
    public string[] sList = new string[5];
    public GameObject circle;
    public GameObject incendio;
    public GameObject atmos;
    public GameObject expul;

    // Start is called before the first frame update
    void Start()
    {
        setMagicValueList();
        setNameList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setMagicValueList()
    {
        mList[0] = 4;
        mList[1] = 4;
        mList[2] = 2;
    }

    void setNameList()
    {
        sList[0] = "Incendio";
        sList[1] = "Atmosperic Charm";
        sList[2] = "Expulso";
    }

    public int getMagicValue(int i)
    {
        return mList[i];
    }
    public string getName(int i)
    {
        return sList[i];
    }

    public void useSpell(Vector3 buttonPos, int n, int toKill)
    {
        if(n == 0)
        {
            useIncendio(buttonPos, toKill);
        }
        else if (n == 1)
        {
            useAtmospheric(buttonPos, toKill);
        }
        else if (n == 2)
        {
            useExpulso(buttonPos, toKill);
        }
    }
    public void execSpell(Vector3 execPos, int n, int toKill)
    {
        if (n == 0)
        {
            execIncendio(execPos, toKill);
        }
        else if (n == 1)
        {
            execAtmospheric(execPos, toKill);
        }
        else if (n == 2)
        {
            execExpulso(execPos, toKill);
        }
    }

    void useIncendio(Vector3 buttonPos, int toKill)
    {
        var o = Instantiate(circle, buttonPos, Quaternion.identity);
        o.transform.localScale = new Vector3(10f, 1f, 10f);
        o.GetComponent<Circle>().buttonPos = buttonPos;
        o.GetComponent<Circle>().SpellIndex = 0;
        o.GetComponent<Circle>().SpellMP = getMagicValue(0);
        o.GetComponent<Circle>().SpellToKill = toKill;
    }
    void execIncendio(Vector3 execPos, int toKill)
    {
        Vector3 pos = new Vector3(execPos.x, 2f, execPos.z);
        var o = Instantiate(incendio, pos, Quaternion.identity);
        o.GetComponent<IncenScript>().toKill = toKill;
    }

    void useAtmospheric(Vector3 buttonPos, int toKill)
    {
        var o = Instantiate(circle, buttonPos, Quaternion.identity);
        o.transform.localScale = new Vector3(10f, 1f, 10f);
        o.GetComponent<Circle>().buttonPos = buttonPos;
        o.GetComponent<Circle>().SpellIndex = 1;
        o.GetComponent<Circle>().SpellMP = getMagicValue(1);
        o.GetComponent<Circle>().SpellToKill = toKill;
    }
    void execAtmospheric(Vector3 execPos, int toKill)
    {
        Vector3 pos = new Vector3(execPos.x, 13f, execPos.z);
        var o = Instantiate(atmos, pos, Quaternion.identity);
        o.GetComponent<AtmosScript>().toKill = toKill;
    }

    void useExpulso(Vector3 buttonPos, int toKill)
    {
        var o = Instantiate(circle, buttonPos, Quaternion.identity);
        o.transform.localScale = new Vector3(8f, 1f, 8f);
        o.GetComponent<Circle>().buttonPos = buttonPos;
        o.GetComponent<Circle>().SpellIndex = 2;
        o.GetComponent<Circle>().SpellMP = getMagicValue(2);
        o.GetComponent<Circle>().SpellToKill = toKill;
    }
    void execExpulso(Vector3 execPos, int toKill)
    {
        Vector3 pos = new Vector3(execPos.x, 2f, execPos.z);
        var o = Instantiate(expul, pos, Quaternion.identity);
        o.GetComponent<ExpulScript>().toKill = toKill;
    }
}

