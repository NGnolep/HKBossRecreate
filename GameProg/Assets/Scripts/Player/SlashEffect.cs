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
                enemy.TakeDamage(1);

                if (isDownwardSlash && PlayerMovement.Instance != null && !PlayerMovement.Instance.IsGrounded())
                {
                    PlayerMovement.Instance.Pogo();
                }
            }
        }
}
