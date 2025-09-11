using Enemies.Combat;
using UnityEngine;

namespace Enemies.EnemyStateMachine
{
    public class EnemyHitState : EnemyBaseState
    {

        public Transform hitByTarget;
        private static readonly int HurtHash = UnityEngine.Animator.StringToHash("Hurt");
        public EnemyHitState(EnemyController enemyController, Animator animator) : base(enemyController, animator) {
            StateIdentifier = EnemyStates.Hurt;
        }

        public override void OnEnter()
        {
            Debug.Log("Entered");
            Animator.SetTrigger(HurtHash);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (hitByTarget)
            {
                EnemyController.transform.position += (EnemyController.transform.position - hitByTarget.position).normalized * (Time.fixedDeltaTime * 3);
                return;
            }
            EnemyController.transform.position -= EnemyController.transform.forward * (Time.fixedDeltaTime * 3);
            
        }

        public override void OnExit()
        {
            base.OnExit();
            hitByTarget = null;
        }
    }
}