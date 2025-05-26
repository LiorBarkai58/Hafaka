using System;
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
        private readonly List<ItemData> items = new List<ItemData>();

        // Events for UI or other systems to subscribe to
        public event Action<ItemData> OnItemAdded;
        public event Action<ItemData> OnItemRemoved;

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
        public bool Add(ItemData item)
        {
            if (items.Count >= capacity)
            {
                Debug.LogWarning("Inventory is full!");
                return false;
            }
            items.Add(item);
            OnItemAdded?.Invoke(item);
            return true;
        }

        /// <summary>
        /// Try to remove an item. Returns true if it was in the inventory.
        /// </summary>
        public bool Remove(ItemData item)
        {
            if (items.Remove(item))
            {
                OnItemRemoved?.Invoke(item);
                return true;
            }
            Debug.LogWarning($"Tried to remove {item.itemName}, but it wasn't in inventory.");
            return false;
        }

        /// <summary>
        /// Read-only view of current items.
        /// </summary>
        public IReadOnlyList<ItemData> Items => items.AsReadOnly();

        /// <summary>
        /// (Optional) Check if there's room for N more items.
        /// </summary>
        public bool HasSpaceFor(int count = 1) => items.Count + count <= capacity;
    }
}