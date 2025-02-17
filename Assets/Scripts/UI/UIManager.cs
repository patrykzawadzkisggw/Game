using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Win")]
    [SerializeField] private GameObject winScreen;
    [SerializeField] private AudioClip winSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    [Header("Min Score")]
    [SerializeField] private bool isMin = false;
    [SerializeField] private int minScore = 20;
    public bool isGame = true;
    private bool isone = true;

    [SerializeField] private GameObject SoundsScreen;
    private float? music;
    private float? sound;

    private void Awake()
    {
        if(isGame)
        {
            gameOverScreen.SetActive(false);
            pauseScreen.SetActive(false);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGame)
        {

            PauseGame(!pauseScreen.activeInHierarchy);
        }
    }

    #region Game Over
 
    public void GameOver()
    {
        if (isMin && GameObject.FindObjectOfType<Healthbar>().getFruits()>=minScore) {
            if(isone)
            {
                 isone = false;
                GameLogger log = new GameLogger();
                Healthbar healthbar = GameObject.FindObjectOfType<Healthbar>();
                log.AddLogEntry(SceneManager.GetActiveScene().buildIndex, healthbar.getFruits(), healthbar.getHealth(), transform.position, true);

            }
            
            Win();

        } else
        {
            gameOverScreen.SetActive(true);
            //SoundManager.instance.PlaySound(gameOverSound);
            Invoke("Restart", 2f);
        }
        
    }
    public void OpenSoundsScreen()
    {
        SoundsScreen.SetActive(true);
        var d = SoundManager.LoadAudioSettings2();
        music = d.musicVolume;
        sound = d.soundVolume;
    }

    public void CloseSoundsScreen()
    {
        SoundsScreen.SetActive(false);
    }
    public void CloseSoundsScreenWithoutSaving()
    {
        SoundsScreen.SetActive(false);
        if(music != null && sound!=null)
        {
            SoundManager.SaveAudioSettings2(new SoundManager.AudioSettings2 { soundVolume = (float)sound, musicVolume = (float)music });
            SoundManager.instance.ChangeMusicLevel((float)music);
            SoundManager.instance.ChangeSoundLevel((float)sound);
        }
    }
    
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(7);
        //SoundManager.destroy();
    }

    public void ResetGame()
    {
        new GameLogger().DeleteLogs();
    }

   
    public void Quit()
    {
        Application.Quit(); 

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
#endif
    }
    #endregion

    #region Pause
    public void PauseGame(bool status)
    {

        pauseScreen.SetActive(status);


        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    public void SoundVolume(float level)
    {
        SoundManager.instance.ChangeSoundLevel(level);
    }
    public void MusicVolume(float level)
    {
        SoundManager.instance.ChangeMusicLevel(level);
    }

    public float getMusicVolume()
    {
        return PlayerPrefs.GetFloat("musicVolume");
    }
    public float getSoundVolume()
    {
        return PlayerPrefs.GetFloat("soundVolume");
    }
    #endregion
    public void Win()
    {
        winScreen.SetActive(true);
        SoundManager.instance.PlayLongSoundForOneSecond(winSound);
        Invoke("CompleteLevel", 2f);
    }
    public void Win(int scene,int fruits,float live,Vector3 pos,bool isfinished)
    {
        GameLogger log = new GameLogger();
        log.AddLogEntry(scene, fruits, live, pos, isfinished);
        winScreen.SetActive(true);
        SoundManager.instance.PlaySound(winSound);
        Invoke("CompleteLevel", 2f);
    }
    private void CompleteLevel()
    {
        SceneManager.LoadScene(7);
    }
}
