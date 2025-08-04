using System;
using EventSystem;
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
            //upgradePickupEventListener.OnEvent +=
        }

        private void Start() {
            // Initialize with zero seeds
            ApplySeedEffects();
        }

        public void UpgradeFlask() {
            SeedCount++;
            ApplySeedEffects();
            OnFlaskUpgraded?.Invoke(CurrentFlaskUses);
        }

        private void ApplySeedEffects() {
            CurrentFlaskUses = baseFlaskUses + SeedCount * flaskIncrementPerSeed;
        }
    }
}