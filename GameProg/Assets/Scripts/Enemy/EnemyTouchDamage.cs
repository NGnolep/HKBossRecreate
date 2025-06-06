using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTouchDamage : MonoBehaviour
{
    public int damageAmount = 1;
    public float damageCooldown = 2f;

    private float lastDamageTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.time - lastDamageTime < damageCooldown) return;

        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                lastDamageTime = Time.time;
            }
        }
    }
}
