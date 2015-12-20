using UnityEngine;
using System.Collections;

public class move_obelisk : MonoBehaviour {

    public float speed = 1.0f;
    public float target_x;
    public float target_minus_x;
    Vector3 target = Vector3.zero;
    Vector3 target_minus = Vector3.zero;
    float step;

    bool targetreached_right = true;
    bool targetreached_left = false;




    void Start()
    {
        target = this.transform.position + new Vector3(target_x, 0, 0);
        target_minus = this.transform.position + new Vector3(target_minus_x, 0, 0);
    }

    void Update()
    {
        step = speed * Time.deltaTime;

        if (targetreached_right)
        {
            Targetreached_left();
        }
        if (targetreached_left)
        {
            Targetreached_right();
        }
    }

    void Targetreached_right()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        if (Mathf.Approximately(transform.position.x, target.x))
        {
            targetreached_right = true;
            targetreached_left = false;
        }
    }

    void Targetreached_left()
    {
        transform.position = Vector3.MoveTowards(transform.position, target_minus, step);
        if (Mathf.Approximately(transform.position.x, target_minus.x))
        {
            targetreached_left = true;
            targetreached_right = false;

        }
    }
}
