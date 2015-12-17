using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerController : MonoBehaviour {

    // player settings
    private Rigidbody rb;
    Vector3 player_velocity;
    Vector3 player_angular_velocity;
    Quaternion player_rotation;

    GameManager GM;

    private Vector3 lastPosition; // last position of player - needed to calculate speed of player
    private float plyayerSpeed = 0; // speed of player - at start 0

    public bool noPlayerStop; // if player is too slow, there is a speeding up
	
    public bool invertControl = false;

    // power up player jump
    public bool playerJump = false;
    public bool isFalling = false;
    public float jumpForce = 10.0f;

    public float speedMultiplier = 1; // multiplier to adjust speed of player

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
    public Text countdownUIText;
    public Text winTotalScoreUIText;

    public Button button_spiel_beenden;
    public Button button_spiel_fortsetzen;

    public Image myPanel;
    float fadeTime = 5f;
    Color colorToFadeTo;

    void Start ()
    {
        // game manager stuff - set state + scene name
        GM = GameManager.Instance;
        GM.SetGameState(GameState.Countdown);
        GM.SetCurrentSceneName(Application.loadedLevelName);

        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // player is not able to move during countdown

        // reset score + UI elements
        count = 0;
        SetCountText();
        setUIHealth();
        winUIText.text = "";
        setUpButtons();

        colorToFadeTo = new Color(1f, 1f, 1f, 0f);
        myPanel.CrossFadeColor(colorToFadeTo, 0.0f, true, true);

        // set time for specific level
        if (GM.currentscene == "daniels_level")
        {
            timerMax = 50.0f;
            timer = timerMax;
        }
        else if (GM.currentscene == "achims_level_2")
        {
	        timerMax = 200.0f;
	        timer = timerMax;
        }
        else
        {
            timer = timerMax;
        }
       

        StartCoroutine(startCountDown()); // start countdown
    }


    // Update is called once per frame
    // is run before rendering a frame
    void Update ()
    {
        if (!isGameOver)
        {
            if (timer < 0) // no more time left -> game over
            {
                Debug.Log("timer Zero reached !");
                timer = 0;				
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
        setUITimer();
    }




    // is ran before performing any physics calculation
    void FixedUpdate()
    {
        if (!isGameOver)
        {
            // Pause bei druecken von 'Escape' (ausser waehrend Countdown)
            if (Input.GetKeyUp(KeyCode.Escape) && GM.gameState != GameState.Countdown)
            {
                // Spiel ist bereits pausiert
                if (GM.gameState == GameState.GamePaused)
                {
                    resumeGame();
                }
                else // pausiere Spiel
                {
                    pauseGame();
                }

            }

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
        else // game is over
        {
            // slightly slow down player
            rb.velocity = rb.velocity * 0.985f;
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
                    count += (int)count * ((int)timer / 2); // bonus points for left time
                    count = count + 100; // add 100 extra points
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
                    SoundManager.instance.PlaySingle("power_up");
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
                    SoundManager.instance.PlaySingle("power_up");
                    speedMultiplier += 1.3f;
                    // disable pick / power up
                    other.gameObject.SetActive(false);
                    break;
                case "PowerUp-AddMass": // increase mass of player
                    SoundManager.instance.PlaySingle("power_up");
                    rb.mass += 0.145f;
                    // disable pick / power up
                    other.gameObject.SetActive(false);
                    break;
                case "PowerUp-PlayerJump": //player jump
                    SoundManager.instance.PlaySingle("power_up");
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


    IEnumerator startCountDown()
    {
        countdownUIText.text = "3";
        yield return new WaitForSeconds(1);
        countdownUIText.text = "2";
        yield return new WaitForSeconds(1);
        countdownUIText.text = "1";
        yield return new WaitForSeconds(1);
        countdownUIText.text = "Go";
        GM.SetGameState(GameState.Game); // set game state to 'Game'
        rb.isKinematic = false; // player is able to move
        yield return new WaitForSeconds(1.5f); // 'Go' is displayed for 1.5 seconds
        countdownUIText.text = ""; // remove 'Go'
    }

    void SetCountText()
    {
        countUIText.text = "Score: " + count.ToString();
    }



    void finishedLevel() // level won
    {
        SoundManager.instance.PlaySingle("game_won"); // Gewonnen-Sound abspielen
        isGameOver = true;
        GM.SetGameState(GameState.FinishedLevel);

        colorToFadeTo = new Color(0f, 0f, 0f, 1f);
        myPanel.CrossFadeColor(colorToFadeTo, 1.2f, true, true);

        setUILevelWinText();
        StartCoroutine(updateTotalScoreAndUI());
        StartCoroutine(loadNextLevel());
    }

    void setGameOver() // level / game lost
    {
        SoundManager.instance.PlaySingle("game_lose"); // Game-Over Sound abspielen
        isGameOver = true;
        GM.SetGameState(GameState.GameOver);

        colorToFadeTo = new Color(0f, 0f, 0f, 1f);
        myPanel.CrossFadeColor(colorToFadeTo, 1.2f, true, true);

        setUIGameOver();
        StartCoroutine(updateTotalScoreAndUI());
        StartCoroutine(waitForHighscoreScene());
    }


    IEnumerator updateTotalScoreAndUI()
    {
        yield return new WaitForSeconds(1.5f); // wait for x seconds
        winTotalScoreUIText.text = "Total Score: " + GM.totalscore.ToString();
        yield return new WaitForSeconds(1.5f); // wait for x seconds

        int score_ui = GM.totalscore; // get total score
        int level_score = count; // count / score of current level

        if (level_score > 1110)
        {
            for (int i = 0; i <= level_score; i += 1000)
            {
                winTotalScoreUIText.text = "Total Score: " + (score_ui + i).ToString();
                score_ui += i;
                if (GM.gameState == GameState.GameOver)
                {
                    winUIText.text = "Game over! \n\n Score: " + (level_score - i).ToString();
                }
                else
                {
                    winUIText.text = "Well done! \n\n Score: " + (level_score - i).ToString();

                }
                countUIText.text = "Score: " + (level_score - i).ToString();
                yield return new WaitForSeconds(0.0425f); // wait for x seconds
                // http://answers.unity3d.com/questions/43752/is-waitforseconds-framerate-dependent.html
                level_score -= i;
            }
        }


        if (level_score > 210)
        {
            for (int i = 0; i <= level_score; i+=100)
            {
                winTotalScoreUIText.text = "Total Score: " + (score_ui + i).ToString();
                score_ui += i;
                if (GM.gameState == GameState.GameOver)
                {
                    winUIText.text = "Game over! \n\n Score: " + (level_score - i).ToString();
                }
                else
                {
                    winUIText.text = "Well done! \n\n Score: " + (level_score - i).ToString();

                }
                countUIText.text = "Score: " + (level_score - i).ToString();
                yield return new WaitForSeconds(0.0425f); // wait for x seconds
                // http://answers.unity3d.com/questions/43752/is-waitforseconds-framerate-dependent.html
                level_score -= i;
            }
        }

        
        for (int i = 0;i <= level_score; i++)
        {
            winTotalScoreUIText.text = "Total Score: " + (score_ui + i).ToString();
            if(GM.gameState == GameState.GameOver)
            {
                winUIText.text = "Game over! \n\n Score: " + (level_score - i).ToString();
            }
            else
            {
                winUIText.text = "Well done! \n\n Score: " + (level_score - i).ToString();

            }
            countUIText.text = "Score: " + (level_score - i).ToString();
            yield return new WaitForSeconds(0.015f); // wait for x seconds
            // http://answers.unity3d.com/questions/43752/is-waitforseconds-framerate-dependent.html
        }

        // GM.SetTotalScore(count); // update total score in game manager

        yield return new WaitForSeconds(2); // wait for x seconds
    }



    IEnumerator loadNextLevel()
    {
        yield return new WaitForSeconds(11); // wait for x seconds

        GM.SetTotalScore(count); // update total score in game manager

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
                Application.LoadLevel("test_highscore");
                // Application.LoadLevel("highscore"); // highscore not implemented yet
                break;
            default:
                Application.LoadLevel("menu");
                break;
        }
    }



    IEnumerator waitForHighscoreScene() // wait for x seconds and then load highscore scene
    {
        yield return new WaitForSeconds(11);
        GM.SetTotalScore(count); // update total score in game manager
        Application.LoadLevel("test_highscore");
    }





    void setUITimer() // UI time
    {
        if (GM.gameState == GameState.Game)
        {
            // Zeit wird erst gestartet sobald das Spiel begonnen wurde
            timer -= Time.deltaTime;
            if (timer < 10)
            {
                timerUIText.color = Color.red;
            }
        }
        timerUIText.text = "Time: " + timer.ToString("0.00");
    }

    void setUIHealth() // UI health
    {
        int health = (int)((healthPoints / 50) * 100);
        if(health <= 10)
        {
            healthUIText.color = Color.red;
        }
        healthUIText.text = "Health: " + health.ToString("0");
    }

    void setUIGameOver() // UI - shows text if game is over
    {
        string text = "Game over! \n\n Score: " + count.ToString();
        winUIText.text = text;
    }


    void setUILevelWinText() // UI - shows text if game is over
    {
        string text = "Well done! \n\n Score: " + count.ToString();
        winUIText.text = text;
    }


    void setUpButtons()
    {
        // disable buttons (at start)
        disableButtonSpielFortsetzen();
        disableButtonSpielBeenden();

        // add listener
        button_spiel_beenden.GetComponent<Button>().onClick.AddListener(() => { playerClickedQuitsGame(); });
        button_spiel_fortsetzen.GetComponent<Button>().onClick.AddListener(() => { playerClickedResumesGame(); });
    }

    void playerClickedQuitsGame()
    {
        GM.SetGameState(GameState.GameOver);
        Application.LoadLevel("menu");
    }

    void playerClickedResumesGame()
    {
        resumeGame();
    }

    void resumeGame()
    {
        GM.SetGameState(GameState.Game); // set game state to 'Game'
        Debug.Log("spiel geht weiter");

        rb.rotation = player_rotation; // set player rotation of values before pause
        rb.angularVelocity = player_angular_velocity; // set player angular velocity of values before pause
        rb.velocity = player_velocity; // set player velocity of values before pause
        rb.isKinematic = false; // player is able to move

        colorToFadeTo = new Color(1f, 1f, 1f, 0f);
        myPanel.CrossFadeColor(colorToFadeTo, 0.30f, true, true);
        disableButtonSpielFortsetzen();
        disableButtonSpielBeenden();

    }

    void pauseGame()
    {
        GM.SetGameState(GameState.GamePaused); // set game state to 'Game Paused'
        Debug.Log("show menu");
        
        player_rotation = rb.rotation; // save current rotation of palyer
        player_angular_velocity = rb.angularVelocity; // save angular velocity rotation of palyer
        player_velocity = rb.velocity; // save current velocity of palyer
        rb.isKinematic = true; // player is not able to move

        colorToFadeTo = new Color(0f, 0f, 0f, 1f);
        myPanel.CrossFadeColor(colorToFadeTo, 1.2f, true, true);
        enableButtonSpielFortsetzen();
        enableButtonSpielBeenden();
    }


    void enableButtonSpielFortsetzen()
    {
        button_spiel_fortsetzen.interactable = true;
        button_spiel_fortsetzen.gameObject.SetActive(true);
    }

    void disableButtonSpielFortsetzen()
    {
        button_spiel_fortsetzen.interactable = false;
        button_spiel_fortsetzen.gameObject.SetActive(false);
    }

    void enableButtonSpielBeenden()
    {
        button_spiel_beenden.interactable = true;
        button_spiel_beenden.gameObject.SetActive(true);
    }

    void disableButtonSpielBeenden()
    {
        button_spiel_beenden.interactable = false;
        button_spiel_beenden.gameObject.SetActive(false);
    }


}