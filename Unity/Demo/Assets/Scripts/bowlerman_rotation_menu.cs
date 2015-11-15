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
