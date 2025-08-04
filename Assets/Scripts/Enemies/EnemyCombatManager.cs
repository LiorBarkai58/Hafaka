using DG.Tweening;
using EventSystem;
using UnityEngine;

namespace Enemies
{
    public class EnemyCombatManager : EntityCombatManager
    {
        [Header("Events")]
        [SerializeField] private DamageArgsEventChannel damageArgsEventChannel;
        [SerializeField] private IntEventChannel onEnemyDeathEventChannel;
        
        [Header("References")]
        [SerializeField] private Transform visuals;
        
        [Header("XP Reward")]
        [SerializeField] private int xpReward = 20;
        
        public override void TakeDamage(DamageDealtArgs damageDealtArgs)
        {
            visuals.DOShakePosition(0.5f, 0.5f);
            base.TakeDamage(damageDealtArgs);
            damageArgsEventChannel.Invoke(damageDealtArgs);
        }

        protected override void Death() {
            onEnemyDeathEventChannel.Invoke(xpReward);
            base.Death();
        }
    }
}