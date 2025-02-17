using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float moveSpeed=12f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void Start()
    {
        rb.velocity = Vector2.left * moveSpeed;
    }

    void Update()
    {

        if (transform.position.x < -7f)
        {
            Destroy(gameObject);
        }   
    }
}
