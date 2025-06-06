using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornetIdleState : HornetState
{
    public HornetIdleState(HornetStateMachine sm) : base(sm) { }

    public override void Update()
    {
        if (stateMachine.currentPhase == HornetStateMachine.HornetPhase.Phase1)
            stateMachine.SwitchState(stateMachine.movementState);
        else if (stateMachine.currentPhase == HornetStateMachine.HornetPhase.Phase2)
            stateMachine.SwitchState(stateMachine.needleThrowState);
        else
            stateMachine.SwitchState(stateMachine.desperationState);
    }
}
