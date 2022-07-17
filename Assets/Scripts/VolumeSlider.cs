using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private UnityEvent<float> changeVolume;
    void Start(){
        slider.onValueChanged.AddListener(val => changeVolume.Invoke(val));
    }
}
