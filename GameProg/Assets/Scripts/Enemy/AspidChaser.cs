using System.Collections;
using UnityEngine;

public class AspidChaser : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float detectionRange = 20f;

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector3 originalScale;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalScale = transform.localScale;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    void Update()
    {
        if (isDead || player == null) return;

        // Flip to face the player
        Vector3 newScale = originalScale;
        newScale.x = (player.position.x < transform.position.x) ? Mathf.Abs(originalScale.x) : -Mathf.Abs(originalScale.x);
        transform.localScale = newScale;
    }

    void FixedUpdate()
    {
        if (isDead || player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= detectionRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        rb.velocity = Vector2.zero;
        animator.SetTrigger("Die");
        Destroy(gameObject, 0.5f); // Adjust if animation is longer
    }
}
