using UnityEngine;
using System.Collections;

public class ball_hindernis : MonoBehaviour {

    Rigidbody rb;

	// Use this for initialization
	void Start () {
	    rb = GetComponent<Rigidbody>();
    }
	

    void OnTriggerEnter()
    {
        rb.constraints = RigidbodyConstraints.None;
    }
}
