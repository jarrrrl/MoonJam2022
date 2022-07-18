using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource, effectSource;
    public static SoundManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void PlaySound(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.Stop();
        musicSource.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float value){

        AudioListener.volume = value;
    }

    public void ChangeMusicVolume(float value)
    {
        musicSource.volume = value;
    }
    public void ChangeEffectsVolume(float value)
    {
        effectSource.volume = value;
    }

    //this row adds reference to audio file
    //[SerializeField] private AudioClip clip;

    //this command plays this clip once
    //SoundManager.instance.PlaySound(clip); for effects
    //SoundManager.instance.PlayMusic(clip); for music
}
