using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    Plane plane = new Plane(Vector3.up, 0);
    float timeNotMove;
    public Vector3 buttonPos;
    public int buttonIndex;
    public int SpellIndex;
    public int SpellMP;
    public int SpellToKill;
    public bool animal;
    public bool friend;
    Spells SpellScript;
    GameObject player;
    PlayerController PlayerScript;
    ButtonController ButtonScript;

    // Start is called before the first frame update
    void Start()
    {
        timeNotMove = Time.time + 0.4f;
        GameObject s = GameObject.Find("SpellManager");
        SpellScript = s.GetComponent<Spells>();
        ButtonScript = s.GetComponent<ButtonController>();
        player = GameObject.Find("Player");
        PlayerScript = player.GetComponent<PlayerController>();

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
                Vector3 pos = new Vector3(temp.x, transform.position.y, temp.z);
                if (!animal)
                {
                    transform.position = pos;
                }
                else
                {
                    if(Distance(player, pos) < 10 || !OnGround(pos))
                    {
                        transform.position = pos;
                    }
                    else
                    {
                        Vector3 d = new Vector3(pos.x - player.transform.position.x, 0, pos.z - player.transform.position.z);
                        d.Normalize();
                        Vector3 pos2 = player.transform.position + d * 10;
                        transform.position = new Vector3(pos2.x, transform.position.y, pos2.z);
                    }
                }
               
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
                if (!(v.x <= buttonPos.x + 5 && v.x >= buttonPos.x - 5&& v.z <= buttonPos.z && v.z >= buttonPos.z - 13))
                {
                    PlayerScript.magicPoint -= SpellMP;
                    if (friend)
                    {
                        ButtonScript.setNextFriend();
                        ButtonScript.setLastTime(Time.time);
                    }
                    else
                    {
                        ButtonScript.setNextSpell(buttonIndex);
                    }
                }
                PlayerScript.ChoosingShootDir = false;
                Destroy(this.gameObject);               
            }
                          
        }
    }

    public float Distance(GameObject player, Vector3 vec)
    {
        float dx = player.transform.position.x - vec.x;
        float dy = player.transform.position.z - vec.z;
        float d = Mathf.Sqrt(dx * dx + dy * dy);
        return d;
    }

    bool OnGround(Vector3 v)
    {
        if (v.x <= 49 && v.x >= -49 && v.z <= 24 && v.z >= -24)
        {
            return true;
        }  
        return false;
    }
}

