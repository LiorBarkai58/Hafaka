using UnityEngine;
using UnityEngine.Events;

namespace Experience {
    public class WeaponUpgradeManager : MonoBehaviour {
        [Header("Shard Cost Array")]
        [Tooltip("Bone Shard Costs per Tier (index 0 = tier0 -> tier1)")]
        [SerializeField] private int[] shardCostPerTier;

        public int CurrentTier { get; private set; }
        
        public event UnityAction<int> OnWeaponTierChanged;

        public bool TryUpgradeWeapon(int availableShards) {
            var nextTier = CurrentTier + 1;
            
            // If there is next tier for the weapon and the player has enough bone shard --> Upgrade the weapon
            // If not, return false
            if (nextTier < shardCostPerTier.Length && availableShards >= shardCostPerTier[nextTier]) {
                CurrentTier = nextTier;
                OnWeaponTierChanged?.Invoke(CurrentTier);
                return true;
            }
            
            return false;
        }

        public int GetCostForNextTier() {
            var nextTier = CurrentTier + 1;
            
            if (nextTier < shardCostPerTier.Length)
                return shardCostPerTier[nextTier];
            
            return -1; // maxed
        }
    }
}