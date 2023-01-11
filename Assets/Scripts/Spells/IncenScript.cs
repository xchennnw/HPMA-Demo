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
        setBloodValues(60, 1f, false, 5);
        decreaseBlood();
   

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 10f, 0.0f, Space.World);
    }



}
