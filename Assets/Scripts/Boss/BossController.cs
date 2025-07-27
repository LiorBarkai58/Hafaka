using System;
using UnityEngine;
using UnityEngine.Serialization;

public class BossController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;

    [SerializeField] private PlayerTransform playerTransform;

    private StateMachine stateMachine;

    [Header("State Management")]
    private BossState _defaultState;
    private TriggerTransition _attackTrigger;

    [FormerlySerializedAs("AttackDistance")]
    [Header("Boss Data")] 
    [SerializeField] private float attackDistance;

    private void Awake()
    {
        SetupStateMachine(); // Initialize the state machine
    }

    private void SetupStateMachine()
    {
        stateMachine = new StateMachine();

        // Initialize states (placeholders for now, replace them with actual subclasses of BossState)
        IdleState idleState = new IdleState(this, animator, BossStates.Idle);
        ChaseState chaseState = new ChaseState(this, animator, BossStates.Chase);
        BossAttackState attackState = new BossAttackState(this, animator, BossStates.Attacking);
        

        // Assign default state
        _defaultState = idleState;

        // Create triggers

        // Define transitions between states
        At(idleState, chaseState, new Func<bool>(DetectsPlayer));
        At(chaseState, idleState, new Func<bool>(LostPlayer));
        At(chaseState, attackState, new Func<bool>(WithinAttackRange));

        // Set the initial state
        stateMachine.SetState(idleState);
    }

    private void Update()
    {
        stateMachine.Update();
        // Add any additional per-frame updates required for the boss
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    private void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);

    // Placeholder conditions (replace with actual logic)
    private bool DetectsPlayer()  {
        return playerTransform != null;
    } // Replace with your logic to detect the player
    private bool WithinAttackRange() => Vector3.Distance(transform.position, playerTransform.Transform.position) < attackDistance; // Replace with logic to check if the player is within attack range
    private bool LostPlayer() => false; // Replace with logic when the player is "lost"
    private bool PlayerOutOfRange() => false; // Replace with logic when the player moves out of range
}