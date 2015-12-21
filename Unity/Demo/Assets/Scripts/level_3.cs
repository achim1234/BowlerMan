using UnityEngine;
using System.Collections;

public class level_3 : MonoBehaviour {

    GameManager GM;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseUp()
    {
        SoundManager.instance.PlaySingle("stop_music");
        GM.SetGameMode(GameMode.SingleGame);
        Application.LoadLevel("werners_level");
    }
}
