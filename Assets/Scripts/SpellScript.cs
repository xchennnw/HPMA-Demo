using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{

    public int toKill;
    protected GameObject tk;
    protected RoleController toKillRoleScript;
    protected int bloodDecValue;
    protected float bloodDecTimeInterval;
    protected bool bloodDecOnlyOnce;
    protected float attackRadium;
    protected bool switchToKill = false;
    protected List<GameObject> targetObjs = new List<GameObject>();

    public float distance(GameObject obj)
    {
        float dx = obj.transform.position.x - transform.position.x;
        float dy = obj.transform.position.z - transform.position.z;
        float d = Mathf.Sqrt(dx * dx + dy * dy);
        return d;
    }

    public void setup()
    {
        if((toKill == 1 && !switchToKill) || (toKill == 0 && switchToKill))
        {
            tk = GameObject.Find("Enemy");
            toKillRoleScript =  tk.GetComponent<RoleController>();
        }
        else
        {
            tk = GameObject.Find("Player");
            toKillRoleScript = tk.GetComponent<RoleController>();
        }
    }

    public void setBloodValues(int b, float f, bool once, float r)
    {
        bloodDecValue = b;
        bloodDecTimeInterval = f;
        bloodDecOnlyOnce = once;
        attackRadium = r;
    }


    public void decreaseBlood()
    {
        if (bloodDecOnlyOnce)
        {
            decreaseBloodOnce();
        }
        else
        {
            InvokeRepeating("decreaseBloodOnce", 0f, bloodDecTimeInterval);
        }
    }

    protected virtual void decreaseBloodOnce()
    {
        UpdateTargetObjs();
        if (distance(tk) < attackRadium)
        {                
            toKillRoleScript.TakeDamage(bloodDecValue);
        }
        
        for (int i = 0; i < targetObjs.Count; i++)
        {
            if (targetObjs[i] == null) continue;
            AnimalScript temp = targetObjs[i].GetComponent<AnimalScript>();
            temp.TakeDamage(bloodDecValue);
        }
    }

    public void UpdateTargetObjs()
    {
        targetObjs.Clear();
        List<GameObject> tkTeam = toKillRoleScript.team;       
        for (int i = 0; i < tkTeam.Count; i++)
        {            
            if (distance(toKillRoleScript.team[i]) < attackRadium)
            {
                targetObjs.Add(toKillRoleScript.team[i]);
            }
        }
    }
  
}
