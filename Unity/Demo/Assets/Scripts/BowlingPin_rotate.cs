using UnityEngine;
using System.Collections;

public class BowlingPin_rotate : MonoBehaviour {

    public float x;
    public float y;
    public float z;


	// Use this for initialization
	void Start () {
	
	}

    void Update()
    {
        transform.Rotate(new Vector3(x, y, z) * Time.deltaTime);
    }
}
