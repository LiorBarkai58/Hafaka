using UnityEngine;

namespace Enemies.Combat {
    [CreateAssetMenu(menuName = "Enemy/Attack Data")]
    public class EnemyAttackData : ScriptableObject {
        public AnimationClip clip;
        public string attackStateName;
        public int attackHash;
        public float damage;
        
        private void OnValidate()
        {
            if (clip != null)
                attackHash = Animator.StringToHash(attackStateName);
        }
    }
}