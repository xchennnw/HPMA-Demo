using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceScript : SpellScript
{

    float lifetime = 4f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifetime);
        switchToKill = true;
        setup();        
        setBloodValues(-60, 1f, false, 5);
        decreaseBlood();


    }
}