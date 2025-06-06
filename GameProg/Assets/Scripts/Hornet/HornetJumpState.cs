using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornetJumpState : HornetState
{
    public HornetJumpState(HornetStateMachine sm) : base(sm) { }
    private bool hasJumped = false;
    public override void Enter()
    {
        animator.Play("JumpAnticipate");
        hasJumped = false;
    }

    public override void Update()
    {
        if (!hasJumped)
        {
            stateMachine.Rb.velocity = new Vector2(0, 8f);
            animator.Play("JumpMidair");
            hasJumped = true;
        }

        if (stateMachine.Rb.velocity.y < 0)
            stateMachine.SwitchState(stateMachine.landState);
    }

}
