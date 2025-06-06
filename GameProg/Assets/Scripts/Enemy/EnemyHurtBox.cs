using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtBox : MonoBehaviour
{
    public Aspid aspid;
    public int hp = 3;

    public void Start()
    {
        if (aspid == null)
            aspid = GetComponent<Aspid>();
    }
    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0 && aspid != null)
        {
            aspid.Die();
        }
    }
}
