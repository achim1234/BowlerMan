using UnityEngine;
using System.Collections;
public class bowlerman_rotation_menu : MonoBehaviour
{
    public float smooth = 2.0F;
    public float tiltAngle_y;
    public float tiltAngle_x;
    public float tiltAngle_minus_y;
    public float tiltAngle_minus_x;
    public float tiltAngle_middle_y;
    public float tiltAngle_middle_x;

    public float bow_down_x;
    public float bow_down_y;
    public float bow_down_z;

    public float bow_up_x;
    public float bow_up_y;
    public float bow_up_z;

    bool targetreached_right = true;
    bool targetreached_left = false;
    bool targetreached_middle = true;

    bool bow_down_bool = false;
    bool bow_up_bool = true;


    void Update()
    {
        if((bow_down_bool == false) && (bow_up_bool == true))
        {
            bow_down();
        }
        if ((bow_down_bool == true) && (bow_up_bool == false))
        {
            bow_up();
        }

        if ((targetreached_right == true) && (targetreached_left == false) && (targetreached_middle == true)  && (bow_down_bool == true) && (bow_up_bool == true))
        {
            Target_reached_left();
        }
        if ((targetreached_right == false) && (targetreached_left == true ) && (targetreached_middle == false) && (bow_down_bool == true) && (bow_up_bool == true))
        {
            Target_reached_right();
        }
        if((targetreached_right == true) && (targetreached_left == false) && (targetreached_middle == false) && (bow_down_bool == true) && (bow_up_bool == true))
        {
            Target_reached_middle();
        }
    }


    void bow_down()
    {
      
        Quaternion target = Quaternion.Euler(bow_down_x, bow_down_y, bow_down_z);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

        if (Mathf.Approximately(transform.rotation.eulerAngles.z, bow_down_z))
        {
            bow_down_bool = true;
            bow_up_bool = false;
        }

    }

    void bow_up()
    {
        Quaternion target = Quaternion.Euler(bow_up_x, bow_up_y, bow_up_z);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

        if (Mathf.Approximately(transform.rotation.eulerAngles.z, bow_up_z))
        {
            bow_down_bool = true;
            bow_up_bool = true;
        }
    }



    void Target_reached_left()
    {
        //tiltAngle_y = Input.GetAxis("Horizontal") * tiltAngle_y;
        //tiltAngle_x = Input.GetAxis("Vertical") * tiltAngle_x;
        Quaternion target = Quaternion.Euler(tiltAngle_x, tiltAngle_y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

        
        if (Mathf.Approximately(transform.rotation.eulerAngles.y, tiltAngle_y))
        {
            targetreached_right = false;
            targetreached_left = true;
            targetreached_middle = false;
        }
    }


    void Target_reached_right()
    {
        //float tiltAroundy = Input.GetAxis("Horizontal") * tiltAngle_y;
        //float tiltAroundX = Input.GetAxis("Horizontal") * tiltAngle_x;
        Quaternion target = Quaternion.Euler(tiltAngle_minus_x, tiltAngle_minus_y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

        if (Mathf.Approximately(transform.rotation.eulerAngles.y, tiltAngle_minus_y))
        {
            targetreached_right = true;
            targetreached_left = false;
            targetreached_middle = false;
        }
    }


    void Target_reached_middle()
    {
        //float tiltAroundy = Input.GetAxis("Horizontal") * tiltAngle_y;
        //float tiltAroundX = Input.GetAxis("Horizontal") * tiltAngle_x;
        Quaternion target = Quaternion.Euler(tiltAngle_middle_x, tiltAngle_middle_y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

        if (Mathf.Approximately(transform.rotation.eulerAngles.y, tiltAngle_middle_y))
        {
            targetreached_right = true;
            targetreached_left = false;
            targetreached_middle = true;
        }
    }


}

    







/*
using UnityEngine;
using System.Collections;

public class bowlerman_rotation_menu : MonoBehaviour {

    public float rotation_speed = 10;
    public float rotation_speed_minus = -75;
    public float target_left_reached;
    public float target_right_reached;
    public Quaternion y_value;


    bool targetreached_right = true;
    bool targetreached_left = false;



    // Use this for initialization
    void Start () {
        //target_minus_right = this.transform.position + new Vector3(0, target_right, 0);
        //target_plus_left = this.transform.position + new Vector3(0, target_z, 0);
        y_value = this.transform.rotation;
    }

    
    void Update(){

        if (targetreached_right)
        {
            Target_reached_left();
        }
        if (targetreached_left)
        {
            Target_reached_right();
        }

    }

    void Target_reached_left()
    {
        //target_left_reached += Input.GetAxis("Horizontal");
        //transform.eulerAngles = new Vector3(10, target_left_reached, 0);
        y_value = this.transform.rotation;
        transform.RotateAround(this.transform.position, this.transform.up, target_left_reached * Time.deltaTime);
        if (y_value.y >= target_left_reached)
        {
            targetreached_right = false;
            targetreached_left = true;
        }
    }


    void Target_reached_right()
    {
        //target_right_reached += Input.GetAxis("Horizontal");
        // transform.eulerAngles = new Vector3(-10, target_right_reached, 0);
        //transform.RotateAround(this.transform.position, this.transform.up, rotation_speed * Time.deltaTime);
        y_value = this.transform.rotation;
        transform.RotateAround(this.transform.position, this.transform.up, target_right_reached * Time.deltaTime);
        if (transform.eulerAngles.y == target_right_reached)
        {
            targetreached_right = true;
            targetreached_left = false;
        }
    }

}
*/
