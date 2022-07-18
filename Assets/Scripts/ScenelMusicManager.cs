using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenelMusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip menuClip;
    [SerializeField] private AudioClip castleClip;
    [SerializeField] private AudioClip cutClip;
    void Start()
    {
        SceneManager.sceneLoaded += StartPlayMusic;
        //SoundManager.instance.PlayMusic(castleClip);
        string naym = SceneManager.GetActiveScene().name;
        switch (naym)
        {
            case ("MainMenu"):
                SoundManager.instance.PlayMusic(menuClip);
                break;
            case ("Castle1"):
                SoundManager.instance.PlayMusic(castleClip);
                break;
            case ("Cutscene"):
                SoundManager.instance.PlayMusic(cutClip);
                break;
        }
    }
    private void StartPlayMusic(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case ("MainMenu"):
                SoundManager.instance.PlayMusic(menuClip);
                break;
            case ("Castle1"):
                SoundManager.instance.PlayMusic(castleClip);
                break;
            case ("Cutscene"):
                SoundManager.instance.PlayMusic(cutClip);
                break;
        }
    }
}
