using System;
using Enemies.Detection;
using Enemies.EnemyStateMachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Enemies
{
    public class EnemyController : MonoBehaviour {
        [Header("References")]
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private PlayerDetector playerDetector;
        
        [Header("Radius")]
        [SerializeField] private float wanderRadius = 10f;

        [Header("Timer")] 
        [SerializeField] private float wanderTimer = 1f;
        
        // State machine
        private StateMachine _stateMachine;

        private void Start() {
            _stateMachine = new StateMachine();

            var wanderState = new EnemyWanderState(this, agent, wanderRadius, wanderTimer);
            var chaseState = new EnemyChaseState(this, agent, playerDetector.player);
            
            At(wanderState, chaseState, () => playerDetector.CanDetectPlayer());
            At(chaseState, wanderState, () => !playerDetector.CanDetectPlayer());
            
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
