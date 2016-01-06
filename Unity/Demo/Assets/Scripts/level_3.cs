using UnityEngine;
using System.Collections;

public class level_3 : MonoBehaviour {

    GameManager GM;

    // Use this for initialization
    void Start () {
        // game manager stuff
        GM = GameManager.Instance;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseUp()
    {
        SoundManager.instance.PlaySingle("stop_music");
        GM.SetGameMode(GameMode.SingleGame); // no campaing -> single game
        Application.LoadLevel("werners_level_2");
    }
}
