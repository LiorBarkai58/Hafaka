using DG.Tweening;
using UnityEngine;

namespace Enemies
{
    public class EnemyCombatManager : EntityCombatManager
    {
        [SerializeField] private Transform visuals;
        public override void TakeDamage(DamageDealtArgs damageDealtArgs)
        {
            base.TakeDamage(damageDealtArgs);
            visuals.DOShakePosition(0.5f, 0.2f);
        }
    }
}