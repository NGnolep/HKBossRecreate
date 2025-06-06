using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornetMovementState : HornetState
{
    public HornetMovementState(HornetStateMachine sm) : base(sm) { }
    public override void Update()
    {
        Vector2 dir = (stateMachine.Player.position - hornet.transform.position).normalized;
        stateMachine.Rb.velocity = new Vector2(dir.x * 3f, stateMachine.Rb.velocity.y);

        if (Random.value < 0.01f)
        {
            switch (Random.Range(0, 4))
            {
                case 0: stateMachine.SwitchState(stateMachine.jumpState); break;
                case 1: stateMachine.SwitchState(stateMachine.groundDashState); break;
                case 2: stateMachine.SwitchState(stateMachine.midairDashState); break;
                case 3: stateMachine.SwitchState(stateMachine.evadeState); break;
            }
        }
    }
}
