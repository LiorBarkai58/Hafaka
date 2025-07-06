using UnityEngine;
using UnityEngine.Events;

namespace Experience {
    public class ExperienceManager : MonoBehaviour
    {
        [Header("Essence (XP) Curve")]
        [Tooltip("Total essence required per level (index 0 unused).")]
        [SerializeField] private int[] totalEssencePerLevel;

        [Header("Stat bumps per level (index 0 unused)")]
        [SerializeField] private StatIncrease[] statBumps;

        public int CurrentEssence { get; private set; }
        public int CurrentLevel { get; private set; } = 1;

        public event UnityAction<int> OnLevelUp;
        public event UnityAction<int> OnEssenceChanged;

        public void AddEssence(int amount)
        {
            // Add to the current essence (XP)
            CurrentEssence += amount;
            
            // Inform UI for change in essence amount
            OnEssenceChanged?.Invoke(CurrentEssence);

            // Check for level ups
            while (CurrentLevel + 1 < totalEssencePerLevel.Length && 
                    CurrentEssence >= totalEssencePerLevel[CurrentLevel])
            {
                CurrentLevel++;
                OnLevelUp?.Invoke(CurrentLevel);
            }
        }

        public StatIncrease GetStatIncreaseForLevel(int level)
        {
            if (level > 0 && level < statBumps.Length)
                return statBumps[level];
            return new StatIncrease();
        }
    }
}