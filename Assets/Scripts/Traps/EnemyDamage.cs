using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            var t = collision.GetComponent<Health>();
            t.TakeDamage(damage);
               if(t==null || t.currentHealth<=0) GameObject.FindObjectOfType<UIManager>().GameOver();
        }
            
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
}