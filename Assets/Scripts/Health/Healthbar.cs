using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private Health playerHealth;
    private ItemCollector playerMoney;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;
    [SerializeField] private Image totalmoneyBar;
    [SerializeField] private Image currentmoneyBar;
    [SerializeField] private int maxMoney=3;
    [SerializeField] private int maxHealth=3;
    private int fruits = 0;
    [SerializeField] private Text fruitstxt;
    [SerializeField] private AudioClip collectionSoundEffect;
    bool loded = false;

    private void Start()
    {
        totalhealthBar.fillAmount = 1;
        totalmoneyBar.fillAmount = 1;
        GameLogger log = new GameLogger();
        var d = log.GetLatestLogForScene(SceneManager.GetActiveScene().buildIndex);
        if (d != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<Health>();
            Debug.Log("Loading from log");
            fruits = d.points;
            fruitstxt.text = fruits.ToString();
            playerHealth.sethealth(d.lives);
            loded = true;
            player.GetComponent<Transform>().position = d.playerPosition;
        }
    }
    private void Update()
    {
       
        
        if (playerHealth == null || playerMoney == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                playerHealth = player.GetComponent<Health>();
            }
        }

        if (playerHealth != null )
        {
            currenthealthBar.fillAmount = playerHealth.currentHealth / maxHealth;
            currentmoneyBar.fillAmount = fruits / (float)maxMoney;
        }
        if(getHealth()<=0)
        {

            FindObjectOfType<UIManager>().GameOver();
        }
    }

    public int getFruits()
    {
        return fruits;
    }
    public void setFruits(int d)
    {
        fruits = d;
    }
    public float getHealth()
    {
        return playerHealth.currentHealth;
    }
    public void increaseFruits()
    {
        SoundManager.instance.PlaySound(collectionSoundEffect);
        fruits++;
        fruitstxt.text = fruits.ToString();
    }

}