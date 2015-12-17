using UnityEngine;

public enum GameState { NullState, Intro, MainMenu, Loading, Countdown, Game, GamePaused, FinishedLevel, HighScore, GameOver }
public delegate void OnStateChangeHandler();

/**
 * GameManger - Class to manage game states & game progress
 * http://rusticode.com/2013/12/11/creating-game-manager-using-state-machine-and-singleton-pattern-in-unity3d/
 */
public class GameManager
{


    private static GameManager _instance = null;
    public event OnStateChangeHandler OnStateChange;

    public GameState gameState { get; private set; }
    public int totalscore { get; set; }
    public string currentscene { get; set; }


    protected GameManager() {
        totalscore = 0;
    }

    

    // Singleton pattern implementation
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        if (OnStateChange != null)
        {
            OnStateChange();
        }
    }

    public void SetTotalScore(int score)
    {
        this.totalscore += score;
    }

    public void resetTotalScore()
    {
        this.totalscore = 0;
    }

    public void SetCurrentSceneName(string scene)
    {
        this.currentscene = scene;
    }
}