using System.Collections.Generic;
using Interactables;
using UnityEngine;

namespace Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        public List<Interactable> _interactables;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Interactable")) return;
            
            var interactable = other.GetComponent<Interactable>();

            if (interactable != null)
            {
                _interactables.Add(interactable);
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
                Debug.Log("Item not in Range");
            }
        }
    }
}
