using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Enemies.Combat {
    public class EnemyCombat : MonoBehaviour {
        [Header("References")] 
        [SerializeField] private Animator animator;
        
        [Header("Timer")]
        [SerializeField] private float attackCooldown = 1f;

        [Header("Attacks")]
        [SerializeField] private int attacksAmount;
        
        private CountdownTimer _attackTimer;

        private int _attackIndex;
        
        [Header("Hitboxes")]
        [SerializeField] private List<Collider> hitboxes;
        
        private static readonly int AttackHash = Animator.StringToHash("AttackIndex");

        private void Start() {
            _attackTimer = new CountdownTimer(attackCooldown);
        }

        private void Update()
        {
            _attackTimer.Tick(Time.deltaTime);
        }

        public void Attack() {
            if (_attackTimer.IsRunning) return;
            
            Debug.Log("Attacking");
            
            SetRandomAttackIndex();
            animator.SetInteger(AttackHash, _attackIndex);
            _attackTimer.Start();
            // TODO: Implement take damage here (player)
        }

        private void SetRandomAttackIndex() {
            _attackIndex = Random.Range(0, attacksAmount);
        }
        
        public void EnableColliders()
        {
            foreach (Collider collider in hitboxes)
            {
                collider.enabled = true;
            }
        }
        
        public void DisableColliders()
        {
            foreach (Collider collider in hitboxes)
            {
                collider.enabled = false;
            }
        }
    }
}