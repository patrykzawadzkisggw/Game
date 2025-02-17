using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    private UIManager menager;
    private Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
        menager = GameObject.FindObjectOfType<UIManager>();
        slider.value = menager.getSoundVolume();
        slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
    }

    private void OnSliderValueChanged()
    {
        menager.SoundVolume(slider.value);
        SoundManager.SaveAudioSettings2(new SoundManager.AudioSettings2 { soundVolume = slider.value, musicVolume = PlayerPrefs.GetFloat("musicVolume") });
            
    }
}
