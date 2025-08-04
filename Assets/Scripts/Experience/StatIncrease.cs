using UnityEngine;

namespace Experience {
    [CreateAssetMenu(menuName = "Stats Upgrade/Stats")]
    public class StatIncrease : ScriptableObject
    {
        public int hp;
        public int fp;
        public int strength;
        public int spellDamage;
    }
}