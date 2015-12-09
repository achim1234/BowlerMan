using UnityEngine;
using System.Collections;

public class bowlerman_hat_ingame : MonoBehaviour {

    float bowlerman_x;
    float bowlerman_y;
    float bowlerman_z;
    public GameObject objekt;

    /*
    Quaternion currentRotation;
    public float winkel_max;
    public float positon_max;
    

    float angeldifferent = 0f;
    float positiondifferent;
    */



    // Use this for initialization
	void Start () {
        /*
        currentRotation = this.transform.rotation;
	    */
   }
	
	// Update is called once per frame
	void LateUpdate () {
        bowlerman_x = objekt.transform.position.x;

        bowlerman_z = objekt.transform.position.z;
        bowlerman_z = bowlerman_z - 0.35f;

        bowlerman_y = objekt.transform.position.y;
        bowlerman_y = bowlerman_y + 1.07f;

        
        this.transform.position = new Vector3(bowlerman_x,bowlerman_y , bowlerman_z);
        /*
        // this.transform.Rotate(new Vector3(0f, angeldifferent, 0));
        //transform.position = new Vector3(Mathf.Clamp(Time.time, 1.0F, 3.0F), 0, 0);

        float rotationX = Mathf.Clamp(angeldifferent, currentRotation.eulerAngles.x, currentRotation.eulerAngles.x + winkel_max);
       transform.rotation = Quaternion.Euler(new Vector3(rotationX, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z));
      //  Debug.Log("AAAA");
      */
    }

    /*
    void Update() {

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {

            angeldifferent = getKeyDownMeasurement();
            angeldifferent += currentRotation.eulerAngles.x;
            
        }
        

    }

    float getKeyDownMeasurement()
    {
   
        float downTime = Time.time;
        float pressTime = 0f;
        pressTime += downTime;
        return pressTime;
    }
    */
}
