using UnityEngine;
using UnityEngine.Tilemaps;

public class MovingTilemap : MonoBehaviour
{
    public float speed = 1f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * speed, 1);
        transform.position = startPosition + Vector3.left * newPosition;
    }
}