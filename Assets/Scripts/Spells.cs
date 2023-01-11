using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    public class Spell
    {
        public bool friend;
        public bool animal;
        public bool toSelf;
        public int id;
        public string name;
        public int magicValue;
        public int radius;
        public GameObject ob;

        public Spell(int i, string n, int m, int r, GameObject o)
        {
            id = i;
            name = n;
            magicValue = m;
            radius = r;
            ob = o;
        }

        public Spell(bool ani, bool self, int i, string n, int m, int r, GameObject o)
        {
            animal = ani;
            toSelf = self;
            id = i;
            name = n;
            magicValue = m;
            radius = r;
            ob = o;
        }

        public Spell(bool fri, bool ani, bool self, int i, string n, int m, int r, GameObject o)
        {
            friend = fri;
            animal = ani;
            toSelf = self;
            id = i;
            name = n;
            magicValue = m;
            radius = r;
            ob = o;
        }

    }

    private int numOfSpells = 11;
    private Spell[] spellList;
    public GameObject circle;
    public GameObject enemy_circle;
    public GameObject incendio;
    public GameObject atmos;
    public GameObject expul;
    public GameObject thunder;
    public GameObject essence;
    public GameObject erumpent;
    public GameObject protego;

    public GameObject friend1;

    public void setSpells()
    {
        spellList = new Spell[numOfSpells];
        spellList[0] = new Spell(0, "Incendio 火焰熊熊", 4, 10, incendio);
        spellList[1] = new Spell(1, "Atmosperic Charm 天气咒", 4, 10, atmos);
        spellList[2] = new Spell(2, "Expulso 飞沙走石", 2, 4, expul);
        spellList[3] = new Spell(3, "Thunder Storm 闪电风暴", 6, 100, thunder);
        spellList[4] = new Spell(false, true, 4, "Essence of Dittany 白藓香精", 4, 10, essence);
        spellList[5] = new Spell(true, false, 5, "Erumpent 毒角兽", 5, 4, erumpent);
        spellList[6] = new Spell(true, true, 6, "Protego Totalum 统统加护", 5, 14, protego);
        spellList[7] = new Spell(2, "Expulso 飞沙走石", 2, 4, expul);

        spellList[8] = new Spell(true, true, false, 8, "FriendA", 0, 4, friend1);
        spellList[9] = new Spell(true, true, false, 8, "FriendA", 0, 4, friend1);
        spellList[10] = new Spell(true, true, false, 8, "FriendA", 0, 4, friend1);
    }

    public Spell getSpell(int i)
    {
        return spellList[i];
    }

    public int getMagicValue(int i)
    {
        return spellList[i].magicValue;
    }
    public string getName(int i)
    {
        return spellList[i].name;
    }

    public void useSpell(Vector3 pos, int n, int toKill, int btn_index)
    {
        float r = spellList[n].radius;

        if (toKill == 1)
        {
            var o = Instantiate(circle, pos, Quaternion.identity);
            o.transform.localScale = new Vector3(r, 1f, r);
            o.GetComponent<Circle>().buttonPos = pos;
            o.GetComponent<Circle>().buttonIndex = btn_index;
            o.GetComponent<Circle>().SpellIndex = n;
            o.GetComponent<Circle>().SpellMP = getMagicValue(n);
            o.GetComponent<Circle>().SpellToKill = toKill;
            o.GetComponent<Circle>().animal = spellList[n].animal;
            o.GetComponent<Circle>().friend = spellList[n].friend;
        }
        else
        {
            var o = Instantiate(enemy_circle, pos, Quaternion.identity);
            o.transform.localScale = new Vector3(r, 1f, r);
            o.GetComponent<ECircle>().SpellIndex = n;
            o.GetComponent<ECircle>().SpellMP = getMagicValue(n);
            o.GetComponent<ECircle>().SpellToKill = toKill;
        }
    }

    public void execSpell(Vector3 execPos, int n, int toKill)
    {
        GameObject spell = spellList[n].ob;
        Vector3 pos = new Vector3(execPos.x, 2f, execPos.z);
        var o = Instantiate(spell, pos, Quaternion.identity);
        if(spellList[n].animal)
        {
            o.GetComponent<AnimalScript>().toKill = toKill;
        }
        else
        {
            o.GetComponent<SpellScript>().toKill = toKill;
        }                
    }
}

