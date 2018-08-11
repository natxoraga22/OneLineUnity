using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour {

    public static AudioManager instance = null;

    private bool muted = false;


    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public bool IsMuted() 
    {
        return muted;
    }

    public void ToggleMute() 
    {
        muted = !muted;
        gameObject.GetComponent<AudioSource>().mute = muted;
    }

}
