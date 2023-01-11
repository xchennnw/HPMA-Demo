using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpulScript : SpellScript
{
    float lifetime = 0.2f;
    float start_time;
    float fly_start_time;
    bool tk_flyaway;

    Vector3 scaleChange;
   
    protected List<GameObject> attackTargets = new List<GameObject>();
    protected Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        start_time = Time.time;
        setup();
        setBloodValues(120, 0f, true, 6);
        decreaseBlood();
        if (distance(tk) < attackRadium)
        {
            toKillRoleScript.CanNotMove = true;
            dir = new Vector3(tk.transform.position.x - transform.position.x, 0, tk.transform.position.z - transform.position.z);
            dir.Normalize();
            attackTargets.Add(tk);
            tk_flyaway = true;
        }
        for (int i = 0; i < targetObjs.Count; i++)
        {
            if (distance(targetObjs[i]) < attackRadium)
            {
                if (targetObjs[i] == null) continue;
                targetObjs[i].GetComponent<AnimalScript>().SetCanNotMove();
                Vector3 d = new Vector3(targetObjs[i].transform.position.x - transform.position.x, 0, targetObjs[i].transform.position.z - transform.position.z);
                d.Normalize();
                Vector3 target_pos = transform.position + d * 0.6f * 30;
                if (!targetObjs[i].GetComponent<AnimalScript>().CanNotGetDamage) { 
                        targetObjs[i].GetComponent<AnimalScript>().SetFly(target_pos, 2000);
                }
                attackTargets.Add(targetObjs[i]);
            }
        }
        if (attackTargets.Count > 0)
        {            
            fly_start_time = Time.time;
        }
        scaleChange = new Vector3(0.3f, 0f, 0.3f);
    }

    void Update()
    {
        transform.localScale += scaleChange;

        if (tk_flyaway && Time.time - fly_start_time < 0.1f)
        {                   
            if(onGround(tk.transform.position) && !toKillRoleScript.CanNotGetDamage)
            {
                tk.transform.Translate(dir * 0.6f);
            }                          
        }

        if(Time.time - start_time >= lifetime)
        {               
            toKillRoleScript.CanNotMove = false;
            for (int i = 0; i < attackTargets.Count; i++)
            {
                if (attackTargets[i] == null) continue;
                if (attackTargets[i].tag != "Player" && attackTargets[i].tag != "Enemy")
                {
                    attackTargets[i].GetComponent<AnimalScript>().EndCanNotMove();
                }                                 
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


}
