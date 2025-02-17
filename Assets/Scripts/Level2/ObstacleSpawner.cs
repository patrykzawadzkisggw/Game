using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacles;
    public float minSpawnTime, maxSpawnTime;
    void Start()
    {
        StartCoroutine("Spawn");
    }

    

    IEnumerator Spawn()
    {
        float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
        yield return new WaitForSeconds(waitTime);
        while(true)
        {
            SpawnObstacle();
            waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);
            
        }
    }

    void SpawnObstacle()
    {  
        GameObject obstacle=Instantiate(obstacles[Random.Range(0, obstacles.Length)], transform.position, Quaternion.identity);
        if (obstacle.CompareTag("Cherry"))
        {
            obstacle.AddComponent<Rigidbody2D>();
            obstacle.AddComponent<Obstacle>();
            obstacle.AddComponent<BoxCollider2D>().size = new Vector2(0.1f, 0.1f); 
        } else
        {
            obstacle.AddComponent<Obstacle>();
            EnemyDamage d=obstacle.AddComponent<EnemyDamage>();
            d.SetDamage(1);
            obstacle.GetComponent<BoxCollider2D>().size = new Vector2(0.18f, 0.18f);
            BoxCollider2D boxCollider = obstacle.AddComponent<BoxCollider2D>();


            boxCollider.isTrigger = true;
            boxCollider.size = new Vector2(0.2f, 0.2f);
        }
        
        

    }
}
