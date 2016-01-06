using UnityEngine;
using System.Collections;

public class script_lives : MonoBehaviour {

    public int lives = 3;


    GameObject RawImage1;
    GameObject RawImage2;
    GameObject RawImage3;

    // Use this for initialization
    void Start () {
        RawImage1 = GameObject.Find("RawImage1");
        RawImage2 = GameObject.Find("RawImage2");
        RawImage3 = GameObject.Find("RawImage3");
    }
	
	// Update is called once per frame
	void Update () {

        switch (lives)
        {
            case 0:
                RawImage1.SetActive(false);
                RawImage2.SetActive(false);
                RawImage3.SetActive(false);
                break;
            case 1:
                RawImage1.SetActive(true);
                RawImage2.SetActive(false);
                RawImage3.SetActive(false);
                break;
            case 2:
                RawImage1.SetActive(true);
                RawImage2.SetActive(true);
                RawImage3.SetActive(false);
                break;
            case 3:
                RawImage1.SetActive(true);
                RawImage2.SetActive(true);
                RawImage3.SetActive(true);
                break;
            default:
                break;
        }

    }


}
