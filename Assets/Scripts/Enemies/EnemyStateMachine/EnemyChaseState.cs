using UnityEngine;
using UnityEngine.AI;

namespace Enemies.EnemyStateMachine
{
    public class EnemyChaseState : EnemyBaseState {
        private NavMeshAgent _agent;
        private Transform _player;
        private float _chaseSpeed;
        
        public EnemyChaseState(EnemyController enemyController, Animator animator, NavMeshAgent agent, Transform player, float chaseSpeed) : base(enemyController, animator) {
            _agent = agent;
            _player = player;
            _chaseSpeed = chaseSpeed;
        }

        public override void OnEnter() {
            _agent.speed = _chaseSpeed;
            Animator.CrossFade(LocomotionHash, CrossFadeDuration);
        }

        public override void Update() {
            _agent.SetDestination(_player.position);
        }
    }
}