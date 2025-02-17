using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public class AudioSettings2
    {
        public float musicVolume;
        public float soundVolume;
    }
    private string settingsPath;

    public static SoundManager instance { get; private set; }
    private AudioSource soundSource;
    private AudioSource musicSource;
    private AudioClip audioClip;
    private void Start()
    {
        settingsPath = Path.Combine(Application.persistentDataPath, "music.json");
        soundSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();
        var d = LoadAudioSettings();

        musicSource.volume = d.musicVolume;

        PlayerPrefs.SetFloat("musicVolume", d.musicVolume);
        soundSource.volume = d.soundVolume;

        PlayerPrefs.SetFloat("soundVolume", d.soundVolume);
    }
    private void Awake()
    {
        settingsPath = Path.Combine(Application.persistentDataPath, "music.json");
        soundSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();
        instance = this;


        var d = LoadAudioSettings();

        musicSource.volume = d.musicVolume;

        PlayerPrefs.SetFloat("musicVolume", d.musicVolume);
        soundSource.volume = d.soundVolume;

        PlayerPrefs.SetFloat("soundVolume", d.soundVolume);
    }
    public static void destroy()
    {
        instance.soundSource = null;
        instance.musicSource = null;
        Destroy(instance.gameObject);
        instance = null;
    }
    public void SaveAudioSettings(AudioSettings2 settings)
    {
        string json = JsonUtility.ToJson(settings);
        File.WriteAllText(settingsPath, json);
    }
    public static void SaveAudioSettings2(AudioSettings2 settings)
    {
        string json = JsonUtility.ToJson(settings);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "music.json"), json);
    }
    public AudioSettings2 LoadAudioSettings()
    {
        if (File.Exists(settingsPath))
        {
            string json = File.ReadAllText(settingsPath);
            return JsonUtility.FromJson<AudioSettings2>(json);
        }

        return new AudioSettings2 { musicVolume=1,soundVolume=1};
    }
    public static AudioSettings2 LoadAudioSettings2()
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, "music.json")))
        {
            string json = File.ReadAllText(Path.Combine(Application.persistentDataPath, "music.json"));
            return JsonUtility.FromJson<AudioSettings2>(json);
        }

        return new AudioSettings2 { musicVolume = 1, soundVolume = 1 };
    }

    public void PlaySound(AudioClip _sound)
    {
        try
        {
            soundSource.PlayOneShot(_sound);
        } catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    public void ChangeSoundVolume(float _change)
    {
        ChangeSourceVolume(1, "soundVolume", _change, soundSource);
    }
    public void ChangeMusicVolume(float _change)
    {
        ChangeSourceVolume(0.3f, "musicVolume", _change, musicSource);
    }

    private void ChangeSourceVolume(float baseVolume, string volumeName, float change, AudioSource source)
    {
        
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1);
        currentVolume += change;

        
        if (currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;

       
        float finalVolume = currentVolume * baseVolume;
        source.volume = finalVolume;

        
        PlayerPrefs.SetFloat(volumeName, currentVolume);
        if (volumeName == "musicVolume")
        {
            SaveAudioSettings(new AudioSettings2 { musicVolume = currentVolume, soundVolume = PlayerPrefs.GetFloat("soundVolume") });
        }
        else
        {
            SaveAudioSettings(new AudioSettings2 { musicVolume = PlayerPrefs.GetFloat("musicVolume"), soundVolume = currentVolume });
        }
    }

    public void ChangeSoundLevel(float change)
    {
        float currentVolume = Mathf.Clamp(change, 0.0f, 1.0f);

        soundSource.volume = currentVolume;

        PlayerPrefs.SetFloat("soundVolume", currentVolume);
    }

    public void ChangeMusicLevel(float change)
    {
        float currentVolume = Mathf.Clamp(change, 0.0f, 1.0f);

        musicSource.volume = currentVolume;

        PlayerPrefs.SetFloat("musicVolume", currentVolume);
    }
    public void PlayLongSoundForOneSecond(AudioClip _sound)
    {
        try
        {
            double startTime = AudioSettings.dspTime;
            soundSource.clip = _sound;
            soundSource.PlayScheduled(startTime);
            soundSource.SetScheduledEndTime(startTime + 3.0);
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    public void PlaySoundRepeatedly(AudioClip _sound, int repeatCount)
    {
        audioClip = _sound;
        for (int i = 0; i < repeatCount; i++)
        {
            try
            {
                Invoke("playPrivate", i*audioClip.length);
            }
            catch (System.Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }   
    }

    private void playPrivate()
    {
        try
        {
            soundSource.PlayOneShot(audioClip);
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
}