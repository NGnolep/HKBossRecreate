using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornetJumpState : HornetState
{
    public HornetJumpState(HornetStateMachine sm) : base(sm) { }
    private bool hasJumped = false;
    private bool hasDecidedNextState = false;
    public override void Enter()
    {
        animator.Play("JumpAnticipate");
        hasJumped = false;
        hasDecidedNextState = false;
    }
    public override void Update()
    {
        if (!hasJumped)
        {
            stateMachine.Rb.velocity = new Vector2(0, 8f);
            stateMachine.Animator.Play("JumpMidair");
            hasJumped = true;
        }

        if (stateMachine.Rb.velocity.y < 0 && !hasDecidedNextState)
        {
            hasDecidedNextState = true;

            if (stateMachine.currentPhase == HornetStateMachine.HornetPhase.Phase2
                || stateMachine.currentPhase == HornetStateMachine.HornetPhase.Desperation)
            {
                float rand = Random.value;

                if (rand > 0.7f)
                {
                    // 30% chance to land immediately
                    stateMachine.SwitchState(stateMachine.landState);
                }
                else if (rand > 0.35f)
                {
                    // 35% chance to midair sphere attack
                    stateMachine.SwitchState(stateMachine.midairSphereState);
                }
                else
                {
                    // 35% chance to midair dash
                    stateMachine.SwitchState(stateMachine.midairDashState);
                }
            }
            else
            {
                // Phase 1 - either land or dash (e.g. 50/50)
                if (Random.value > 0.5f)
                {
                    stateMachine.SwitchState(stateMachine.landState);
                }
                else
                {
                    stateMachine.SwitchState(stateMachine.midairDashState);
                }
            }
        }
    }

}
