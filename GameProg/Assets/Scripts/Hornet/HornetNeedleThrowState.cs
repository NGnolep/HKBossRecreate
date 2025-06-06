using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornetNeedleThrowState : HornetState
{
    public HornetNeedleThrowState(HornetStateMachine sm) : base(sm) { }
    public override void Enter() => animator.Play("NeedleThrowAnticipate");

    public override void Update()
    {
        // Wait until done to return
        if (AnimationFinished("ThrowRecover"))
            stateMachine.SwitchState(stateMachine.idleState);
    }
}


