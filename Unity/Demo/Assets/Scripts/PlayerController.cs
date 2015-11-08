using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerController : MonoBehaviour {

    // player settings
    private Rigidbody rb;

    public int health = 100;

    private Vector3 lastPosition; // last position of player - needed to calculate speed of player
    private float plyayerSpeed = 0; // speed of player - at start 0

    public bool noPlayerStop; // if player is too slow, there is a speeding up
	
    public bool invertControl = false;

    public float speedMultiplier; // multiplier to adjust speed of player

    private int count; // counter - how much pick ups were picked


    // UI elements
    public Text countText;
    public Text winText;



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
                if (invertControl == true)
                {
                    moveVertical = -15;
                }
                else
                {
                    moveVertical = 15;
                }
            }
        }

        
        if (invertControl == true)
		{
            Vector3 movement = new Vector3(moveHorizontal * -1, 0.0f, moveVertical * -1);
            rb.AddForce(movement * speedMultiplier);
        }
        else
        {
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.AddForce(movement * speedMultiplier);
        }        


    }


    void OnTriggerEnter(Collider other)// wird ausgefuehrt, wenn der Spieler mit einem anderen TriggerCollider kollidiert
    {
        // get tag of pick / power up
        string tag = other.gameObject.tag;

        // get current size of player
        Vector3 currentSize = this.transform.localScale;

        switch (tag)
        {
            case "PickUp": // player getes points
                other.gameObject.SetActive(false);
                count = count + 1;
                SetCountText();
                break;
            case "PowerUp-GrosseKugel": // increase size of player
                if ((currentSize.x <= 3f) && (currentSize.y <= 3f) && (currentSize.z <= 3f)) // has player already reached max size
                {
	                this.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
                }
                this.transform.localScale += new Vector3(1, 1, 1);
                break;
            case "PowerUp-KleineKugel": // decrease size of player
                if ((currentSize.x >= 0.5f) && (currentSize.y >= 0.5f) && (currentSize.z >= 0.5f)) // has player already reached smallest size
                {
                    this.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
                }
                break;
            case "PowerUp-InvertControl": // invert control
                if(invertControl)
                {
	                invertControl = false; // control is no longer inverted
                }
                else
                {
	                invertControl = true; // control is is from now on inverted
                }
                break;
            default:
                break;
        }
        // disable pick / power up
        other.gameObject.SetActive(false);
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        /*
        if(count >= 13)
        {
	        winText.text = "You Win!";
        }
        */
    }
}