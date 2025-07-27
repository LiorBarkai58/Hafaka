using UnityEngine;
using UnityEngine.Events;

namespace Experience {
    public class ExperienceManager : MonoBehaviour
    {
        [Header("Essence (XP) Formula")]
        [SerializeField] private int baseEssenceCost = 100;
        [SerializeField] private float growthMultiplier = 1.5f;
        [SerializeField] private float levelExponent = 1.2f;

        [Header("Stat bumps per level (index 0 unused)")]
        [SerializeField] private StatIncrease[] statBumps;

        public int CurrentEssence { get; private set; }
        public int CurrentLevel { get; private set; } = 1;

        public event UnityAction<int> OnLevelUp;
        public event UnityAction<int> OnEssenceChanged;

        public void AddEssence(int amount) {
            CurrentEssence += amount;
            OnEssenceChanged?.Invoke(CurrentEssence);

            // Level up while enough essence for next level
            while (CurrentEssence >= EssenceForLevel(CurrentLevel + 1)) {
                CurrentLevel++;
                OnLevelUp?.Invoke(CurrentLevel);
            }
        }

        private int EssenceForLevel(int level) {
            // Formula: baseCost * growthMultiplier^(level-1) * level^levelExponent
            float cost = baseEssenceCost * Mathf.Pow(growthMultiplier, level - 1) * Mathf.Pow(level, levelExponent);
            return Mathf.FloorToInt(cost);
        }

        public StatIncrease GetStatIncreaseForLevel(int level) {
            if (level > 0 && level < statBumps.Length)
                return statBumps[level];
            return new StatIncrease();
        }
    }
}