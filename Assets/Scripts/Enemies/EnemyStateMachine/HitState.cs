using Enemies.Combat;
using UnityEngine;

namespace Enemies.EnemyStateMachine
{
    public class HitState : EnemyBaseState
    {
        public HitState(EnemyController enemyController, Animator animator) : base(enemyController, animator) {
            StateIdentifier = EnemyStates.Attacking;
        }
    }
}