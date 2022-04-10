using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    public int[] mList = new int[5];
    public string[] sList = new string[5];
    public GameObject circle;
    public GameObject enemy_circle;
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

    public void useSpell(Vector3 buttonPos, int n, int toKill, int btn_index)
    {
        float r = 0;
        if(n == 0)
        {
            r = 10f;
        }
        else if (n == 1)
        {
            r = 10f;
        }
        else if (n == 2)
        {
            r = 8f;
        }
        
        if(toKill == 1)
        {
            var o = Instantiate(circle, buttonPos, Quaternion.identity);
            o.transform.localScale = new Vector3(r, 1f, r);
            o.GetComponent<Circle>().buttonPos = buttonPos;
            o.GetComponent<Circle>().buttonIndex = btn_index;
            o.GetComponent<Circle>().SpellIndex = n;
            o.GetComponent<Circle>().SpellMP = getMagicValue(n);
            o.GetComponent<Circle>().SpellToKill = toKill;
        }
        else
        {
            var o = Instantiate(enemy_circle, buttonPos, Quaternion.identity);
            o.transform.localScale = new Vector3(r, 1f, r);
            o.GetComponent<ECircle>().SpellIndex = n;
            o.GetComponent<ECircle>().SpellMP = getMagicValue(n);
            o.GetComponent<ECircle>().SpellToKill = toKill;
        }
        
  

    }
    public void execSpell(Vector3 execPos, int n, int toKill)
    {
        
        if (n == 0)
        {
            Vector3 pos = new Vector3(execPos.x, 2f, execPos.z);
            var o = Instantiate(incendio, pos, Quaternion.identity);
            o.GetComponent<IncenScript>().toKill = toKill;
        }
        else if (n == 1)
        {
            Vector3 pos = new Vector3(execPos.x, 13f, execPos.z);
            var o = Instantiate(atmos, pos, Quaternion.identity);
            o.GetComponent<AtmosScript>().toKill = toKill;
        }
        else if (n == 2)
        {
            Vector3 pos = new Vector3(execPos.x, 2f, execPos.z);
            var o = Instantiate(expul, pos, Quaternion.identity);
            o.GetComponent<ExpulScript>().toKill = toKill;
        }

       
    }


}

