using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuController : MonoBehaviour
{

    string username = "";
    string score = "";
    bool added_highscore = false;
    bool button_reset_highscore = false;

    List<Scores> highscore;

    GameManager GM;

    // Use this for initialization
    void Start()
    {
        //EventManager._instance._buttonClick += ButtonClicked;

        highscore = new List<Scores>();

        GM = GameManager.Instance;
        GM.SetGameState(GameState.HighScore);
        GM.SetCurrentSceneName(Application.loadedLevelName);

    }


    void ButtonClicked(GameObject _obj)
    {
        print("Clicked button:" + _obj.name);
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnGUI()
    {
        if(GM.totalscore > 0)
        {
            if (added_highscore == false)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Name :", GUILayout.Width(Screen.width / 2));
                username = GUILayout.TextField(username, 25);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Score :", GUILayout.Width(Screen.width / 2));
                GUILayout.Label("" + GM.totalscore);
                score = GM.totalscore.ToString();
                //score = GUILayout.TextField(score);
                GUILayout.EndHorizontal();

                if (GUILayout.Button("Add Score"))
                {
                    if (!username.Equals(""))
                    {
                        added_highscore = true;
                        HighScoreManager._instance.SaveHighScore(username, System.Int32.Parse(score));
                        highscore = HighScoreManager._instance.GetHighScore();
                    }
                }
            }
            else
            {
                showHighScore();
            }


            /*
            if (GUILayout.Button("Get LeaderBoard"))
            {
                highscore = HighScoreManager._instance.GetHighScore();
            }
            */
        }
        else
        {
            showHighScore();
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                // CTRL + R
                if (GUILayout.Button("Clear Leaderboard"))
                {
                    HighScoreManager._instance.ClearLeaderBoard();
                }
            }
        }
    }

    public void showHighScore()
    {
        GUILayout.Space(60);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Name", GUILayout.Width(Screen.width / 2));
        GUILayout.Label("Score", GUILayout.Width(Screen.width / 2));
        GUILayout.EndHorizontal();

        GUILayout.Space(25);

        highscore = HighScoreManager._instance.GetHighScore();
        foreach (Scores _score in highscore)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(_score.name, GUILayout.Width(Screen.width / 2));
            GUILayout.Label("" + _score.score, GUILayout.Width(Screen.width / 2));
            GUILayout.EndHorizontal();
        }

        if (GUILayout.Button("zurück zum Hauptmenu"))
        {
            GM.resetTotalScore();
            Application.LoadLevel("menu");
        }
    }
}