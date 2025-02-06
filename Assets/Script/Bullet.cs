using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 3f;
    private Vector3 moveDirection;
    private Rigidbody2D rb;

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
        rb.velocity = new Vector3(moveDirection.x, moveDirection.y) * bulletSpeed; // ใช้ velocity แบบ 2D
    }
}
