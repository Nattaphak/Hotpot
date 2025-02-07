using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashPower = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    [SerializeField] private Transform pointerDash;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D box;
    [SerializeField] private Player player;
    [SerializeField] private AudioSource DashSound;

    private Vector2 moveDir;
    private bool isMoving;
    public bool isDashing = false;
    private bool canDash = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (isDashing) return;

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

        player.immune = true;
        DashSound.Play();

        Vector2 dashDirection = (pointerDash.position - transform.position).normalized;
        rb.velocity = Vector2.zero;
        rb.AddForce(dashDirection * dashPower, ForceMode2D.Impulse);

        yield return new WaitForSeconds(dashDuration);

        player.immune = false;

        rb.velocity = Vector2.zero;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}