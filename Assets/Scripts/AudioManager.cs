using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour {

    public static AudioManager instance = null;

    public AudioClip pointScoredClip;
    public AudioClip playerDiedClip;

    private AudioSource backgroundAudioSource;
    private AudioSource soundEffectsAudioSource;
    private bool muted = false;


    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        backgroundAudioSource = audioSources[0];
        soundEffectsAudioSource = audioSources[1];
    }

    public bool IsMuted() 
    {
        return muted;
    }

    public void ToggleMute() 
    {
        muted = !muted;
        backgroundAudioSource.mute = muted;
        soundEffectsAudioSource.mute = muted;
    }

    public void SetMute(bool newMuted) 
    {
        muted = newMuted;
        backgroundAudioSource.mute = muted;
        soundEffectsAudioSource.mute = muted;
    }

    public void PlayPointScoredSound() 
    {
        soundEffectsAudioSource.Stop();
        soundEffectsAudioSource.clip = pointScoredClip;
        soundEffectsAudioSource.volume = 0.5f;
        soundEffectsAudioSource.Play();
    }

    public void PlayPlayerDiedSound() 
    {
        soundEffectsAudioSource.Stop();
        soundEffectsAudioSource.clip = playerDiedClip;
        soundEffectsAudioSource.volume = 1.0f;
        soundEffectsAudioSource.Play();
    }

}
