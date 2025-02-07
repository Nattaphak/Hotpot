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
    [SerializeField] private Color damageColor = Color.red;
    [SerializeField] private Color iframeColor = Color.grey;
    [SerializeField] private BoxCollider2D playerHitBox;

    public SpriteRenderer sprite;
    Color defaultColor;

    void Start()
    {
        playerHitBox = GetComponent<BoxCollider2D>();
        currentLife = life;
        defaultColor = sprite.color;
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
            StartCoroutine(SwirchColor());

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

    IEnumerator SwirchColor()
    {
        playerHitBox.enabled = false;
        for(int i = 0; i < 4; i++)
        {
            sprite.color = damageColor;
            yield return new WaitForSeconds(0.3f);
            sprite.color = iframeColor;
            yield return new WaitForSeconds(0.2f);
        }
        sprite.color = defaultColor;
        playerHitBox.enabled = true;
    }
}
