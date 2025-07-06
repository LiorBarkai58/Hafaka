using Enemies.Combat;
using UnityEngine;

namespace Enemies.EnemyStateMachine
{
    public class EnemyAttackState : EnemyBaseState {
        private EnemyCombat _enemyCombat;
        private int _attackIndex;
        
        public EnemyAttackState(EnemyController enemyController, Animator animator, EnemyCombat enemyCombat) : base(enemyController, animator) {
            _enemyCombat = enemyCombat;
            StateIdentifier = EnemyStates.Attacking;
        }

        public override void Update() {
            _enemyCombat.Attack();
        }
    }
}