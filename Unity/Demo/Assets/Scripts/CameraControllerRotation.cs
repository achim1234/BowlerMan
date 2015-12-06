using UnityEngine;
using System.Collections;

public class CameraControllerRotation : MonoBehaviour {


    public float turnSpeed = 4.0f;		// Speed of camera turning when mouse moves in along an axis
	public float panSpeed = 4.0f;		// Speed of the camera when being panned
	public float zoomSpeed = 4.0f;		// Speed of the camera going back and forth
	
	private Vector3 mouseOrigin;	// Position of cursor when mouse dragging starts
	private bool isRotating;	// Is the camera being rotated?
	

    public GameObject player;
    private Vector3 offset;
    
 
    // Use this for initialization
    void Start()
    {
        // Abstand Kamera - Spieler
        player = GameObject.FindGameObjectWithTag("player"); //dem spieler das tag player hinzufügen
        offset = transform.position - player.transform.position;
    }





    // Update is called once per frame (LateUpdate -> it is run after "all other things are done" / after we know the player object has moved)
    void LateUpdate()
    {
        
        transform.position = player.transform.position + offset;
    }





	void Update () 
	{
        
        
		// Get the left mouse button
		if(Input.GetMouseButtonDown(0))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isRotating = true;
		}
		
		
		
		// Disable movements on button release
		if (!Input.GetMouseButton(0)) isRotating=false;
		
		
		// Rotate camera along X and Y axis
		if (isRotating)
		{
	        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

			transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
			transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
		}

        
	}

  





   /* public float smooth = 1.5f;         // The relative speed at which the camera will catch up.


    private Transform player;           // Reference to the player's transform.
    private Vector3 relCameraPos;       // The relative position of the camera from the player.
    private float relCameraPosMag;      // The distance of the camera from the player.
    private Vector3 newPos;             // The position the camera is trying to reach.


    void Awake()
    {
        // Setting up the reference.
        player = GameObject.FindGameObjectWithTag("player").transform;

        // Setting the relative position as the initial relative position of the camera in the scene.
        relCameraPos = transform.position - player.position;
        relCameraPosMag = relCameraPos.magnitude - 0.5f;
    }


    void FixedUpdate()
    {
        // The standard position of the camera is the relative position of the camera from the player.
        Vector3 standardPos = player.position + relCameraPos;

        // The abovePos is directly above the player at the same distance as the standard position.
        Vector3 abovePos = player.position + Vector3.up * relCameraPosMag;

        // An array of 5 points to check if the camera can see the player.
        Vector3[] checkPoints = new Vector3[5];

        // The first is the standard position of the camera.
        checkPoints[0] = standardPos;

        // The next three are 25%, 50% and 75% of the distance between the standard position and abovePos.
        checkPoints[1] = Vector3.Lerp(standardPos, abovePos, 0.25f);
        checkPoints[2] = Vector3.Lerp(standardPos, abovePos, 0.5f);
        checkPoints[3] = Vector3.Lerp(standardPos, abovePos, 0.75f);

        // The last is the abovePos.
        checkPoints[4] = abovePos;

        // Run through the check points...
        for (int i = 0; i < checkPoints.Length; i++)
        {
            // ... if the camera can see the player...
            if (ViewingPosCheck(checkPoints[i]))
                // ... break from the loop.
                break;
        }

        // Lerp the camera's position between it's current position and it's new position.
        transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime);

        // Make sure the camera is looking at the player.
        SmoothLookAt();
    }


    bool ViewingPosCheck(Vector3 checkPos)
    {
        RaycastHit hit;

        // If a raycast from the check position to the player hits something...
        if (Physics.Raycast(checkPos, player.position - checkPos, out hit, relCameraPosMag))
            // ... if it is not the player...
            if (hit.transform != player)
                // This position isn't appropriate.
                return false;

        // If we haven't hit anything or we've hit the player, this is an appropriate position.
        newPos = checkPos;
        return true;
    }


    void SmoothLookAt()
    {
        // Create a vector from the camera towards the player.
        Vector3 relPlayerPosition = player.position - transform.position;

        // Create a rotation based on the relative position of the player being the forward vector.
        Quaternion lookAtRotation = Quaternion.LookRotation(relPlayerPosition, Vector3.up);

        // Lerp the camera's rotation between it's current rotation and the rotation that looks at the player.
        transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, smooth * Time.deltaTime);
    }*/
}
