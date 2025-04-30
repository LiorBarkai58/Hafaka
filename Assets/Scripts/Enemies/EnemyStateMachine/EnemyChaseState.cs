using UnityEngine;
using UnityEngine.AI;

namespace Enemies.EnemyStateMachine
{
    public class EnemyChaseState : EnemyBaseState {
        private NavMeshAgent _agent;
        private Transform _player;
        
        public EnemyChaseState(EnemyController enemyController, NavMeshAgent agent, Transform player) : base(enemyController) {
            _agent = agent;
            _player = player;
        }

        public override void OnEnter() {
            Debug.Log("Chase");
        }

        public override void Update() {
            _agent.SetDestination(_player.position);
        }
    }
}