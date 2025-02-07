using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set;}
    public Player player;
    public Obstacle obstacle;
    public SpawnEnemy spawnEnemy;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text FinalscoreText;
    [SerializeField] private GameObject StartPanal;
    [SerializeField] private GameObject GameOverPanal;
    [SerializeField] private GameObject PausePanal;
    [SerializeField] private GameObject PauseButtonAndSoundButton;
    [SerializeField] private CircleCollider2D  spawnPlayer;
    [SerializeField] private GameObject HealthText;
    
    [SerializeField] private AudioSource BgSound;
    [SerializeField] private Image imageSound;
    [SerializeField] private Sprite SoundOn;
    [SerializeField] private Sprite SoundOff;

    private bool soundtoggle = false;
    private float score;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }

    void Start()
    {
        Time.timeScale = 0;
        player = FindObjectOfType<Player>();
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().isDashing = true;
    }

    private void Update()
    {
        scoreCount();
    }

    public void NewGame()
    {
        Time.timeScale = 1;
        Enemy[] enemys = FindObjectsOfType<Enemy>();
        Bullet[] bullets = FindObjectsOfType<Bullet>();
        foreach (var enemy in enemys)
        {
            Destroy(enemy.gameObject);
        }
        foreach (var bullet in bullets)
        {
            Destroy(bullet.gameObject);
        }
        
        score = 0;

        gameSpeed = initialGameSpeed;
        enabled = true;

        player.currentLife = 3;
        obstacle.ResetRotate();

        player.gameObject.transform.position = GetRandomSpawnPosition();
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().isDashing = false;
        
        if (!soundtoggle)
        {
            BgSound.Stop();
            BgSound.Play();
        }

        spawnEnemy.currentRound = 1;
        spawnEnemy.timeToSpawn = 0;

    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        PausePanal.SetActive(true);
        GameObject.FindWithTag("Player").GetComponent< PlayerMovement >().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().isDashing = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        PausePanal.SetActive(false);
        HealthText.SetActive(false);
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().isDashing = false;
    }

    public void GameOver()
    {
        Time.timeScale = 0;

        gameSpeed = 0;
        enabled = false;

        obstacle.timeToChange = 0;
        obstacle.ChangeRotate = true;

        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        GameOverPanal.SetActive(true);
        HealthText.SetActive(false);
        PauseButtonAndSoundButton.SetActive(false);
        scoreText.gameObject.SetActive(false);

        BgSound.Stop();
    }

    public void scoreCount()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime * 2;
        scoreText.text = Mathf.FloorToInt(score).ToString("D6");
        FinalscoreText.text = Mathf.FloorToInt(score).ToString("D6");
    }

    public void StartGame()
    {
        NewGame();
        Time.timeScale = 1;

        StartPanal.SetActive(false);
        GameOverPanal.SetActive(false);
        PauseButtonAndSoundButton.SetActive(true);
        PausePanal.SetActive(false);
        HealthText.SetActive(true);
        scoreText.gameObject.SetActive(true);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        if (spawnPlayer == null) return player.transform.position;

        Vector2 center = spawnPlayer.bounds.center;
        float radius = spawnPlayer.radius * spawnPlayer.transform.lossyScale.x;

        Vector2 randomPoint = Random.insideUnitCircle * radius;
        
        return new Vector3(center.x + randomPoint.x, center.y + randomPoint.y, 0);
    }

    public void SoundToggleButton()
    {
        if(!soundtoggle)
        {
            soundtoggle = true;
            imageSound.sprite = SoundOff;
            BgSound.Pause();
        }
        else
        {
            soundtoggle = false;
            imageSound.sprite = SoundOn;
            BgSound.Play();
        }
    }
}
