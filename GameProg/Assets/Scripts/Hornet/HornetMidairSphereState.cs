using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornetMidairSphereState : HornetState
{
    public HornetMidairSphereState(HornetStateMachine sm) : base(sm) { }
    public override void Enter()
    {
        animator.Play("SphereAnticipate");
    }

    public override void Update()
    {
        if (AnimationFinished("SphereRecover"))
            stateMachine.SwitchState(stateMachine.landState);
    }
}
