using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 5;
    private int currentHP;
    public float iFrameDuration = 1f;
    private bool isInvincible = false;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        currentHP = maxHP;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(int amount)
    {
        if (isInvincible) return;

        currentHP -= amount;
        Debug.Log("Player took damage. HP: " + currentHP);


        if (currentHP <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityFrames());
        }
    }

    IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        float blinkInterval = 0.1f;
        float elapsed = 0f;

        while (elapsed < iFrameDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // toggle visibility for blink
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        spriteRenderer.enabled = true; // ensure visible at the end
        isInvincible = false;
    }

    void Die()
    {
        Debug.Log("Player Died");
        animator.SetTrigger("Die");
        StartCoroutine(WaitAndDestroy(0.5f));
        // Add death animation or reload scene
    }
    IEnumerator WaitAndDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

}
