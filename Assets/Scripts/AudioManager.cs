using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour {

    public static AudioManager instance = null;

    public AudioClip pointScoredClip;

    private AudioSource audioSource;
    private bool muted = false;


    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public bool IsMuted() 
    {
        return muted;
    }

    public void ToggleMute() 
    {
        muted = !muted;
        audioSource.mute = muted;
    }

    public void SetMute(bool newMuted) 
    {
        muted = newMuted;
        audioSource.mute = muted;
    }

    public void PlayPointScoredSound() 
    {
        audioSource.PlayOneShot(pointScoredClip);
    }

}
