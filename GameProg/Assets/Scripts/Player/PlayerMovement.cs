using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float jumpForce = 16f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public Transform slashPointRight;
    public Transform slashPointLeft;
    public Transform slashPointUp;
    public Transform slashPointDown;
    public GameObject slashRight;
    public GameObject slashLeft;
    public GameObject slashUp;
    public GameObject slashDown;
    private bool isAttacking;
    public float attackCooldown = 0.5f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded;
    private float moveInput;
    private bool facingRight = true;
    private bool wasGrounded;
    private bool canJump = true;
    public float jumpCooldown = 2.5f;

    private bool isTouchingWall;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartCoroutine(Attack());
        }

        moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0 && !facingRight)
            Flip();
        else if (moveInput < 0 && facingRight)
            Flip();

        if (Input.GetButtonDown("Jump") && isGrounded && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            StartCoroutine(JumpCooldownRoutine());
            animator.SetTrigger("Jump");
        }

        if (!wasGrounded && isGrounded)
        {
            animator.SetTrigger("Land");
        }

        float animSpeed = isGrounded ? Mathf.Abs(moveInput) : 0f;
        animator.SetFloat("Speed", animSpeed);
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("VerticalVelocity", rb.velocity.y);

        wasGrounded = isGrounded;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !facingRight;
    }

    IEnumerator JumpCooldownRoutine()
    {
        canJump = false;
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }
    IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        GameObject slashToSpawn = slashRight;
        Transform slashPoint = slashPointRight;
        Quaternion rotation = Quaternion.identity;

        if (inputDir.y > 0.1f)
        {
            slashToSpawn = slashUp;
            slashPoint = slashPointUp;
            rotation = Quaternion.identity;
        }
        else if (inputDir.y < -0.1f)
        {
            slashToSpawn = slashDown;
            slashPoint = slashPointDown;
            rotation = Quaternion.identity;
        }
        else if (inputDir.x < 0)
        {
            slashToSpawn = slashLeft;
            slashPoint = slashPointLeft;
            rotation = Quaternion.identity;
        }
        else if (inputDir.x > 0)
        {
            slashToSpawn = slashRight;
            slashPoint = slashPointRight;
            rotation = Quaternion.identity;
        }
        else
        {
            // No input — fallback to facing direction
            if (facingRight)
            {
                slashToSpawn = slashRight;
                slashPoint = slashPointRight;
            }
            else
            {
                slashToSpawn = slashLeft;
                slashPoint = slashPointLeft;
            }
            rotation = Quaternion.identity;
        }

        Object Slash = Instantiate(slashToSpawn, slashPoint.position, rotation);
        Destroy(Slash, 0.06f);
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

}
