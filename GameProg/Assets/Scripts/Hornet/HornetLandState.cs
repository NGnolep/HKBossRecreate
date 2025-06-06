using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornetLandState : HornetState
{
    public HornetLandState(HornetStateMachine sm) : base(sm) { }
    public override void Enter()
    {
        animator.Play("MidairToLand");
    }

    public override void Update()
    {
        if (stateMachine.IsGrounded())
            stateMachine.SwitchState(stateMachine.idleState);
    }
}

