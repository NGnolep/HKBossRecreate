using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornetEvadeState : HornetState
{
    public HornetEvadeState(HornetStateMachine sm) : base(sm) { }
    public override void Enter()
    {
        animator.Play("Evade");
        stateMachine.Rb.velocity = new Vector2(-hornet.transform.localScale.x * 6f, 0);
    }

    public override void Update()
    {
        if (AnimationFinished("Evade"))
            stateMachine.SwitchState(stateMachine.idleState);
    }
}
