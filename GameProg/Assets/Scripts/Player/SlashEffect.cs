using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect : MonoBehaviour
{
        public bool isDownwardSlash = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<EnemyHurtBox>(out var enemy))
            {
                enemy.TakeDamage(1, transform.position);

                if (isDownwardSlash && PlayerMovement.Instance != null && !PlayerMovement.Instance.IsGrounded())
                {
                    PlayerMovement.Instance.Pogo();
                }
            }
            if (collision.TryGetComponent<EnemyPrimalHurtBox>(out var enemy2))
            {
                enemy2.TakeDamage(1, transform.position);

                if (isDownwardSlash && PlayerMovement.Instance != null && !PlayerMovement.Instance.IsGrounded())
                {
                    PlayerMovement.Instance.Pogo();
                }
            }
            if (collision.TryGetComponent<EnemyChaserHurtBox>(out var enemy3))
            {
                enemy3.TakeDamage(1, transform.position);

                if (isDownwardSlash && PlayerMovement.Instance != null && !PlayerMovement.Instance.IsGrounded())
                {
                    PlayerMovement.Instance.Pogo();
            }
        }
    }
}
