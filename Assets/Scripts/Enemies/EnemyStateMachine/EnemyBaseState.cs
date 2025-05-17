using UnityEngine;

namespace Enemies.EnemyStateMachine
{
    public class EnemyBaseState : IState
    {
        protected EnemyController EnemyController;
        protected Animator Animator;

        protected static readonly int LocomotionHash = Animator.StringToHash("Locomotion");
        protected static readonly int AttackHash = Animator.StringToHash("Attack");

        protected const float CrossFadeDuration = 0.1f;

        protected EnemyBaseState(EnemyController enemyController, Animator animator) {
            EnemyController = enemyController;
            Animator = animator;
        }
        
        public virtual void OnEnter(){
            
        }

        public virtual void Update(){
            
        }

        public virtual void FixedUpdate(){
            
        }

        public virtual void OnExit(){
            
        }
    }
}