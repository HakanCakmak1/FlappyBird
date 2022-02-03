using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float epsilon = 1f;
    
    private float leftEdge;
    private bool isMoving = true;

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - epsilon;
    }

    private void FixedUpdate()
    {
        if (!isMoving) { return; }
        transform.position += Vector3.left * speed * 0.02f;

        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }

    public void StopPipe()
    {
        isMoving = false;
    }
}
