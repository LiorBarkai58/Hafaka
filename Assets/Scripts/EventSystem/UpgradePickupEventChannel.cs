using Interactables;
using UnityEngine;

namespace EventSystem {
    [CreateAssetMenu(menuName = "Events/UpgradePickupEventChannel")]
    public class UpgradePickupEventChannel : EventChannel<UpgradeType> { }
}