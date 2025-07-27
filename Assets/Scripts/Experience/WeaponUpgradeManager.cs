using UnityEngine;
using UnityEngine.Events;

namespace Experience {
    public class WeaponUpgradeManager : MonoBehaviour {
        [Header("Bone Shard Costs per Tier (index = tier)")]
        [SerializeField] private int[] shardCostPerTier;

        public int CurrentTier { get; private set; }
        public event UnityAction<int> OnWeaponTierChanged;

        public bool TryUpgrade(int availableShards) {
            int nextTier = CurrentTier + 1;
            
            if (nextTier < shardCostPerTier.Length && availableShards >= shardCostPerTier[nextTier]) {
                CurrentTier = nextTier;
                OnWeaponTierChanged?.Invoke(CurrentTier);
                return true;
            }
            
            return false;
        }

        public int GetCostForNextTier() =>
            CurrentTier + 1 < shardCostPerTier.Length
                ? shardCostPerTier[CurrentTier + 1]
                : -1;
    }
}