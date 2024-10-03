using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;

public class PlayerController : MonoBehaviour, IController
{
    [Header("Attribute")]
    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    [SerializeField] Transform groundCheck;
    [SerializeField] Transform faceCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask enemyLayer;

    [Header("References")]
    [SerializeField] private Animator animator;

    //hidden components
    private Rigidbody2D rb;

    private float horizontalInput;
    private bool isFacingRight;

    private Vector2 startPos;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        isFacingRight = true;
        animator.SetBool("isMoving", false);

        this.RegisterEvent<RestartEvent>(Restart).UnRegisterWhenGameObjectDestroyed(gameObject);
    }

    void Restart(RestartEvent e)
    {
        transform.position = startPos;
        AudioManager.Instance.PlaySFXSound("die");
    }

    private void Update()
    {
        if (!IsFalling() || IsGrounded())
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            animator.SetBool("isMoving", true);
        }
        else
        {
            horizontalInput = 0;
        }

        if(horizontalInput == 0) animator.SetBool("isMoving", false);

        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            AudioManager.Instance.PlayJumpSound();
        }

        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }

    private void Flip()
    {
        if(isFacingRight && horizontalInput < 0 || !isFacingRight && horizontalInput > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    private bool IsFalling()
    {
        return Physics2D.OverlapCircle(faceCheck.position, 0.3f, groundLayer);
    }

    private bool IsOnTopOfEnemy()
    {
        Collider2D collision = Physics2D.OverlapCircle(groundCheck.position, 0.3f, enemyLayer);
        if(collision != null)
        {
            collision.GetComponent<EnemyController>().GetKilled();
            AudioManager.Instance.PlaySFXSound("kill");
            return true;
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra xem va chạm với kẻ địch
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!IsGrounded())
            {
                if (IsOnTopOfEnemy())
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    return;
                }
            }
            this.SendCommand<RestartCommand>();
        }
    }

    IArchitecture IBelongToArchitecture.GetArchitecture()
    {
        return SadnessKnightAdventure.Interface;
    }
}
