using UnityEngine;
using System.Collections;

public class level : MonoBehaviour {
    
    /*
    public void startgame()
    {
        Application.LoadLevel("demo");
    }
    */

    void OnMouseUp()
    {
        Application.LoadLevel("obstacles");
    } 
}
