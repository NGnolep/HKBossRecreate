using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaserHurtBox : MonoBehaviour
{
    public AspidChaser aspid;
    public int hp = 2;
    public float knockbackForce = 10f;
    public float knockbackDuration = 0.05f;
    public void Start()
    {
        if (aspid == null)
            aspid = GetComponent<AspidChaser>();
    }
    public void TakeDamage(int amount, Vector2 attackerPosition)
    {
        hp -= amount;
        Rigidbody2D rb = aspid.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 knockbackDir = ((Vector2)transform.position - attackerPosition).normalized;
            rb.velocity = Vector2.zero; // reset previous velocity
            rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
            StartCoroutine(StopKnockbackAfterDelay(rb));
        }
        if (hp <= 0 && aspid != null)
        {
            aspid.Die();
        }
    }

    private IEnumerator StopKnockbackAfterDelay(Rigidbody2D rb)
    {
        yield return new WaitForSeconds(knockbackDuration);
        if (rb != null) rb.velocity = Vector2.zero;
    }
}
