using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Vector3 target_pos;
    public GameObject target;
    public int attack = 10;
    public int speed = 70;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target_pos, speed * Time.deltaTime);       
        if(target != null && Distance(target.transform.position) <= 1f)
        {
            if(target.tag == "Player" || target.tag == "Enemy")
            {
                target.GetComponent<RoleController>().TakeDamage(attack);
            }
            else
            {
                target.GetComponent<AnimalScript>().TakeDamage(attack);
            }
        }
        if (Distance(target_pos) <= 1f)
        {
            Destroy(this.gameObject);
        }
    }

    public float Distance(Vector3 pos)
    {
        float dx = pos.x - transform.position.x;
        float dy = pos.z - transform.position.z;
        float d = Mathf.Sqrt(dx * dx + dy * dy);
        return d;
    }
}
