using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerController : MonoBehaviour {

    public float speed;
    public bool noPlayerStop; // if player is too slow, there is a speeding up

    // UI elements
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;

    private Vector3 lastPosition;
    private float plyayerSpeed = 0;


    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }

	// Update is called once per frame
    // is run before rendering a frame
	void Update ()
    {
	
	}

    // is ran before performing any physics calculation
    void FixedUpdate()
    {
        // get player input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // calculate current speed of player
        plyayerSpeed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        lastPosition = transform.position;

        // is player allowed to stop / be really slowly
        if (noPlayerStop)
        {
            // if player is to slow, speed up
            if (plyayerSpeed < 7)
            {
                moveVertical = 15;
            }
        }


        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }


    void OnTriggerEnter(Collider other)// wird ausgefuehrt, wenn der Spieler mit einem anderen TriggerCollider kollidiert
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }

    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 13)
        {
            winText.text = "You Win!";
        }
    }
}