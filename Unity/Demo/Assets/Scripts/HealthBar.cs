using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

    public Image image;

    public static float fillAmount = 1;

    // https://www.youtube.com/watch?v=y3OZXMxsrUI

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        image.fillAmount = fillAmount;
        if(fillAmount > 0.5f)
        {
            image.color = Color.green;
        }
        else if(fillAmount < 0.5f && fillAmount > 0.2f)
        {
            image.color = Color.blue;
        }
        else
        {
            image.color = Color.red;
        }

	
	}
}
