using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    Plane plane = new Plane(Vector3.up, 0);
    float timeNotMove;
    public Vector3 buttonPos;
    public int SpellIndex;
    public int SpellMP;
    public int SpellToKill;
    Spells SpellScript;
    PlayerController PlayerScript;
   
    // Start is called before the first frame update
    void Start()
    {
        timeNotMove = Time.time + 1.4f;
        GameObject s = GameObject.Find("SpellManager");
        SpellScript = s.GetComponent<Spells>();
        GameObject p = GameObject.Find("Player");
        PlayerScript = p.GetComponent<PlayerController>();

        PlayerScript.ChoosingShootDir = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        float distance;
        Vector3 temp;
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance))
            {
                temp = ray.GetPoint(distance);
                transform.position = new Vector3(temp.x, transform.position.y, temp.z);
            }

        }
        else
        {
            if (Time.time > timeNotMove)
            {
                Vector3 v = transform.position;
                if (v.x < 50 && v.x > -50 && v.z < 25 && v.z > -25)
                {                   
                    SpellScript.execSpell(v, SpellIndex, SpellToKill);
                }
                if (!(v.x <= buttonPos.x + 4 && v.x >= buttonPos.x - 4 && v.z <= buttonPos.z - 1 && v.z >= buttonPos.z - 12))
                {
                    PlayerScript.magicPoint -= SpellMP;
                }
                PlayerScript.ChoosingShootDir = false;
                Destroy(this.gameObject);               
            }
                          
        }
    }
}
