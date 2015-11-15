
using UnityEngine;
using System.Collections;
using System;

public class bowlerman_rotate : MonoBehaviour
{

    public float speed = 1.0f;
    public float target_z;
    public float target_minus_z;
    Vector3 target = Vector3.zero;
    Vector3 target_minus = Vector3.zero;
    public float step;
    public float magnitutde;

    bool targetreached_right = true;
    bool targetreached_left = false;




    void Start()
    {
        target_minus = this.transform.position + new Vector3(0, target_minus_z, 0);
        target = this.transform.position + new Vector3(0, target_z, 0);
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
        transform.position = Vector3.RotateTowards(transform.position, target_minus, step, magnitutde);
        if (Mathf.Approximately(transform.rotation.y, target.y))
        {
            targetreached_right = true;
            targetreached_left = false;
        }
    }

    void Targetreached_left()
    {
        transform.position = Vector3.MoveTowards(transform.position, target_minus, step);
        if (Mathf.Approximately(transform.rotation.y, target_minus.y))
        {
            targetreached_left = true;
            targetreached_right = false;
        }
    }



}



























/*
using UnityEngine;
using System.Collections;

public class bowlerman_rotate : MonoBehaviour {


    //public float rotation_speed = 500000.0f;
    public float speed = 5.0f;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update () {

        transform.Rotate(Vector3.up * Time.deltaTime * speed, Space.Self);

        //transform.rotation = Quaternion.AngleAxis(47, Vector3.up * Time.deltaTime* slowdown);


        // transform.rotation = Quaternion.FromToRotation(Vector3.up, transform.(new Vector3(0, 47, 0) * Time.deltaTime));
        //transform.RotateAround(this.transform.position, this.transform.up, rotation_speed * Time.deltaTime);
    }
}
*/
