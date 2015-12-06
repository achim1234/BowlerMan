using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerController : MonoBehaviour {

    // player settings
    private Rigidbody rb;

    GameManager GM;

    private Vector3 lastPosition; // last position of player - needed to calculate speed of player
    private float plyayerSpeed = 0; // speed of player - at start 0

    public bool noPlayerStop; // if player is too slow, there is a speeding up
	
    public bool invertControl = false;

    // power up player jump
    public bool playerJump = false;
    public bool isFalling = false;
    public float jumpForce = 10.0f;

    public float speedMultiplier; // multiplier to adjust speed of player

    private int count; // counter - how much pick ups were picked

    // Lebenspunkte kugel
    public float healthPoints = 10000.0f;


    // timer
    float timer = 0.0f;
    float timerMax = 45.0f;

    bool isGameOver = false;

    // UI elements
    public Text countUIText;
    public Text winUIText;
    public Text timerUIText;
    public Text healthUIText;



    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        setUIHealth();

        timer = timerMax;

        winUIText.text = "";

        GM = GameManager.Instance;
        GM.SetGameState(GameState.Game);
        GM.SetCurrentSceneName(Application.loadedLevelName);
    }

    // Update is called once per frame
    // is run before rendering a frame
    void Update ()
    {
        if (!isGameOver)
        {
            timer -= Time.deltaTime;

            setUITimer();

            if (timer < 0) // no more time left -> game over
            {
                Debug.Log("timer Zero reached !");

                timer = 0;
                setUITimer();
				
				setGameOver();
            }
        }
        else // game is over
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
				Debug.Log("load level menu");
				Application.LoadLevel("menu");                
            }
        }
    }

    // is ran before performing any physics calculation
    void FixedUpdate()
    {
        if (!isGameOver)
        {
            //Debug.Log(healthPoints);
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

            // jump
            if (playerJump == true)
            {
                if (Input.GetKey(KeyCode.Space) && isFalling == false)
                {
                    isFalling = true;
                    rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
                }
            }
        }      
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isGameOver)
        {
            // Debug.Log("collided with: " + collision.gameObject.name);
            isFalling = false;

            //collision.gameObject.GetComponent<Cube>();

            if (healthPoints > 0 && collision.gameObject.tag == "CubeObstacle")
            {
                healthPoints = healthPoints - plyayerSpeed;
                setUIHealth();
               // Debug.Log(healthPoints);

            }
            else if (healthPoints < 0.0f) // no more health -> game over
            {
                Debug.Log("Game Over! Keine Lebenspunkte mehr!");
                healthPoints = 0;
                setUIHealth();
                setGameOver();
            }

            // get tag of pick / power up
            string name = collision.gameObject.name;
            switch (name)
            {
                case "BowlingPin-EndLevel":
                    count = count + 50; // points for finisihing level
                    count = (int)count * (int)timer; // bonus points for left time
                    SetCountText();
                    finishedLevel();
                    break;
                default:
                    break;


            }
        }

    }



    void OnTriggerEnter(Collider other)// wird ausgefuehrt, wenn der Spieler mit einem anderen TriggerCollider kollidiert
    {
        if (!isGameOver)
        {
            // get tag of pick / power up
            string tag = other.gameObject.tag;

            // get current size of player
            Vector3 currentSize = this.transform.localScale;

            switch (tag)
            {
                case "BowlingPin": // player getes points
                    count = count + 10;
                    SetCountText();
                    SoundManager.instance.PlaySingle("pin_hit");
                    break;
                case "BowlingPin-EndLevel": // player finished level
                    count = count + 10;
                    SetCountText();
                    finishedLevel();
                    break;
                case "PickUp": // player getes points
                    other.gameObject.SetActive(false);
                    count = count + 10;
                    SetCountText();
                    SoundManager.instance.PlaySingle("power_up");
                    break;
                case "PowerUp-GrosseKugel": // increase size of player
                    if ((currentSize.x <= 3f) && (currentSize.y <= 3f) && (currentSize.z <= 3f)) // has player already reached max size
                    {
                        this.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
                    }
                    this.transform.localScale += new Vector3(1, 1, 1);
                    // disable pick / power up
                    other.gameObject.SetActive(false);
                    SoundManager.instance.PlaySingle("power_up");
                    break;
                case "PowerUp-KleineKugel": // decrease size of player
                    if ((currentSize.x >= 0.5f) && (currentSize.y >= 0.5f) && (currentSize.z >= 0.5f)) // has player already reached smallest size
                    {
                        this.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
                    }
                    // disable pick / power up
                    other.gameObject.SetActive(false);
                    SoundManager.instance.PlaySingle("power_up");
                    break;
                case "PowerUp-InvertControl": // invert control
                    if (invertControl)
                    {
                        invertControl = false; // control is no longer inverted
                    }
                    else
                    {
                        invertControl = true; // control is is from now on inverted
                    }
                    // disable pick / power up
                    other.gameObject.SetActive(false);
                    break;
                case "PowerUp-SpeedUp": // speed up player
                    speedMultiplier += 10;
                    // disable pick / power up
                    other.gameObject.SetActive(false);
                    break;
                case "PowerUp-AddMass": // increase mass of player
                    rb.mass += 0.145f;
                    // disable pick / power up
                    other.gameObject.SetActive(false);
                    break;
                case "PowerUp-PlayerJump": //player jump
                    playerJump = true;
                    // disable pick / power up
                    other.gameObject.SetActive(false);
                    break;
                case "Water": // player hits water
                    setGameOver();
                    break;
                default:
                    break;
            }
        }
    }

    void SetCountText()
    {
        countUIText.text = "Score: " + count.ToString();
    }



    void finishedLevel() // level won
    {
        SoundManager.instance.PlaySingle("game_won"); // Gewonnen-Sound abspielen
        isGameOver = true;
        GM.SetTotalScore(count);
        GM.SetGameState(GameState.FinishedLevel);
        setUILevelWinText();
        StartCoroutine(loadNextLevel());        
    }



    IEnumerator loadNextLevel()
    {
        yield return new WaitForSeconds(6); // wait for x seconds

        string currentscene = GM.currentscene; // get name of current scene

        switch (currentscene)
        {
            case "achims_level":
                Application.LoadLevel("daniels_level");
                break;
            case "daniels_level":
                Application.LoadLevel("werners_level");
                break;
            case "werners_level":
                Application.LoadLevel("menu");
                // Application.LoadLevel("highscore"); // highscore not implemented yet
                break;
            default:
                Application.LoadLevel("menu");
                break;
        }
    }



    IEnumerator waitForHighscoreScene() // wait for x seconds
    {
        SoundManager.instance.musicSource.Play();
        yield return new WaitForSeconds(6);
        Application.LoadLevel("menu");
        // Application.LoadLevel("highscore"); // highscore not implemented yet
    }


    void setGameOver() // level / game lost
	{
        SoundManager.instance.PlaySingle("game_lose"); // Game-Over Sound abspielen
        isGameOver = true;
        GM.SetTotalScore(count);
        GM.SetGameState(GameState.GameOver);
		setUIGameOver();
        StartCoroutine(waitForHighscoreScene());
       
    }


    void setUITimer() // UI time
    {
        timerUIText.text = "Time: " + timer.ToString("0.00");
    }

    void setUIHealth() // UI health
    {
        int health = (int)((healthPoints / 50) * 100);
        healthUIText.text = "Health: " + health.ToString("0");
    }

    void setUIGameOver() // UI - shows text if game is over
    {
        string text = "Game over! \n\n Score: " + count.ToString();
        winUIText.text = text;
    }


    void setUILevelWinText() // UI - shows text if game is over
    {
        string text = "Good job! \n\n Score: " + count.ToString("0.00");
        winUIText.text = text;
    }
}