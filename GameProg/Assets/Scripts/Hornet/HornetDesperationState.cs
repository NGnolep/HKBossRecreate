using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HornetDesperationState : HornetState
{
    public HornetDesperationState(HornetStateMachine sm) : base(sm) { }
    float desperationTimer = 10f;

    public override void Enter()
    {
        animator.Play("DesperationStart");
        desperationTimer = 10f;
    }

    public override void Update()
    {
        desperationTimer -= Time.deltaTime;
        if (desperationTimer <= 0)
            stateMachine.SwitchState(stateMachine.idleState);
    }
}
