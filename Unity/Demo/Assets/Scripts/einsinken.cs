using UnityEngine;
using System.Collections;

public class einsinken : MonoBehaviour {


    public float speed = 1.0f;
    public float verschiebung_y;
    bool kollision;

    Vector3 target;
  

    void OnTriggerEnter()
    {
        kollision = true;
    }


    void FixedUpdate()
    {
        if (kollision == true)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, target, speed * Time.deltaTime);
        }
    }



    void Start()
    {
        target = this.transform.position + new Vector3(0, verschiebung_y, 0);
        kollision = false;

    }

}
