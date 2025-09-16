using EventSystem;
using ScriptableObjects;
using UnityEngine;

namespace Interactables {
    public class ReadingInteractable : MonoBehaviour, IInteractable {
        [SerializeField] private ReadableEventChannel readEventChannel;
        [SerializeField] private ReadObject readPanelText;
        
        public void Interact() {
            readEventChannel.Invoke(readPanelText);
        }

        public string GetPrompt() => "E to Read";
    }
}