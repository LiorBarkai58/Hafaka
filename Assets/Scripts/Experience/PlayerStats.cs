using UnityEngine;
using UnityEngine.Events;

namespace Experience {
    public class PlayerStats : MonoBehaviour
    {
        public static PlayerStats Instance { get; private set; }

        // Primary progression values
        public int Level { get; private set; } = 1;

        // Stats
        public int MaxHp { get; private set; }
        public int MaxFp { get; private set; }
        public int Strength { get; private set; }
        public int SpellDamage { get; private set; }

        public event UnityAction<int> OnLevelChanged;
        public event UnityAction OnStatsUpdated;
        
        [Header("References")]
        [SerializeField] private ExperienceManager xpManager;
        [SerializeField] private WeaponUpgradeManager weaponManager;
        [SerializeField] private FlaskUpgradeManager flaskManager;

        private void Awake()
        {
            if (Instance != null && Instance != this) Destroy(gameObject);
            else Instance = this;

            // Subscribe to events
            xpManager.OnLevelUp += HandleLevelUp;
            UpdateStats();
        }

        private void HandleLevelUp(int newLevel)
        {
            Level = newLevel;
            UpdateStats();
            OnLevelChanged?.Invoke(Level);
        }

        private void UpdateStats()
        {
            var bump = xpManager.GetStatIncreaseForLevel(Level);
            MaxHp = bump.hp;
            MaxFp = bump.fp;
            Strength = bump.strength;
            SpellDamage = bump.spellDamage;
            OnStatsUpdated?.Invoke();
        }
    }
}
