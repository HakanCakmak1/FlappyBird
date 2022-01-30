using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float gravity = -10f;
    [SerializeField] private float strength;
    [SerializeField] private float animationPeriod;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private int notFlyingIndex = 1;
    
    private Vector3 direction;
    private SpriteRenderer spriteRenderer;
    private int spriteIndex;
    private bool isFlying;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Animate), animationPeriod, animationPeriod);
    }

    private void Update()
    {
        HandleInput();
        Physics();
        SetFlying();
    }

    private void Physics()
    {
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    private void SetFlying()
    {
        if (direction.y > 0)
        {
            isFlying = true;
        } 
        else
        {
            isFlying = false;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                direction = Vector3.up * strength;
            }
        }
    }

    private void Animate()
    {
        if (isFlying)
        {
            spriteIndex++;
            spriteIndex %= sprites.Length;
        }
        else
        {
            spriteIndex = notFlyingIndex;
        }
        spriteRenderer.sprite = sprites[spriteIndex];
    }
}
