using UnityEngine;
using System.Collections;

public class changeTexture : MonoBehaviour {

    public Texture textur;
    public Texture currenttextur;

    public GameObject objekt;
    Renderer rend;

    void OnMouseOver()
    {
        rend.material.mainTexture = textur;
    }

    void OnMouseExit()
    {
        rend.material.mainTexture = currenttextur;
    }

    void Start()
    {
        rend = objekt.GetComponent<Renderer>();
    }
}
