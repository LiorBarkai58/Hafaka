using System;
using DG.Tweening;
using EventSystem;
using UnityEngine;
using Utilities;

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
        

        private CountdownTimer hurtTimer;

        public bool isHurt => hurtTimer != null && hurtTimer.IsRunning;
        [SerializeField] private float hurtDuration = 1;


        private void Start()
        {
            hurtTimer = new CountdownTimer(hurtDuration);
        }

        public override void TakeDamage(DamageDealtArgs damageDealtArgs)
        {
            visuals.DOShakePosition(0.5f, 0.5f);
            base.TakeDamage(damageDealtArgs);
            damageArgsEventChannel.Invoke(damageDealtArgs);
            if(!hurtTimer.IsRunning) hurtTimer.Start();
        }

        protected override void Death() {
            onEnemyDeathEventChannel.Invoke(xpReward);
            base.Death();
        }
    }
}