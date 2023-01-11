using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderScript : SpellScript
{
    float lifetime = 8f;
    public GameObject lightning; 

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifetime);
        setup();
        setBloodValues(60, 2f, false, 50);
        decreaseBlood();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void decreaseBloodOnce()
    {
        int x = Random.Range(0, 100);
        if (!toKillRoleScript.CanNotGetDamage && (targetObjs.Count == 0 || x <= 40) && distance(tk) < attackRadium )
        {
            toKillRoleScript.TakeDamage(bloodDecValue);
            Vector3 tk_pos = tk.transform.position;
            var o = Instantiate(lightning, tk_pos, Quaternion.identity);
            return;
        }
 
        for (int i = 0; i < targetObjs.Count; i++)
        {
            if (targetObjs[i] == null) continue;
            AnimalScript temp = targetObjs[i].GetComponent<AnimalScript>();
            temp.TakeDamage(bloodDecValue);
            Vector3 tk_pos = targetObjs[i].transform.position;
            var o = Instantiate(lightning, tk_pos, Quaternion.identity);
            return;
        }
    }

}
