using System.Collections.Generic;
using Interactables;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private InputReader input;
        
        private List<Interactable> _interactables;

        public event UnityAction<Interactable> InRange;
        public event UnityAction OutOfRange;

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
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Interactable")) return;
            
            var interactable = other.GetComponent<Interactable>();

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
            
            var interactable = other.GetComponent<Interactable>();

            if (interactable != null)
            {
                _interactables.Remove(interactable);
                OutOfRange?.Invoke();
                Debug.Log("Item not in Range");
            }
        }
    }
}
