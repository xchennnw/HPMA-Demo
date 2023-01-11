using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendScript : AnimalScript
{
    public float attackRange = 10;
    public GameObject bullet;
    private GameObject target;
    private bool hasTarget;
    protected override void ChangeTarget()
    {
        if (!CanNotMove && !hasTarget)
        {
            target_pos = tk.transform.position;
            agent.destination = target_pos;
        }
    }
    protected override void SearchTarget()
    {
        hasTarget = false;
        List<GameObject> tkTeam = toKillRoleScript.team;
        if (Distance(tk) < attackRange)
        {
            target = tk;
            hasTarget = true;
            return;
        }
        for (int i = 0; i < tkTeam.Count; i++)
        {
            if (Distance(toKillRoleScript.team[i]) < attackRange)
            {
                target = toKillRoleScript.team[i];
                hasTarget = true;
                return;
            }
        }
    }

    protected override void Attack()
    {
        if (hasTarget)
        {
            var o = Instantiate(bullet, transform.position, Quaternion.identity);
            o.GetComponent<BulletScript>().target_pos = target.transform.position;
            o.GetComponent<BulletScript>().target = target;
        }
    }


}
