using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioSource efxSource;                           //Drag a reference to the audio source which will play the sound effects.
    public AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.


    public AudioClip woohoo;                      //Bei klick auf Bowlerman
    public AudioClip game_won;                    //Spiel gewonnen


    public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.             
 


    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        //DontDestroyOnLoad(gameObject);
    }


    //Used to play single sound clips.
    public void PlaySingle(string soundevent)
    {

        switch (soundevent)
        {
            case "woohoo_sound":
                //Debug.Log("case tritt ein");
                efxSource.clip = woohoo;
                efxSource.Play();
                break;

            case "game_won":
                efxSource.clip = game_won;
                efxSource.Play();
                break;

            default:
                Debug.Log("Default");
                break;
        }
    }
}
