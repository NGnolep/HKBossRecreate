using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HornetState
{
    protected HornetStateMachine stateMachine;
    protected Animator animator;
    protected GameObject hornet;

    public HornetState(HornetStateMachine sm)
    {
        stateMachine = sm;
        hornet = sm.gameObject;
        animator = sm.Animator;
    }
    protected bool AnimationFinished(string animName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animName) &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
    }
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
}


