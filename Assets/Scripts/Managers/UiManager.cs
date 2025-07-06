using Interactables;
using Player;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class UiManager : MonoBehaviour {
        [Header("References")]
        [SerializeField] private PlayerInteractor playerInteractor;
        [SerializeField] private GameObject interactUi;
        [SerializeField] private TextMeshProUGUI interactText;

        private IInteractable _interactableOwner;

        private void OnEnable() {
            playerInteractor.InRange += ShowPrompt;
            playerInteractor.OutOfRange += HidePrompt;
        }

        private void OnDisable() {
            playerInteractor.InRange -= ShowPrompt;
            playerInteractor.OutOfRange -= HidePrompt;
        }

        private void ShowPrompt(IInteractable interactable) {
            if (_interactableOwner != null && _interactableOwner != interactable) return;

            _interactableOwner = interactable;
            interactText.text = interactable.GetPrompt();
            interactUi.SetActive(true);
        }

        private void HidePrompt(IInteractable interactable) {
            if (_interactableOwner != interactable) return;

            _interactableOwner = null;
            interactUi.SetActive(false);
        }
    }
}