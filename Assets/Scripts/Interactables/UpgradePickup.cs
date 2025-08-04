using EventSystem;
using UnityEngine;

namespace Interactables {
    public class UpgradePickup : MonoBehaviour, IInteractable {
        [SerializeField] private UpgradeType upgradeType;
        [SerializeField] private UpgradePickupEventChannel upgradePickupEventChannel;
        
        public virtual void Interact() {
            upgradePickupEventChannel.Invoke(upgradeType);
        }

        public virtual string GetPrompt() => "E to Pick up";
    }

    public enum UpgradeType {
        Flask,
        Hp,
        Fp,
    }
}