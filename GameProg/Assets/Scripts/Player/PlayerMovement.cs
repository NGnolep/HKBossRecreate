using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }
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
    [SerializeField] private bool isGrounded;
    private float moveInput;
    private bool facingRight = true;
    [SerializeField] private bool wasGrounded;
    private bool canJump = true;
    public float jumpCooldown = 2.5f;
    public bool canMove = true;
    public float pogoForce = 8f;
    private bool isTouchingWall;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!canMove)
        {
            moveInput = 0f;
            animator.ResetTrigger("Jump");
            animator.ResetTrigger("Attack");
            animator.SetFloat("Speed", 0f);
            return;
        }

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
            canJump = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
            Debug.Log("Jumping");
            StartCoroutine(JumpCooldownRoutine());
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (!wasGrounded && isGrounded)
        {
            animator.SetTrigger("Land");
            
        }

        float animSpeed = isGrounded ? Mathf.Abs(moveInput) : 0f;
        wasGrounded = isGrounded;
        animator.SetFloat("Speed", animSpeed);
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("VerticalVelocity", rb.velocity.y);

    }

    void FixedUpdate()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;  // stop all movement
        }
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
        animator.ResetTrigger("Jump");
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
        Destroy(Slash, 0.04f);
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
    public void Pogo()
    {
        rb.velocity = new Vector2(rb.velocity.x, pogoForce);
    }
    public bool IsGrounded()
    {
        return isGrounded;
    }
}
