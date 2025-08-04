using UnityEngine;

namespace EventSystem {
    [CreateAssetMenu(menuName = "Events/EmptyEventChannel")]
    public class EmptyEventChannel : EventChannel<Empty> { }
}