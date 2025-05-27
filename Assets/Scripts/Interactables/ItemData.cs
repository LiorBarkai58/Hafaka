using UnityEngine;

namespace Interactables {
    
    [CreateAssetMenu(menuName = "Items/Item Data")]
    public class ItemData : ScriptableObject {
        public string itemName;
        public int quantity = 1;
    }
}