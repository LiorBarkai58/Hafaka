using UnityEngine;

namespace Interactables {
    public abstract class Interactable : MonoBehaviour {
        public virtual void Interact()
        {
            
        }

        public virtual string GetPrompt()
        {
            return "";
        }
    }
}