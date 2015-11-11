using UnityEngine;
using System.Collections;

public class mouse_over_manu : MonoBehaviour {
    
    public float z_value;

    void Start()
    {
        z_value = this.transform.rotation.z;
    }

    
    void OnMouseOver() { 
       
            transform.RotateAround(this.transform.position, this.transform.forward, 100 * Time.deltaTime);
          
    }

    void OnMouseExit()
    {
        transform.Rotate(0, 0, z_value);
    }
    
    /*
	// Update is called once per frame
	void Update () {
        while (z_value < z_value + 360)
        {
            transform.RotateAround(this.transform.position, this.transform.forward, 100 * Time.deltaTime);
        }
   }
    */

  
        public float rotationleft = 360;
        public float rotationspeed = 10;
        public float rotation;


    /*
    void Update() { 
    

        float rotation = rotationspeed * Time.deltaTime;
        if (rotationleft > rotation)
        {
            rotationleft -= rotation;
        }
        else
        {
            rotation = rotationleft;
            rotationleft = 0;
        }
        transform.Rotate(0, 0, rotation);
    

    }
    */
}
