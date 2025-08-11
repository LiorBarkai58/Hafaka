using UnityEngine;

namespace EventSystem {
    [CreateAssetMenu(menuName = "Events/DamageArgsEventChannel")]
    public class DamageArgsEventChannel : EventChannel<DamageDealtArgs> { }
}