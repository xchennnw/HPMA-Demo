using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{

    public Vector3 target_pos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target_pos, 100 * Time.deltaTime);
        if (distance()<= 1f)
        {
            Destroy(this.gameObject);
        }
    }

    public float distance()
    {
        float dx = target_pos.x - transform.position.x;
        float dy = target_pos.z - transform.position.z;
        float d = Mathf.Sqrt(dx * dx + dy * dy);
        return d;
    }
}
