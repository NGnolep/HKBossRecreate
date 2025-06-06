using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornetStateMachine : MonoBehaviour
{
    [HideInInspector] public Animator Animator;
    [HideInInspector] public Rigidbody2D Rb;
    [HideInInspector] public Transform Player;

    public HornetState currentState;

    public HornetIdleState idleState;
    public HornetMovementState movementState;
    public HornetJumpState jumpState;
    public HornetMidairDashState midairDashState;
    public HornetLandState landState;
    public HornetEvadeState evadeState;
    public HornetGroundDashState groundDashState;
    public HornetGroundSphereState groundSphereState;
    public HornetMidairSphereState midairSphereState;
    public HornetNeedleThrowState needleThrowState;
    public HornetDesperationState desperationState;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    
    public enum HornetPhase { Phase1, Phase2, Desperation }
    public HornetPhase currentPhase = HornetPhase.Phase1;

    void Awake()
    {
        Animator = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        idleState = new HornetIdleState(this);
        movementState = new HornetMovementState(this);
        jumpState = new HornetJumpState(this);
        midairDashState = new HornetMidairDashState(this);
        landState = new HornetLandState(this);
        evadeState = new HornetEvadeState(this);
        groundDashState = new HornetGroundDashState(this);
        groundSphereState = new HornetGroundSphereState(this);
        midairSphereState = new HornetMidairSphereState(this);
        needleThrowState = new HornetNeedleThrowState(this);
        desperationState = new HornetDesperationState(this);
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckDistance, groundLayer);
        return hit.collider != null;
    }
    void Start()
    {
        currentState = idleState;
        currentState.Enter();
    }

    void Update()
    {
        currentState?.Update();

        // Example phase switching trigger (call this from damage logic)
        // if (condition for phase2) currentPhase = HornetPhase.Phase2;
        // if (condition for desperation) currentPhase = HornetPhase.Desperation;
    }

    void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }

    public void SwitchState(HornetState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

}
