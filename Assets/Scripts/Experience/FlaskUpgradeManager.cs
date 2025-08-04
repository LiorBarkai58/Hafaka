using EventSystem;
using Interactables;
using UnityEngine;
using UnityEngine.Events;

namespace Experience {
    public class FlaskUpgradeManager : MonoBehaviour {
        [Header("Flask Base & Growth Formula")]
        [SerializeField] private int baseFlaskUses = 3;
        [SerializeField] private int flaskIncrementPerSeed = 1;

        [Header("Events")] 
        [SerializeField] private UpgradePickupEventListener upgradePickupEventListener;
        
        public int SeedCount { get; private set; }
        public int CurrentFlaskUses { get; private set; }

        public event UnityAction<int> OnFlaskUpgraded;

        private void OnEnable() {
            upgradePickupEventListener.OnEvent += UpgradeFlask;
        }

        private void OnDisable() {
            upgradePickupEventListener.OnEvent -= UpgradeFlask;
        }

        private void Start() {
            // Initialize with zero seeds
            ApplySeedEffects();
        }

        private void UpgradeFlask(UpgradeType upgradeType) {
            SeedCount++;
            ApplySeedEffects();
            OnFlaskUpgraded?.Invoke(CurrentFlaskUses);
        }

        private void ApplySeedEffects() {
            CurrentFlaskUses = baseFlaskUses + SeedCount * flaskIncrementPerSeed;
        }
    }
}