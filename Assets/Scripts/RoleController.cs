using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleController : MonoBehaviour
{
    public Text bloodText;
    public Text topText;
    public Slider bloodSlider;

    protected Vector3 targetPos;
    public Plane plane = new Plane(Vector3.up, 0);
    public int maxHealth = 1400;
    protected int health;
    public int magicPoint;

    // Abnormal States
    public bool CanNotMove;
    public bool CanNotGetDamage;

    protected float speed = 14f;
    protected float topTextStartTime;
    protected Color c;

    public List<GameObject> team = new List<GameObject>();


    void increaseMagicPoint()
    {
        if (magicPoint < 10)
            magicPoint++;
    }

    protected virtual void SetColor()
    {
        if (CanNotMove)
        {
            GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else if (CanNotGetDamage)
        {
            GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = c;
        }
    }

    public void setTopText(string s)
    {
        topText.text = s;
        topTextStartTime = Time.time;
    }

    public void AddTeamMember(GameObject o)
    {
        team.Add(o);
    }

    public void RemoveTeamMember(GameObject o)
    {
        team.Remove(o);
    }

    public IEnumerator RemoveBuff()
    {
        yield return new WaitForSeconds(2);
        CanNotMove = false;
    }

    public void TakeDamage(int d)
    {
        if(CanNotGetDamage && d >= 0)
        {
            return;
        }

        int updated_health = health - d;

        if(updated_health <= 0)
        {
            OnGameEnd();
            return;
        }

        if(updated_health > maxHealth)
        {
            Debug.Log(maxHealth);
            d = -1 * (maxHealth - health);
        }

        if (d >= 0)
        {
            setTopText("-" + d);
        }
        else
        {
            setTopText("+" + d * -1);
        }
        health -= d;
    }

    public virtual void OnGameEnd()
    {

    }
}
