using UnityEngine;
using System.Collections;

public class level : MonoBehaviour {


    public float speed = 1.0f;
    public float verschiebung_z;

    Vector3 target;
    public GameObject objekt;

    public bool mausklick = false;


    void FixedUpdate()
    {
        if (mausklick)
        {
            objekt.transform.position = Vector3.Lerp(objekt.transform.position, target, speed * Time.deltaTime);
        }
    }


    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "target_wall_left")
        {
            
            mausklick = false;
        }
    }


    void Start()
    {
        target = objekt.transform.position + new Vector3(0, 0, verschiebung_z);

    }


    void OnMouseUp()
    {
        target = objekt.transform.position + new Vector3(0, 0, verschiebung_z);
        mausklick = true;

    }

    /*
    void OnMouseUp()
    {
        SoundManager.instance.PlaySingle("stop_music");
        Application.LoadLevel("daniels_level");
    } 
    */
}
