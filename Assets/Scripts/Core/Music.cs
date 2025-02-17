using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    private UIManager menager;
    private Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
        menager=GameObject.FindObjectOfType<UIManager>();
        slider.value=menager.getMusicVolume();
        slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
    }

    private void OnSliderValueChanged()
    {
        menager.MusicVolume(slider.value);
        SoundManager.SaveAudioSettings2(new SoundManager.AudioSettings2 { soundVolume = PlayerPrefs.GetFloat("soundVolume"), musicVolume = slider.value });
    }
}
