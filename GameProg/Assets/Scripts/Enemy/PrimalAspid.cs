using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimalAspid : MonoBehaviour
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
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bulletSpawnPoint = transform.Find("BulletSpawn");

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

                yield return new WaitForSeconds(0.5f);

                Vector2 targetPos = playerAimPoint != null
                    ? playerAimPoint.position
                    : (Vector2)player.position + new Vector2(0, 0.75f);

                Vector2 mainDirection = (targetPos - (Vector2)bulletSpawnPoint.position).normalized;

                // Shoot straight at player
                ShootBullet(mainDirection);

                // Shoot left angled
                ShootBullet(RotateVector(mainDirection, 20f));

                // Shoot right angled
                ShootBullet(RotateVector(mainDirection, -20f));

                yield return new WaitForSeconds(attackCooldown);
                isAttacking = false;
            }

            yield return null;
        }
    }

    void ShootBullet(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.GetComponent<AspidBullet>().Initialize(direction);
    }

    Vector2 RotateVector(Vector2 v, float angleDeg)
    {
        float rad = angleDeg * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);
        return new Vector2(
            v.x * cos - v.y * sin,
            v.x * sin + v.y * cos
        ).normalized;
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        rb.velocity = Vector2.zero;
        animator.SetTrigger("Die");
        SFXManager.Instance.PlayEnemyDeath();
        Destroy(gameObject, 0.5f);
        Death deathManager = FindObjectOfType<Death>();
        if (deathManager != null)
        {
            deathManager.playerScore += 1000;
        }
    }
}
