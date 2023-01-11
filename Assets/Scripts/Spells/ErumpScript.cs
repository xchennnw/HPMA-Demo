using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErumpScript : AnimalScript
{
    protected override void Attack()
    {
        if (chaseCharacter)
        {
            if (Distance(tk) < 1)
            {
                toKillRoleScript.TakeDamage(attack);
                toKillRoleScript.CanNotMove = true;
                StartCoroutine(toKillRoleScript.RemoveBuff());
            }
        }
    }  
}
