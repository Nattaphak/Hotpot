using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    [SerializeField] private Joystick joystick;
    [SerializeField] private Rigidbody2D rb;

    private Vector2 moveDir;
    private bool isMoving;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveDir = new Vector2(joystick.Horizontal, joystick.Vertical).normalized;

        if (moveDir.magnitude > 0)
        {
            rb.velocity = moveDir * moveSpeed;
            isMoving = true;
            Rotate(); // เรียกใช้ฟังก์ชัน Rotate เมื่อมีการเคลื่อนที่
        }
        else
        {
            rb.velocity = Vector2.zero;
            isMoving = false;
        }
    }

    void Rotate()
    {
        if (isMoving)
        {
            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}