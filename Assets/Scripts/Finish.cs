using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public AudioClip winSound;
    private UIManager UIManager;
    // Start is called before the first frame update
    void Start()
    {
        UIManager = FindObjectOfType<UIManager>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            UIManager.Win();
            GameLogger log = new GameLogger();
            Healthbar healthbar = GameObject.FindObjectOfType<Healthbar>();
            log.AddLogEntry(SceneManager.GetActiveScene().buildIndex, healthbar.getFruits(), healthbar.getHealth(), transform.position, true);
            //SoundManager.PlaySound(winSound);
        }
    }
}
