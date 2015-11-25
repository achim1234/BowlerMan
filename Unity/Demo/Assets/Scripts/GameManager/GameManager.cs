using UnityEngine;

public enum GameState { NullState, Intro, MainMenu, Game }
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
    protected GameManager() { }

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
}