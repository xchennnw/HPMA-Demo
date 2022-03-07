using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpulScript : SpellScript
{
    float lifetime = 0.3f;
    float starttime;
    float Fly_starttime;
    bool flyaway;
    Vector3 dir;
    Vector3 scaleChange;

    // Start is called before the first frame update
    void Start()
    {
        starttime = Time.time;

        setup();

        scaleChange = new Vector3(0.3f, 0f, 0.3f);

        if (toKill == 1)
        {
            tk = e;
            decreaseBlood_Enemy();
            EnemyScript.CanNotMove = true;
            dir = new Vector3(e.transform.position.x - transform.position.x, 0, e.transform.position.z - transform.position.z);
            dir.Normalize();
        }
        else if (toKill == 0)
        {
            tk = p;
            decreaseBlood_Player();
            PlayerScript.CanNotMove = true;
            dir = new Vector3(p.transform.position.x - transform.position.x, 0, p.transform.position.z - transform.position.z);
            dir.Normalize();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale+= scaleChange;

        if (flyaway && Time.time - Fly_starttime<0.1f && onGround(tk.transform.position))
        {
            tk.transform.Translate(dir* 0.6f);
        }

        if(Time.time - starttime >= lifetime)
        {
            if (toKill == 1)
            {               
                EnemyScript.CanNotMove = false;
            }
            else if (toKill == 0)
            {
                PlayerScript.CanNotMove = false;
            }
            Destroy(this.gameObject);
        }
    }

    bool onGround(Vector3 v)
    {
       
        if(toKill == 1)
        {
            if (v.x <= 49 && v.x >= 1 && v.z <= 24 && v.z >= -24)
            {
                return true;
            }          
        }
        else
        {
            if (v.x <= -1 && v.x >= -49 && v.z <= 24 && v.z >= -24)
            {
                return true;
            }
        }
        return false;
    }


    public override void decreaseBlood_Enemy()
    {
        if (distance(e) < 4)
        {
            EnemyScript.setTopText("-120");
            EnemyScript.blood -= 120;
            flyaway = true;
            Fly_starttime = Time.time;
        }
    }
    public override void decreaseBlood_Player()
    {
        if (distance(p) < 4)
        {
            PlayerScript.setTopText("-120");
            PlayerScript.blood -= 120;
            flyaway = true;
            Fly_starttime = Time.time;
        }
    }
}
