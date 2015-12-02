using UnityEngine;
using System.Collections;

public class bowlerman_hat_ingame : MonoBehaviour {

    float bowlerman_x;
    float bowlerman_y;
    float bowlerman_z;
    public GameObject objekt;
    
    // Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void LateUpdate () {
        bowlerman_x = objekt.transform.position.x;

        bowlerman_z = objekt.transform.position.z;
        bowlerman_z = bowlerman_z - 0.35f;

        bowlerman_y = objekt.transform.position.y;
        bowlerman_y = bowlerman_y + 1.07f;


        this.transform.position = new Vector3(bowlerman_x,bowlerman_y , bowlerman_z); 

	}
}
