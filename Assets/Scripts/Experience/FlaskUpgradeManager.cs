using UnityEngine;
using UnityEngine.Events;

namespace Experience {
    public class FlaskUpgradeManager : MonoBehaviour {
        [Header("Seed Cost Array")]
        [Tooltip("Flask Uses per Seed Count (index = seeds collected)")]
        [SerializeField] private int[] seedCostPerFlaskLevel;
        
        public int CurLevel { get; private set; }
        public int CurrentFlaskUses { get; private set; }
        
        public event UnityAction<int> OnFlaskUpgraded;

        public bool TryUpgradeFlask(int seedAmount) {
            var nextLevel = CurLevel + 1;

            if (nextLevel < seedCostPerFlaskLevel.Length && seedAmount >= seedCostPerFlaskLevel[nextLevel]) {
                CurLevel = nextLevel;
                OnFlaskUpgraded?.Invoke(CurrentFlaskUses);
                return true;
            }

            return false;
        }
        
        public int GetCostForNextTier() {
            var nextTier = CurLevel + 1;
            
            if (nextTier < seedCostPerFlaskLevel.Length)
                return seedCostPerFlaskLevel[nextTier];
            
            return -1; // maxed
        }
    }
}