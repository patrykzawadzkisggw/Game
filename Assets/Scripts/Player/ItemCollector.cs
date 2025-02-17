using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private Healthbar health;
    private void Start()
    {
        health = GameObject.FindObjectOfType<Healthbar>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
           
            Destroy(collision.gameObject);
            health.increaseFruits();
        }
    }
}
