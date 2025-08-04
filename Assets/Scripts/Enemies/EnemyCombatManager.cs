using DG.Tweening;
using EventSystem;
using UnityEngine;

namespace Enemies
{
    public class EnemyCombatManager : EntityCombatManager
    {
        [SerializeField] private Transform visuals;
        [SerializeField] private DamageArgsEventChannel damageArgsEventChannel;
        public override void TakeDamage(DamageDealtArgs damageDealtArgs)
        {
            base.TakeDamage(damageDealtArgs);
            damageArgsEventChannel.Invoke(damageDealtArgs);
        }

        
    }
}