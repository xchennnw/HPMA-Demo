using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyController : RoleController
{

    private bool pause;

    private Spells SpellManager;
    private PlayerController PlayerScript;
    private GameObject player;
    private GameObject target;
    private GameObject regular_target;
    private int num_of_spells = 8;
    private bool hasTarget;
    public float attackRange = 10;
    public GameObject cube;
    public GameObject bullet;
    private int friendIndex;
    // Start is called before the first frame update
    void Start()
    {
        health = 1400;
        maxHealth = 1400;
        topText.text = "";
        pause = true;
        CanNotMove = false;
        CanNotGetDamage = false;
        GameObject s = GameObject.Find("SpellManager");
        SpellManager = s.GetComponent<Spells>();
        player = GameObject.Find("Player");
        PlayerScript = player.GetComponent<PlayerController>();
        friendIndex = 8;
        InvokeRepeating("ChangePauseState", 2f, 2.5f);
        InvokeRepeating("ChangeTarget", 0f, 3.5f);
        InvokeRepeating("increaseMagicPoint", 0f, 1f);
        InvokeRepeating("Attack", 0f, 4f);
        InvokeRepeating("RegularAttack", 0f, 2f);
        InvokeRepeating("CallFriend", 10f, 50f);
        c = GetComponent<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        bloodText.text = "" + health;
        bloodSlider.value = health;

        if (Time.time - topTextStartTime > 0.5f)
        {
            topText.text = "";
        }

        if (!pause && !CanNotMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        SearchTarget();
        SearchRegularAttckTarget();
        SetColor();
    }

    void CallFriend()
    {
        if (friendIndex > 10) return;
        SpellManager.useSpell(transform.position, friendIndex, 0, -1);
        friendIndex++;
    }

    void ChangePauseState()
    {
        pause = !pause;
    }

    void ChangeTarget()
    {
        float x = Random.Range(1, 49);
        float z = Random.Range(-24, 24);
        targetPos = new Vector3(x, transform.position.y, z);
    }

    void SearchTarget()
    {
        List<GameObject> tkTeam = PlayerScript.team;
        target = player;
        float dist = Distance(player);
        for (int i = 0; i < tkTeam.Count; i++)
        {
            if (Distance(tkTeam[i]) < dist)
            {
                target = tkTeam[i];
                dist = Distance(tkTeam[i]);
            }
        }
    }

    void Attack()
    {
        if (magicPoint < 6) return;
        int x = Random.Range(0, num_of_spells);
        if (SpellManager.getSpell(x).animal)
        {
            SpellManager.useSpell(transform.position, x, 0, -1);
        }
        else if (SpellManager.getSpell(x).toSelf)
        {
            Vector3 v = transform.position;
            SpellManager.useSpell(v, x, 0, -1);
        }
        else
        {
            Vector3 v = target.transform.position;
            SpellManager.useSpell(v, x, 0, -1);
            var o = Instantiate(cube, transform.position, Quaternion.identity);
            o.GetComponent<CubeScript>().target_pos = v;
        }     
        magicPoint -= SpellManager.getMagicValue(x);
    }

    protected void SearchRegularAttckTarget()
    {
        hasTarget = false;
        List<GameObject> tkTeam = PlayerScript.team;
        if (Distance(player) < attackRange)
        {
            regular_target = player;
            hasTarget = true;
            return;
        }
        for (int i = 0; i < tkTeam.Count; i++)
        {
            if (Distance(tkTeam[i]) < attackRange)
            {
                regular_target = tkTeam[i];
                hasTarget = true;
                return;
            }
        }
    }

    protected void RegularAttack()
    {
        if (hasTarget)
        {
            var o = Instantiate(bullet, transform.position, Quaternion.identity);
            o.GetComponent<BulletScript>().target_pos = regular_target.transform.position;
            o.GetComponent<BulletScript>().target = regular_target;
        }
    }
    private float Distance(GameObject obj)
    {
        float dx = obj.transform.position.x - transform.position.x;
        float dy = obj.transform.position.z - transform.position.z;
        float d = Mathf.Sqrt(dx * dx + dy * dy);
        return d;
    }

    public override void OnGameEnd()
    {
        GameObject.Find("EndGameManager").GetComponent<EndGame>().winOrLose = "Win.";
        SceneManager.LoadScene("EndScene");
    }
}


