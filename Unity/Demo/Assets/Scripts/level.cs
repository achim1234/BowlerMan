using UnityEngine;
using System.Collections;

public class level : MonoBehaviour {


    void OnMouseUp()
    {
        SoundManager.instance.PlaySingle("stop_music");
        Application.LoadLevel("achims_level");
    } 
}
