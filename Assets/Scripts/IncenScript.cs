using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncenScript : SpellScript
{

    float lifetime = 4f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifetime);
        setup();
        if (toKill == 1)
        {
            InvokeRepeating("decreaseBlood_Enemy", 0f, 1f);
        }
        else if(toKill == 0)
        {
            InvokeRepeating("decreaseBlood_Player", 0f, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 10f, 0.0f, Space.World);
    }


    public override void decreaseBlood_Enemy()
    {
        if (distance(e) < 5)
        {
            EnemyScript.setTopText("-60");
            EnemyScript.blood -= 60;
        }                    
    }

    public override void decreaseBlood_Player()
    {
        if (distance(p) < 5)
        {
            PlayerScript.setTopText("-60");
            PlayerScript.blood -= 60;
        }           
    }
}
