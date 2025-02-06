using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{

    [SerializeField] public int life = 3, currentLife;
    [SerializeField] private Rigidbody2D RB;
    [SerializeField] private TMP_Text healthText;
    
    void Start()
    {
        currentLife = life;
        UpdateHealth();
    }

    void Update()
    {
        UpdateHealth();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Bullet"))
        {
            currentLife--;

            if (currentLife <= 0)
            {
                GameManager.Instance.GameOver();
            }
        }
    }

    void UpdateHealth()
    {
        healthText.text = "X  " + currentLife.ToString();
    }



}
