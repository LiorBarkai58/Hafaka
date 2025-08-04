using System;
using DG.Tweening;
using EventSystem;
using UnityEngine;
using Utilities;

namespace Enemies
{
    public class EnemyCombatManager : EntityCombatManager
    {
        [SerializeField] private Transform visuals;
        [SerializeField] private DamageArgsEventChannel damageArgsEventChannel;

        private CountdownTimer hurtTimer;

        public bool isHurt => hurtTimer != null && hurtTimer.IsRunning;
        [SerializeField] private float hurtDuration = 1;


        private void Start()
        {
            hurtTimer = new CountdownTimer(hurtDuration);
        }

        public override void TakeDamage(DamageDealtArgs damageDealtArgs)
        {
            base.TakeDamage(damageDealtArgs);
            damageArgsEventChannel.Invoke(damageDealtArgs);
            if(!hurtTimer.IsRunning) hurtTimer.Start();
        }

        
    }
}