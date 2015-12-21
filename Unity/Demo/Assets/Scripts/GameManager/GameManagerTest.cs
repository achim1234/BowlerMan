using UnityEngine;
using System.Collections;

/**
 * GameMangerTest - Class to test managing game states & game progress
 * http://rusticode.com/2013/12/11/creating-game-manager-using-state-machine-and-singleton-pattern-in-unity3d/
 */
public class GameManagerTest : MonoBehaviour
{

    GameManager GM;

    void Awake()
    {
        GM = GameManager.Instance;
        GM.OnStateChange += HandleOnStateChange;

        Debug.Log("Current game mode when Awakes: " + GM.gameMode);
        Debug.Log("Current game state when Awakes: " + GM.gameState);

        GM.SetGameState(GameState.Intro);
    }

    void Start()
    {
        Debug.Log("Current game mode when Starts: " + GM.gameMode);
        Debug.Log("Current game state when Starts: " + GM.gameState);
    }

    public void HandleOnStateChange()
    {
        Debug.Log("Handling state change to: " + GM.gameState);
    }

}