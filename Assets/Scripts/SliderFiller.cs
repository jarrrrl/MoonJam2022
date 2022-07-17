using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderFiller : MonoBehaviour
{
    [SerializeField] private bool MasterVolume, MusicVolume, EffectsVolume;
    private Image image;
    private Scrollbar scrollbar;
    void Awake()
    {
        scrollbar = gameObject.GetComponent<Scrollbar>();
        image = gameObject.GetComponent<Image>();
        scrollbar.onValueChanged.AddListener(val => ChangeSliderAndVolume(val));
        image.fillAmount = scrollbar.value;
    }
    private void ChangeSliderAndVolume(float val)
    {
        image.fillAmount = val;
        if (MasterVolume) SoundManager.instance.ChangeMasterVolume(val);
        if (MusicVolume) SoundManager.instance.ChangeMusicVolume(val);
        if (EffectsVolume) SoundManager.instance.ChangeEffectsVolume(val);
    }
}
