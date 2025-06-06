using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornetMidairDashState : HornetState
{
    public HornetMidairDashState(HornetStateMachine sm) : base(sm) { }
    public override void Enter()
    {
        animator.Play("MidairDash");
        stateMachine.Rb.velocity = new Vector2(hornet.transform.localScale.x * 8f, 0f);
    }

    public override void Update()
    {
        if (AnimationFinished("MidairDash"))
            stateMachine.SwitchState(stateMachine.landState);
    }

}
