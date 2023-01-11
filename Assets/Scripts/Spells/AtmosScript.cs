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
        setBloodValues(40, 1f, false, 5);
        decreaseBlood();
        
    }

    // Update is called once per frame
    void Update()
    {
        float minDist = distance(tk);
        GameObject target = tk;
        for (int i = 0; i < targetObjs.Count; i++)
        {
            if (targetObjs[i] == null) continue;
            if (distance(targetObjs[i]) < minDist)
            {
                target = targetObjs[i];
            }
        }
        Vector3 toKillPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, toKillPos, 4 * Time.deltaTime);
    }



}
