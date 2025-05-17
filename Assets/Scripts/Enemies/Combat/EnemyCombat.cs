using UnityEngine;
using Utilities;

namespace Enemies.Combat {
    public class EnemyCombat : MonoBehaviour {
        public EnemyAttackData[] attacks;
        private int _attacksAmount;

        [Header("References")] [SerializeField]
        private Animator animator;
        
        [Header("Timer")]
        [SerializeField] private float attackCooldown = 1f;
        
        private CountdownTimer _attackTimer;

        private int _attackIndex;

        private void Awake() {
            _attacksAmount = attacks.Length;
        }

        private void Start() {
            _attackTimer = new CountdownTimer(attackCooldown);
        }
        
        public void Attack() {
            if (_attackTimer.IsRunning) return;
            
            SetRandomAttackIndex();
            _attackTimer.Start();

            if (attacks.Length > 0) {
                var attackHash = attacks[_attackIndex].attackHash;
                animator.CrossFade(attackHash, 0.1f);
            }
            // TODO: Implement take damage here (player)
        }

        private void SetRandomAttackIndex() {
            _attackIndex = Random.Range(0, _attacksAmount);
        }
    }
}