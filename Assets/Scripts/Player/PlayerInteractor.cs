using System.Collections.Generic;
using Interactables;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerInteractor : MonoBehaviour {
        [SerializeField] private InputReader input;
        
        private readonly List<IInteractable> _interactables = new();

        public event UnityAction<IInteractable> InRange;
        public event UnityAction<IInteractable> OutOfRange;

        private void OnEnable()
        {
            input.Interact += OnInteract;
        }

        private void OnDisable()
        {
            input.Interact -= OnInteract;
        }

        private void OnInteract()
        {
            if (_interactables.Count == 0) return;
            
            _interactables[0].Interact();
            _interactables.Remove(_interactables[0]);
            if (_interactables.Count == 0)
            {
                OutOfRange?.Invoke(null);
            }
            else
            {
                InRange?.Invoke(_interactables[0]);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Interactable")) return;
            
            var interactable = other.GetComponent<IInteractable>();

            if (interactable != null)
            {
                _interactables.Add(interactable);
                InRange?.Invoke(interactable);
                Debug.Log("Item in Range");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Interactable")) return;
            
            var interactable = other.GetComponent<IInteractable>();

            if (interactable != null)
            {
                _interactables.Remove(interactable);
                OutOfRange?.Invoke(interactable);
                Debug.Log("Item not in Range");
            }
            
        }
    }
}
