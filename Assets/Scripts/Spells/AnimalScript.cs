using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class AnimalScript : MonoBehaviour
{
    public Canvas canvas;
    public Slider healthSlider;
    public int maxHealth = 300;
    public int health = 300;
    public float speed = 10;
    public bool dynamicSpeed = true;
    public bool neverMove = false;
    public bool chaseCharacter = true;
    public int attack = 100;
    public int toKill;

    // Abnormal States
    public bool AlwaysGetDamage;
    public bool CanNotMove;
    public bool CanNotGetDamage;

    protected GameObject tk;
    protected GameObject owner;
    protected RoleController toKillRoleScript;
    protected Vector3 target_pos;
    protected NavMeshAgent agent;
    private Color c;
    private float speedHelper;
    void Start()
    {
        maxHealth = 300;
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
        SetUp();
        SetAbnormalTags();
        if (!neverMove)
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = speed;
        }
        InvokeRepeating("ChangeTarget", 0f, 1f);
        InvokeRepeating("Attack", 0f, 1f);
        if(neverMove)
        {
            speed = 0;
        }
        
        CanNotMove = false;
        CanNotGetDamage = false;
        c = GetComponent<MeshRenderer>().material.color;
        speedHelper = Random.Range(1, 10);
    }

    void Update()
    {       
        healthSlider.value = health;
        if(!neverMove) canvas.transform.LookAt(Camera.main.transform.position);
        SearchTarget();
        UpdateDeath();
        SetColor();
        if(!CanNotMove && !neverMove)
        {
            if (dynamicSpeed) agent.speed = speed * (Mathf.Cos(Time.time + speedHelper) + 1) * 0.5f;
            else agent.speed = speed;
        }
    }

    protected virtual void SearchTarget()
    {

    }

    protected virtual void SetAbnormalTags()
    {
        AlwaysGetDamage = false;
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

    public void SetCanNotMove()
    {
        CanNotMove = true;
        if (!neverMove) agent.speed = 0;
    }

    public void EndCanNotMove()
    {
        CanNotMove = false;
        if (!neverMove) agent.speed = speed;
    }

    public void SetFly(Vector3 pos, float flySpeed)
    {
        if (neverMove) return;
        agent.speed = flySpeed;
        agent.destination = pos;
    }

    protected virtual void ChangeTarget()
    {
        if (!CanNotMove && !neverMove)
        {
            target_pos = tk.transform.position;
            agent.destination = target_pos;
        }      
    }

    public float Distance(GameObject obj)
    {
        float dx = obj.transform.position.x - transform.position.x;
        float dy = obj.transform.position.z - transform.position.z;
        float d = Mathf.Sqrt(dx * dx + dy * dy);
        return d;
    }

    public void SetUp()
    {
        if ((toKill == 1))
        {
            tk = GameObject.Find("Enemy");
            toKillRoleScript = tk.GetComponent<RoleController>();
            owner = GameObject.Find("Player");
        }
        else
        {
            tk = GameObject.Find("Player");
            toKillRoleScript = tk.GetComponent<RoleController>();
            owner = GameObject.Find("Enemy");
        }
        owner.GetComponent<RoleController>().AddTeamMember(gameObject);
    }

    protected virtual void Attack()
    {
        if (chaseCharacter)
        {
            if (Distance(tk) < 1)
            {               
                toKillRoleScript.TakeDamage(attack);
            }
        }     
    }

    public void TakeDamage(int d)
    {
        if (CanNotGetDamage && d >= 0)
        {
            return;
        }
        int updated_health = health - d;
        if (updated_health > maxHealth)
        {
            d = -1 * (maxHealth - health);
        }
        health -= d;
    }

    public virtual void UpdateDeath()
    {
        if (health <= 0)
        {
            owner.GetComponent<RoleController>().RemoveTeamMember(gameObject);
            Destroy(gameObject);
        }
    }
}
