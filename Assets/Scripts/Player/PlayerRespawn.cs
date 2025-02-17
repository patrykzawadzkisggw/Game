using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpoint;
    private Transform currentCheckpoint;
    private Healthbar healthbar;
    private UIManager uiManager;

    private void Awake()
    {
        
       healthbar = GameObject.FindObjectOfType<Healthbar>();
       uiManager = FindObjectOfType<UIManager>();
    }

    public void RespawnCheck()
    {
        if (currentCheckpoint == null)
        {
            uiManager.GameOver();
            return;
        }

        uiManager.Restart();
        transform.position = currentCheckpoint.position; 


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(checkpoint);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("activate");
            GameLogger log = new GameLogger();
            log.AddLogEntry(SceneManager.GetActiveScene().buildIndex, healthbar.getFruits(), healthbar.getHealth(), transform.position, false);
        }
    }
}
