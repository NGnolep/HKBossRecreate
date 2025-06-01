using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    //public int damage = 1;
    //public float lifetime = 0.05f;

    void Start()
    {
        //Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //if (col.CompareTag("Enemy"))
        {
            //col.GetComponent<EnemyHealth>()?.TakeDamage(damage);
        }
    }
}
