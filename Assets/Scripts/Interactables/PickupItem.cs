using Managers;
using UnityEngine;

namespace Interactables {
    public class PickupItem : MonoBehaviour, IInteractable {
        [SerializeField] private ItemData itemData;
        public void Interact()
        {
            if(Inventory.Instance.TryAdd(itemData))
                Destroy(gameObject);
        }

        public string GetPrompt() => "E to Pick up";
        
    }
}