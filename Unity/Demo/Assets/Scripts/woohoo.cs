using UnityEngine;
using System.Collections;

public class woohoo : MonoBehaviour {


    // Use this for initialization
    void Start()
    {
        
    }

    void OnMouseDown()
    {
        SoundManager.instance.PlaySingle("woohoo_sound");
    }
}
