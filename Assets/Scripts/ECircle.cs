using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECircle : MonoBehaviour
{
    public Vector3 playerPos;
    public int SpellIndex;
    public int SpellMP;
    public int SpellToKill;
    GameObject p;
    Spells SpellScript;
    PlayerController PlayerScript;
    float starttime;

    // Start is called before the first frame update
    void Start()
    {
        starttime = Time.time;
        GameObject s = GameObject.Find("SpellManager");
        SpellScript = s.GetComponent<Spells>();
        p = GameObject.Find("Player");
        PlayerScript = p.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - starttime >= 1f)
        {
            Vector3 v = transform.position;
            SpellScript.execSpell(v, SpellIndex, SpellToKill);
            Destroy(this.gameObject);
        }

    }
}
