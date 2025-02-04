using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] public int life = 3, currentLife;
    [SerializeField] private Rigidbody2D RB;
    // Start is called before the first frame update
    void Start()
    {
        currentLife = life;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Obstacle"))
        {
            currentLife--;

            if (currentLife <= 0)
            {
                GameManager.Instance.GameOver();
            }
        }
    }

}
