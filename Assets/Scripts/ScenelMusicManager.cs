using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenelMusicManager : MonoBehaviour
{    
    [SerializeField] private AudioClip clip;
    void Start()
    {
        SoundManager.instance.PlayMusic(clip);
    }
}
