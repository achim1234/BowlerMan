using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuController : MonoBehaviour
{

    string username = "";
    string score = "";
    bool added_highscore = false;

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
        // Key combination (STRG + Shift + R) to reset highscore
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Debug.Log("pressed key combinatio to reset highscore");
                    // CTRL + R
                    //if (GUILayout.Button("Clear Leaderboard"))
                    //{
                        HighScoreManager._instance.ClearLeaderBoard();
                    //}
                }
            }
        }
    }


    void OnGUI()
    {
        if(GM.totalscore > 0) 
        {
            if (added_highscore == false)
            {
                int space_left = Screen.width / 2 - 200; // calc left space

                GUILayout.BeginArea(new Rect(space_left, 50, 200, 650));

                GUILayout.BeginHorizontal();
                GUILayout.Label("Name :");
                username = GUILayout.TextField(username, 20);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Score :");
                GUILayout.Label("" + GM.totalscore);
                score = GM.totalscore.ToString();
                GUILayout.EndHorizontal();

                // add button
                if (GUILayout.Button("Add Score"))
                {
                    if (!username.Equals("")) // did user enter a name
                    {
                        added_highscore = true;
                        HighScoreManager._instance.SaveHighScore(username, System.Int32.Parse(score));
                        highscore = HighScoreManager._instance.GetHighScore();
                    }
                }
                GUILayout.EndArea();

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


        } // player has no points -> just show highcore
        else
        {
            showHighScore();
        }
    }

    public void showHighScore()
    {

        int space_left = Screen.width / 2 - 200;

        GUILayout.BeginArea(new Rect(space_left, 50, 650, 650));
        GUILayout.BeginHorizontal();
        //GUILayout.Label("Name", GUILayout.Width(Screen.width / 2));
        GUILayout.Label("Name");
        //GUILayout.Label("Score", GUILayout.Width(Screen.width / 2));
        GUILayout.Label("Score");
        GUILayout.EndHorizontal();

        GUILayout.Space(15);

        highscore = HighScoreManager._instance.GetHighScore();
        foreach (Scores _score in highscore)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(_score.name, GUILayout.Width(323));
            GUILayout.Label("" + _score.score);
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(60);
        //GUILayout.FlexibleSpace();
        if (GUILayout.Button("zurück zum Hauptmenu", GUILayout.Width(200)))
        {
            GM.resetTotalScore();
            Application.LoadLevel("menu");
        }
        //GUILayout.FlexibleSpace();
        GUILayout.EndArea();
    }
}