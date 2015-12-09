using UnityEngine;
using System.Collections;

public class load_scene_highscore : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseUp()
    {
        Application.LoadLevel("test_highscore");
    }
}
