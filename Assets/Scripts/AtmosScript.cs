using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosScript : SpellScript
{
    float lifetime = 15f;
    

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifetime);
        setup();
        if (toKill == 1)
        {
            tk = e;
            InvokeRepeating("decreaseBlood_Enemy", 0f, 1f);
        }
        else if (toKill == 0)
        {
            tk = p;
            InvokeRepeating("decreaseBlood_Player", 0f, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toKillPos = new Vector3(tk.transform.position.x, transform.position.y, tk.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, toKillPos, 2* Time.deltaTime);
    }


    public override void decreaseBlood_Enemy()
    {
        if (distance(e) < 5)
        {
            EnemyScript.setTopText("-40");
            EnemyScript.blood -= 40;
        }
    }
    public override void decreaseBlood_Player()
    {
        if (distance(p) < 5)
        {
            PlayerScript.setTopText("-40");
            PlayerScript.blood -= 40;
        }
    }
}
