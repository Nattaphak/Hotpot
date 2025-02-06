using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 3f;
    private Vector3 moveDirection;
    private Rigidbody2D rb;

    public float timeTodestory;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction;
    }
    
    void Update()
    {
        timeTodestory = Time.deltaTime;
        rb.velocity = new Vector3(moveDirection.x, moveDirection.y) * bulletSpeed;
        if(timeTodestory >= 5f)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
