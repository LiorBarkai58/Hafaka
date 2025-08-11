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
            visuals.DOShakePosition(0.5f, 0.5f);
            base.TakeDamage(damageDealtArgs);
            damageArgsEventChannel.Invoke(damageDealtArgs);
        }

        
    }
}