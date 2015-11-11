
using UnityEngine;
using System.Collections;

public class mouse_over_manu : MonoBehaviour
{

    public float rotation_speed = 300;
    public Quaternion z_value;

    void Start()
    {
        z_value = this.transform.rotation;
    }


    void OnMouseOver()
    {

        transform.RotateAround(this.transform.position, this.transform.forward, rotation_speed * Time.deltaTime);

    }

    void OnMouseExit()
    {
        transform.rotation = z_value;
    }

}

