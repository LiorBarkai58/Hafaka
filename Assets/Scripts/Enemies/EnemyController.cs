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
        [SerializeField] private EnemyCombatManager combatManager;
        
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
            var chaseState = new EnemyChaseState(this, animator, agent, playerDetector.player.Transform, chaseSpeed);
            var attackState = new EnemyAttackState(this, animator, enemyCombat);
            var hitState = new EnemyHitState(this, animator);
            At(wanderState, chaseState, () => playerDetector.CanDetectPlayer());
            At(chaseState, wanderState, () => !playerDetector.CanDetectPlayer());
            At(chaseState, attackState, () => playerDetector.CanAttackPlayer());
            At(attackState, chaseState, () => !playerDetector.CanAttackPlayer());
            
            
            Any(hitState, () => combatManager.isHurt && _stateMachine.Current != hitState);
            
            
            _stateMachine.SetState(wanderState);

            combatManager.OnDeath += () => gameObject.SetActive(false);
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
        void Any(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);
    }
}
