using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aspid : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float detectionRange = 30f;
    public float attackRange = 12f;
    public float attackCooldown = 2f;

    public GameObject bulletPrefab;
    private Transform player;
    private Transform bulletSpawnPoint;
    private Transform playerAimPoint;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector3 originalScale;

    private bool isAttacking = false;
    private bool isDead = false;
    private bool isChasing = false;

    void Start()
    {
        // Dynamically get components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bulletSpawnPoint = transform.Find("BulletSpawn"); // must be a child object named exactly this

        originalScale = transform.localScale;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerAimPoint = player.Find("AimPoint");

            if (playerAimPoint == null)
                Debug.LogWarning("AimPoint not found on Player. Bullets will aim at Player position.");
        }
        else
        {
            Debug.LogError("Player not found!");
        }

        StartCoroutine(AttackLoop());
    }

    void Update()
    {
        if (isDead || isAttacking || player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        isChasing = distance <= detectionRange && distance > attackRange;
        animator.SetBool("isChasing", isChasing);

        // Flip to face the player
        Vector3 newScale = originalScale;
        newScale.x = (player.position.x < transform.position.x) ? Mathf.Abs(originalScale.x) : -Mathf.Abs(originalScale.x);
        transform.localScale = newScale;
    }

    void FixedUpdate()
    {
        if (isDead || isAttacking || player == null) return;

        if (isChasing)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    IEnumerator AttackLoop()
    {
        while (!isDead)
        {
            if (player != null && Vector2.Distance(transform.position, player.position) <= attackRange)
            {
                rb.velocity = Vector2.zero;
                isAttacking = true;
                animator.SetTrigger("Attack");

                yield return new WaitForSeconds(0.5f); // Wait for animation

                Vector2 targetPos;
                if (playerAimPoint != null)
                    targetPos = playerAimPoint.position;
                else
                    targetPos = player.position + new Vector3(0f, 0.75f); // manually offset upwards if AimPoint not found

                Vector2 direction = (targetPos - (Vector2)bulletSpawnPoint.position).normalized;

                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
                bullet.GetComponent<AspidBullet>().Initialize(direction);

                yield return new WaitForSeconds(attackCooldown);
                isAttacking = false;
            }

            yield return null;
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        rb.velocity = Vector2.zero;
        animator.SetTrigger("Die");
        Destroy(gameObject, 0.5f);
    }
}
