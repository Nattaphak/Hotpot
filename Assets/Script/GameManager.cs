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

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject StartPanal;
    [SerializeField] private GameObject GameOverPanal;
    [SerializeField] private CircleCollider2D  spawnPlayer;

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
    }

    private void Update()
    {
        scoreCount();
    }

    public void NewGame()
    {
        Time.timeScale = 1;

        gameSpeed = initialGameSpeed;
        enabled = true;

        player.currentLife = 3;
        obstacle.ResetRotate();

        player.gameObject.transform.position = GetRandomSpawnPosition();

    }

    public void GameOver()
    {
        Time.timeScale = 0;

        gameSpeed = 0;
        enabled = false;

        obstacle.timeToChange = 0;
        obstacle.ChangeRotate = true;

        GameOverPanal.SetActive(true);
    }

    public void scoreCount()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime * 2;
        scoreText.text = Mathf.FloorToInt(score).ToString("D6");
    }

    public void StartGame()
    {
        NewGame();
        Time.timeScale = 1;

        StartPanal.SetActive(false);
        GameOverPanal.SetActive(false);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        if (spawnPlayer == null) return player.transform.position;

        Vector2 center = spawnPlayer.bounds.center;
        float radius = spawnPlayer.radius * spawnPlayer.transform.lossyScale.x;

        Vector2 randomPoint = Random.insideUnitCircle * radius;
        
        return new Vector3(center.x + randomPoint.x, center.y + randomPoint.y, 0);
    }

}
