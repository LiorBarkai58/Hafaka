using DG.Tweening;
using UnityEngine;

namespace Enemies
{
    public class EnemyCombatManager : EntityCombatManager
    {
        [SerializeField] private Transform visuals;
        public override void TakeDamage(DamageDealtArgs damageDealtArgs)
        {
            visuals.DOShakePosition(0.5f, 0.5f);
            base.TakeDamage(damageDealtArgs);
        }

        
    }
}