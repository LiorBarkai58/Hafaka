using EventSystem;
using UnityEngine;

namespace Interactables {
    public class ReadingInteractable : MonoBehaviour, IInteractable {
        [SerializeField] private StringEventChannel readEventChannel;
        [SerializeField] private string readPanelText;
        
        public void Interact() {
            readEventChannel.Invoke(readPanelText);
        }

        public string GetPrompt() => "E to Read";
    }
}