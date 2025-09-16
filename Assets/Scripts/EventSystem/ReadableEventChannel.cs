using ScriptableObjects;
using UnityEngine;

namespace EventSystem {
    [CreateAssetMenu(menuName = "Events/ReadableEventChannel")]
    public class ReadableEventChannel : EventChannel<ReadObject> { }
}