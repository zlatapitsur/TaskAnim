using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 5;
    public float runSpeed = 7;
    public float jumpForce = 300;

    public Rigidbody2D rb;
    public GroundCheck groundCheck;
    public PlayerHealth health;
    public Animator anim;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (health.isDead) return;

        float moveInput = Input.GetAxis("Horizontal");

        // Flip
        if (moveInput > 0)
            spriteRenderer.flipX = false;
        else if (moveInput < 0)
            spriteRenderer.flipX = true;

        // Speed and movement
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;
        rb.velocity = new Vector2(moveInput * currentSpeed, rb.velocity.y);

        // Run animation
        bool isRunning = moveInput != 0;
        anim.SetBool("IsRun", isRunning);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && groundCheck.isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce);

            // Увага: Встановлюємо IsJump на 1 кадр
            anim.SetBool("IsJump", true);
            anim.SetBool("IsFall", false);
        }
        else
        {
            // Якщо не натиснуто пробіл — скидаємо IsJump (щоб він не застряг)
            anim.SetBool("IsJump", false);
        }

        // Fall
        if (rb.velocity.y < -0.1f && !groundCheck.isGrounded)
        {
            anim.SetBool("IsFall", true);
        }

        // Grounded reset
        if (groundCheck.isGrounded)
        {
            anim.SetBool("IsFall", false);
        }

        Debug.Log($"HP={health.GetHealth()} grounded={groundCheck.isGrounded} yVel={rb.velocity.y}");
    }
}