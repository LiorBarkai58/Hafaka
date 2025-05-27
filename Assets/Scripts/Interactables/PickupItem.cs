using Managers;
using UnityEngine;

namespace Interactables {
    public class PickupItem : Interactable {
        [SerializeField] private ItemData itemData;
        public override void Interact()
        {
            if(Inventory.Instance.TryAdd(itemData))
                Destroy(gameObject);
        }

        public override string GetPrompt() => "E to Pick up";
        
    }
}