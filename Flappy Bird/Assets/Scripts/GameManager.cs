using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerScript player;
    [SerializeField] private Spawner spawner;
    [SerializeField] private Environment[] environments;
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject readyScreen;
    [SerializeField] private GameObject scoreScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private Image prizeImage;
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Sprite continueSprite;
    [SerializeField] private Sprite bronze;
    [SerializeField] private Sprite silver;
    [SerializeField] private Sprite gold;
    [SerializeField] private Sprite platinium;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text endScoreText;
    [SerializeField] private TMP_Text endHighScoreText;

    public enum Sound
    {
        Wing,
        Coin,
        Hit,
        Fall
    }

    private int score;
    private int highScore;
    private bool isPaused;
    private Image pauseImage;
    private string HighScoreKey = "HighScore";

    public void Awake()
    {
        pauseImage = pauseButton.GetComponent<Image>();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void StartGame()
    {
        startScreen.SetActive(false);
        readyScreen.SetActive(true);
        scoreScreen.SetActive(true);
        player.StartGame();
    }

    public void Ready()
    {
        spawner.enabled = true;
        pauseButton.SetActive(true);
        readyScreen.SetActive(false);
    }

    public void GameOver()
    {
        scoreScreen.SetActive(false);
        blackScreen.SetActive(true);
        gameOverScreen.SetActive(true);

        highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        if (highScore < score)
        {
            highScore = score;
            PlayerPrefs.SetInt(HighScoreKey, highScore);
        }
        endScoreText.text = score.ToString();
        endHighScoreText.text = highScore.ToString();

        Sprite sprite;
        if (score < 10)
        {
            sprite = null;
            prizeImage.color = new Color(0,0,0,0);
        }
        else if (score < 20) sprite = bronze;
        else if (score < 50) sprite = silver;
        else if (score < 100) sprite = gold;
        else sprite = platinium;

        prizeImage.sprite = sprite;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1f;
            pauseImage.sprite = pauseSprite;
            blackScreen.SetActive(false);
        }
        else
        {
            isPaused = true;
            Time.timeScale = 0f;
            pauseImage.sprite = continueSprite;
            blackScreen.SetActive(true);
        }
    }

    public void StopTheWorld()
    {
        PipeMovement[] pipes = FindObjectsOfType<PipeMovement>();
        foreach (PipeMovement pipe in pipes)
        {
            pipe.StopPipe();
        }
        foreach (Environment environment in environments)
        {
            environment.StopEnvironment();
        }
        spawner.StopSpawn();
    }
}
