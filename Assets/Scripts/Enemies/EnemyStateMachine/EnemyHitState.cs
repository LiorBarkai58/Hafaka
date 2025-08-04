using Enemies.Combat;
using UnityEngine;

namespace Enemies.EnemyStateMachine
{
    public class EnemyHitState : EnemyBaseState
    {

        private static readonly int HurtHash = UnityEngine.Animator.StringToHash("Hurt");
        public EnemyHitState(EnemyController enemyController, Animator animator) : base(enemyController, animator) {
            StateIdentifier = EnemyStates.Hurt;
        }

        public override void OnEnter()
        {
            Debug.Log("Entered");
            Animator.SetTrigger(HurtHash);
        }
        
        
    }
}