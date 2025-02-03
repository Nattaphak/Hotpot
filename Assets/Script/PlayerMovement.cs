using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashPower = 10f;
    public float dashDuration = 0.2f; // ระยะเวลาการ Dash
    public float dashCooldown = 1f; // ระยะเวลาพักหลัง Dash

    [SerializeField] private Transform pointerDash;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Rigidbody2D rb;

    private Vector2 moveDir;
    private bool isMoving;
    private bool isDashing = false;
    private bool canDash = true; // เช็คว่า Dash ได้หรือไม่

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDashing) return; // ถ้ากำลัง Dash อยู่ ให้ข้ามการเคลื่อนที่ปกติไปก่อน

        moveDir = new Vector2(joystick.Horizontal, joystick.Vertical).normalized;

        if (moveDir.magnitude > 0)
        {
            rb.velocity = moveDir * moveSpeed;
            isMoving = true;
            Rotate();
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

    public void Dash()
    {
        if (!isDashing && canDash)
        {
            StartCoroutine(DashForward());
        }
    }

    IEnumerator DashForward()
    {
        isDashing = true;
        canDash = false;

        Vector2 dashDirection = (pointerDash.position - transform.position).normalized;
        rb.velocity = Vector2.zero; // หยุดความเร็วก่อน Dash
        rb.AddForce(dashDirection * dashPower, ForceMode2D.Impulse); // ใช้ AddForce เพื่อความลื่นไหล

        yield return new WaitForSeconds(dashDuration); // ให้ Dash ทำงานตามระยะเวลา

        rb.velocity = Vector2.zero; // หยุด Dash หลังจากเวลาที่กำหนด
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown); // พักก่อน Dash ได้อีกครั้ง
        canDash = true;
    }
}