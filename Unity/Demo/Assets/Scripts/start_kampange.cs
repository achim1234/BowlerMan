using UnityEngine;
using System.Collections;

public class start_kampange : MonoBehaviour {

    GameManager GM;

    // Use this for initialization
    void Start()
    {
        // game manager stuff
        GM = GameManager.Instance;
    }

    void OnMouseUp()
    {
        SoundManager.instance.PlaySingle("stop_music");
        GM.SetGameMode(GameMode.Campaign); // no single game -> campaing through all levels
        GM.SetLives(3);
        Application.LoadLevel("achims_level_2");
    }
}
