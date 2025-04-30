using UnityEngine;
using UnityEngine.AI;
using Utilities;

namespace Enemies.EnemyStateMachine
{
    public class EnemyWanderState : EnemyBaseState {
        private readonly NavMeshAgent _agent;
        private Vector3 _startPoint;
        private readonly float _wanderRadius;
        private CountdownTimer _wanderWaitTimer;
        
        public EnemyWanderState(EnemyController enemyController, NavMeshAgent agent, float wanderRadius, float wanderTimer) : base(enemyController) {
            _agent = agent;
            _wanderRadius = wanderRadius;
            _wanderWaitTimer = new CountdownTimer(wanderTimer);
            _wanderWaitTimer.OnTimerStop += SetNewDestination;
        }

        public override void OnEnter() {
            Debug.Log("Wander");
            SetNewDestination();
        }

        public override void Update() {
            _wanderWaitTimer.Tick(Time.deltaTime);
            
            if (HasReachedDestination() && !_wanderWaitTimer.IsRunning) {
                _wanderWaitTimer.Start();
            }
        }

        private bool HasReachedDestination() {
            return !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance 
                                       && (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f);
        }

        private void SetNewDestination() {
            var randomDirection = Random.insideUnitSphere * _wanderRadius;
            randomDirection += _startPoint;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, _wanderRadius, 1);
            var finalPosition = hit.position;
                
            _agent.SetDestination(finalPosition);
        }
    }
}