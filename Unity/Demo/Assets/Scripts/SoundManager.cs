using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioSource efxSource; //Drag a reference to the audio source which will play the sound effects.

    public AudioSource ten_sec_countdown;
    public Camera _camera;

    public AudioClip woohoo; //Bei klick auf Bowlerman
    public AudioClip game_won; //Spiel gewonnen
    public AudioClip game_lose; //Spiel verlohren
    public AudioClip hit_one_pin; //Sound bei Pin treffer
    public AudioClip power_up; //Sound der bei einsammeln eines Power Up abgespielt wird
    public AudioClip obstical_hit; // Hinderniss berührt
    public AudioClip ten_sec_left; //Zehn sekunden verbleiben noch

    public static SoundManager instance = null; //Allows other scripts to call functions from SoundManager.               


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
        DontDestroyOnLoad(gameObject);


    }

  



    //Used to play single sound clips.
    public void PlaySingle(string soundevent)
    {
        switch (soundevent)
        {
            case "woohoo_sound":
                efxSource.clip = woohoo;
                efxSource.Play();
                break;
            case "game_won":
                ten_sec_countdown.clip = game_won;
                ten_sec_countdown.Play();
                break;
            case "game_lose":
                efxSource.clip = game_lose;
                efxSource.Play();
                break;
            case "pin_hit":
                efxSource.clip = hit_one_pin;
                efxSource.Play();
                break;
            case "power_up":
                efxSource.clip = power_up;
                efxSource.Play();
                break;
            case "obstical_hit":
                efxSource.clip = obstical_hit;
                efxSource.Play();
                break;
            case "ten_sec_left":
                ten_sec_countdown.clip = ten_sec_left;
                ten_sec_countdown.Play();
                break;
            case "pause_music":
                //efxSource.Pause();
                _camera.GetComponent<AudioSource>().Pause();
                break;
            case "start_music":
                //efxSource.Play();
                _camera.GetComponent<AudioSource>().Play();
                break;
            default:
                Debug.Log("Default");
                break;
        }
    }
}
