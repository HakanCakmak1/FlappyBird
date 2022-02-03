using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float gravity = -10f;
    [SerializeField] private float dyingGravity = -8f;
    [SerializeField] private float gameStartPosition = -0.85f;
    [SerializeField] private float strength;
    [SerializeField] private float animationPeriod;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private int notFlyingIndex = 1;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip wingSound;
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip fallSound;
    
    private Vector3 direction;
    private SpriteRenderer spriteRenderer;
    private int spriteIndex;
    private bool isFlying = true;
    private bool isStarted;
    private bool isReady;
    private bool isDead;
    private bool isDying;

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
        if (isReady && !isDying)
        {
            HandleInput();
        }
    }

    private void FixedUpdate() 
    {
        if (isStarted && !isDead)
        {
            SetFlying();
            Physics();
        }      
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Pipe" && !isDying)
        {
            isFlying = false;
            isDying = true;
            StopTheWorld();
            gravity = dyingGravity;
            audioSource.PlayOneShot(hitSound);
            audioSource.PlayOneShot(fallSound);
        }
        else if (other.tag == "Score")
        {
            gameManager.IncreaseScore();
            audioSource.PlayOneShot(coinSound);
        }
        else if (other.tag == "Ground")
        {
            isDead = true;
            if (!isDying) StopTheWorld();
            gameManager.GameOver();
        }
    }

    private void Physics()
    {
        direction.y += gravity * 0.02f;
        transform.position += direction * 0.02f;
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
            InputFunction();
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                InputFunction();
            }
        }
    }

    private void InputFunction()
    {
        if (!isStarted) Ready();
        direction = Vector3.up * strength;
        audioSource.PlayOneShot(wingSound);
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

    private void StopTheWorld()
    {
        gameManager.StopTheWorld();
    }

    private void Ready()
    {
        isStarted = true;
        gameManager.Ready();
    }

    public void StartGame()
    {
        isReady = true;
        gameObject.transform.position += new Vector3(gameStartPosition, 0, 0);
    }
}
