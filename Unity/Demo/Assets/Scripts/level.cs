using UnityEngine;
using System.Collections;

public class level : MonoBehaviour {


    public float speed = 1.0f;
    public float verschiebung_z;

    Vector3 target;
    public GameObject objekt;
	public GameObject objekt2;

    public bool mausklick = false;


    public GameObject level2;
    public GameObject level2_locked;
    public GameObject level3;
    public GameObject level3_locked;


	private float duration = 2.5f;
	private float t = 0f;


    void FixedUpdate()
    {
        if (mausklick)
        {
			objekt2.GetComponent<BoxCollider>().enabled = false;
			objekt.transform.position = Vector3.Lerp(objekt.transform.position, target, t);
			if(t<1.0f)
			{
				t+= Time.deltaTime /duration;
			}
			if(Mathf.Approximately(objekt.transform.position.z, target.z))
			{
				mausklick = false;
				t = 0.0f;
				objekt2.GetComponent<BoxCollider>().enabled = true;
			}

        }
    }


//    void OnCollisionEnter(Collision col)
//    {
//        if(col.gameObject.name == "target_wall_left")
//        {
//            
//            mausklick = false;
//        }
//    }


    void Start()
    {
        target = objekt.transform.position + new Vector3(0, 0, verschiebung_z);

        
        // verify if level 2 is unlocked
        if (PlayerPrefs.GetInt("unlocked_level_2")  == 1)
        {
            level2.SetActive(true);
            level2_locked.SetActive(false);
        }
        else
        {
            level2.SetActive(false);
            level2_locked.SetActive(true);
        }

        // verify if level 2 is unlocked
        if (PlayerPrefs.GetInt("unlocked_level_3") == 1)
        {
            level3.SetActive(true);
            level3_locked.SetActive(false);
        }
        else
        {
            level3.SetActive(false);
            level3_locked.SetActive(true);
        }


    }


    void OnMouseUp()
    {
        this.target = objekt.transform.position + new Vector3(0, 0, verschiebung_z);
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
