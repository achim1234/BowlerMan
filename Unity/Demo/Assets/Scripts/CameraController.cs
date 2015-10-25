using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
        // Abstand Kamera - Spieler
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame (LateUpdate -> it is run after "all other things are done" / after we know the player object has moved)
	void LateUpdate () {
        // 
        transform.position = player.transform.position + offset;
	}
}
