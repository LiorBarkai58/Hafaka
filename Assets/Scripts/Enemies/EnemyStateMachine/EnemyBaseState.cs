using UnityEngine;

namespace Enemies.EnemyStateMachine
{
    public class EnemyBaseState : IState
    {
        protected EnemyController EnemyController;
        protected Animator Animator;

        protected EnemyStates StateIdentifier = EnemyStates.Locomotion;

        private static readonly int EnemyState = Animator.StringToHash("EnemyState");
        protected static readonly int LocomotionHash = Animator.StringToHash("Locomotion");

        protected EnemyBaseState(EnemyController enemyController, Animator animator) {
            EnemyController = enemyController;
            Animator = animator;
        }
        
        public virtual void OnEnter(){
            Animator.SetInteger(EnemyState, (int)StateIdentifier);
        }

        public virtual void Update(){
            
        }

        public virtual void FixedUpdate(){
            
        }

        public virtual void OnExit(){
            
        }
    }
    
    public enum EnemyStates{
        Locomotion,
        Attacking,
        Hurt
    }
}