using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject block;
    public GameObject[] fruits;
    public float maxX;
    public Transform spawnPoint;
    public float spawnRate;
    int i = 1;
    private void Start()
    {
        StartSpawning();
    }
    private void StartSpawning()
    {
        InvokeRepeating("SpawnBlock", 0.5f, spawnRate);
    }
    private void SpawnBlock()
    {
        if (i % 10 == 0)
        {
            SpawnFruit();
            return;
        }
        Vector3 spawnPos = spawnPoint.position;
        spawnPos.x = Random.Range(-maxX, maxX);
        GameObject newBlock = Instantiate(block, spawnPos, Quaternion.identity);
        newBlock.AddComponent<Block>();
        
        BoxCollider2D boxCollider = newBlock.GetComponent<BoxCollider2D>();
        if (boxCollider != null)
        {
            boxCollider.isTrigger = true;
        }
        EnemyDamage d = newBlock.AddComponent<EnemyDamage>();
        d.SetDamage(1);
        i++;
    }

    private void SpawnFruit()
    {
        Vector3 spawnPos = spawnPoint.position;
        spawnPos.x = Random.Range(-maxX, maxX);
        GameObject newFruit = Instantiate(fruits[Random.Range(0, fruits.Length)], spawnPos, Quaternion.identity);
        Rigidbody2D ff =newFruit.AddComponent<Rigidbody2D>();
        ff.drag= 1f;
        newFruit.AddComponent<Block>();
        i++;
    }
}
