using System;
using Enemies.Combat;
using Enemies.Detection;
using Enemies.EnemyStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class EnemyController : MonoBehaviour {
        [Header("References")]
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private PlayerDetector playerDetector;
        [SerializeField] private Animator animator;
        [SerializeField] private EnemyCombat enemyCombat;
        
        [Header("Radius")]
        [SerializeField] private float wanderRadius = 10f;
        
        [Header("Timer")] 
        [SerializeField] private float wanderTimerDuration = 1f;

        [Header("Forces")] 
        [SerializeField] private float wanderSpeed = 2f;
        [SerializeField] private float chaseSpeed = 3f;
        
        // State machine
        private StateMachine _stateMachine;

        private void Start() {
            _stateMachine = new StateMachine();

            var wanderState = new EnemyWanderState(this, animator, agent, wanderRadius, wanderTimerDuration, wanderSpeed);
            var chaseState = new EnemyChaseState(this, animator, agent, playerDetector.player, chaseSpeed);
            var attackState = new EnemyAttackState(this, animator, enemyCombat);
            
            At(wanderState, chaseState, () => playerDetector.CanDetectPlayer());
            At(chaseState, wanderState, () => !playerDetector.CanDetectPlayer());
            At(chaseState, attackState, () => playerDetector.CanAttackPlayer());
            At(attackState, chaseState, () => !playerDetector.CanAttackPlayer());
            
            _stateMachine.SetState(wanderState);
        }

        private void Update() {
            _stateMachine.Update();
        }

        private void FixedUpdate() {
            _stateMachine.FixedUpdate();
        }

        private void At(IState from, IState to, Func<bool> condition) {
            _stateMachine.AddTransition(from, to, condition);
        }
    }
}
