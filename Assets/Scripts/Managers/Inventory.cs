using System.Collections.Generic;
using Interactables;
using UnityEngine;

namespace Managers {
    public class Inventory : MonoBehaviour {
        // The one and only Inventory instance
        public static Inventory Instance { get; private set; }

        [Header("Inventory Settings")]
        [SerializeField] private int capacity = 20;

        // Backing store for items
        private readonly List<ItemData> _items = new();

        // Events for UI or other systems to subscribe to
        //public event Action<ItemData> OnItemAdded;
        //public event Action<ItemData> OnItemRemoved;

        void Awake()
        {
            // Classic MonoBehaviour singleton pattern
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Try to add an item. Returns true if successful.
        /// </summary>
        public bool TryAdd(ItemData item)
        {
            foreach (var itemData in _items)
            {
                if (item != itemData) continue;
                
                itemData.quantity += item.quantity;
                return true;
            }
            
            if (_items.Count >= capacity)
            {
                Debug.Log("Inventory is full!");
                return false;
            }
            
            _items.Add(item);
            return true;
        }

        /// <summary>
        /// Try to remove an item. Returns true if it was in the inventory.
        /// </summary>
        public bool TryRemove(ItemData item, int quantity)
        {
            foreach (var itemData in _items)
            {
                if (itemData != item) continue;
                
                itemData.quantity -= quantity;

                if (itemData.quantity > 0) return true;
                
                _items.Remove(item);
                return true;
            }
            
            Debug.Log($"Tried to remove {item.itemName}, but it wasn't in inventory.");
            return false;
        }

        /// <summary>
        /// Read-only view of current items.
        /// </summary>
        public IReadOnlyList<ItemData> Items => _items.AsReadOnly();
    }
}