using UnityEngine;
using System.Collections;

public class pipe_rotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //transform.Rotate(new Vector3(0, 50,0) * (Time.deltaTime/5));
        transform.RotateAround(this.transform.position, this.transform.up, 20 * Time.deltaTime);
    }
}
