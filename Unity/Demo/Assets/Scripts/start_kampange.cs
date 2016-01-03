using UnityEngine;
using System.Collections;

public class start_kampange : MonoBehaviour {

    void OnMouseUp()
    {
        SoundManager.instance.PlaySingle("stop_music");
        Application.LoadLevel("achims_level_2");
    }
}
