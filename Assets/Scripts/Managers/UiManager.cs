using Experience;
using Interactables;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UiManager : MonoBehaviour {
        [Header("References")]
        [SerializeField] private PlayerInteractor playerInteractor;
        [SerializeField] private ExperienceManager xpManager;
        
        [Header("Interact")]
        [SerializeField] private GameObject interactUi;
        [SerializeField] private TextMeshProUGUI interactText;

        [Header("Bars")] 
        [SerializeField] private Slider hpBar;
        [SerializeField] private Slider manaBar;

        [Header("XP")] 
        [SerializeField] private TextMeshProUGUI xpText;

        [Header("Combo")] 
        [SerializeField] private TextMeshProUGUI comboText;

        [Header("Flask")] 
        [SerializeField] private TextMeshProUGUI flaskAmountText;

        private IInteractable _interactableOwner;

        private void OnEnable() {
            playerInteractor.InRange += ShowPrompt;
            playerInteractor.OutOfRange += HidePrompt;
            xpManager.OnEssenceChanged += SetXpText;
        }

        private void OnDisable() {
            playerInteractor.InRange -= ShowPrompt;
            playerInteractor.OutOfRange -= HidePrompt;
            xpManager.OnEssenceChanged -= SetXpText;
        }

        private void ShowPrompt(IInteractable interactable) {
            if (_interactableOwner != null && _interactableOwner != interactable) return;

            _interactableOwner = interactable;
            interactText.text = interactable.GetPrompt();
            interactUi.SetActive(true);
        }

        private void HidePrompt(IInteractable interactable) {
            if (_interactableOwner != interactable && interactable != null) return;

            _interactableOwner = null;
            interactUi.SetActive(false);
        }

        private void SetXpText(int newXp) {
            xpText.text = newXp.ToString();
        }
    }
}