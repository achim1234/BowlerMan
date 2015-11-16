using UnityEngine;
using System.Collections;

public class woohoo : MonoBehaviour {

    public AudioClip myclip;

    // Use this for initialization
    void Start()
    {
        this.gameObject.AddComponent<AudioSource>();
        this.GetComponent<AudioSource>().clip = myclip;
    }

    void OnMouseDown()
    {
        this.GetComponent<AudioSource>().Play();
    }
}
