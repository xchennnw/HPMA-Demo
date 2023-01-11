using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtegoScript : AnimalScript
{
    public float protectRange = 7;

    protected override void SetAbnormalTags()
    {
        AlwaysGetDamage = true;
    }

    protected override void SearchTarget()
    {
        if (health <= 0) return;
        List<GameObject> team = owner.GetComponent<RoleController>().team;
        if (Distance(owner) < protectRange)
        {
            owner.GetComponent<RoleController>().CanNotGetDamage = true;
        }
        else
        {
            owner.GetComponent<RoleController>().CanNotGetDamage = false;
        }
        for (int i = 0; i < team.Count; i++)
        {
            if (team[i].GetComponent<AnimalScript>().AlwaysGetDamage) continue;

            if (Distance(team[i]) < protectRange)
            {
                team[i].GetComponent<AnimalScript>().CanNotGetDamage = true;
            }
            else
            {
                team[i].GetComponent<AnimalScript>().CanNotGetDamage = false;
            }
        }       
    }

    public override void UpdateDeath()
    {
        if (health <= 0)
        {
            owner.GetComponent<RoleController>().CanNotGetDamage = false;
            owner.GetComponent<RoleController>().RemoveTeamMember(gameObject);
            List<GameObject> team = owner.GetComponent<RoleController>().team;                      
            
            for (int i = 0; i < team.Count; i++)
            {
                team[i].GetComponent<AnimalScript>().CanNotGetDamage = false;
            }
            Destroy(gameObject);
        }
    }
}
