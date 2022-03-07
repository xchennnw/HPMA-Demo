using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellScript : MonoBehaviour
{
    public int toKill;
    public GameObject e;
    public GameObject p;
    public GameObject tk;
    public PlayerController PlayerScript;
    public EnemyController EnemyScript;

    public float distance(GameObject obj)
    {
        float dx = obj.transform.position.x - transform.position.x;
        float dy = obj.transform.position.z - transform.position.z;
        float d = Mathf.Sqrt(dx * dx + dy * dy);
        return d;
    }

    public void setup()
    {
        e = GameObject.Find("Enemy");
        EnemyScript = e.GetComponent<EnemyController>();
        p = GameObject.Find("Player");
        PlayerScript = p.GetComponent<PlayerController>();
    }

    public abstract void decreaseBlood_Enemy();

    public abstract void decreaseBlood_Player();
}
