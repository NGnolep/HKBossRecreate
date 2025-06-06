using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrimalHurtBox : MonoBehaviour
{
    public PrimalAspid aspid;
    public int hp = 3;

    public void Start()
    {
        if (aspid == null)
            aspid = GetComponent<PrimalAspid>();
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
