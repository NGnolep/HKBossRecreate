using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornetGroundDashState : HornetState
{
    public HornetGroundDashState(HornetStateMachine sm) : base(sm) { }
    private bool hasDashed = false;

    public override void Enter()
    {
        animator.Play("GroundDashAnticipate");
        hasDashed = false;
    }

    public override void Update()
    {
        if (!hasDashed && AnimationFinished("GroundDashAnticipate"))
        {
            stateMachine.Rb.velocity = new Vector2(hornet.transform.localScale.x * 9f, 0f);
            hasDashed = true;
        }

        if (hasDashed && AnimationFinished("DashRecover"))
            stateMachine.SwitchState(stateMachine.idleState);
    }

}
