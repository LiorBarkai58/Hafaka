using EventSystem;
using System.Collections;
using DG.Tweening;
using Experience;
using Interactables;
using Player;
using TimeCycleHook;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UiManager : MonoBehaviour {
        [Header("References")]
        [SerializeField] private PlayerInteractor playerInteractor;
        [SerializeField] private ExperienceManager xpManager;
        [SerializeField] private FlaskUpgradeManager flaskUpgradeManager;
        
        [Header("Events Listeners")]
        [SerializeField] private EmptyEventListener gameOverListener;
        [SerializeField] private PhaseEventListener phaseEventListener;
        [SerializeField] private PlayerCombatStateListener combatStateListener;
        
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

        [Header("Time Phase Change Message")] 
        [SerializeField] private GameObject timePhaseGameObject;
        [SerializeField] private TextMeshProUGUI timeChangeText;
        [SerializeField] private float timeChangeTextDuration = 3f;

        [Header("Game Over")] 
        [SerializeField] private GameObject gameOverParent;

        private IInteractable _interactableOwner;

        private void OnEnable() {
            gameOverListener.OnEvent += OpenGameOverUi;
            phaseEventListener.OnEvent += TimePhaseChange;
            combatStateListener.OnEvent += UpdateCombatState;
            playerInteractor.InRange += ShowPrompt;
            playerInteractor.OutOfRange += HidePrompt;
            xpManager.OnEssenceChanged += SetXpText;
            flaskUpgradeManager.OnFlaskUpgraded += SetFlaskAmountText;
        }

        private void OnDisable() {
            gameOverListener.OnEvent -= OpenGameOverUi;
            phaseEventListener.OnEvent -= TimePhaseChange;
            combatStateListener.OnEvent -= UpdateCombatState;
            playerInteractor.InRange -= ShowPrompt;
            playerInteractor.OutOfRange -= HidePrompt;
            xpManager.OnEssenceChanged -= SetXpText;
            flaskUpgradeManager.OnFlaskUpgraded -= SetFlaskAmountText;
        }

        public void TimePhaseChange(TimePhase timePhase) {
            switch (timePhase) {
                case TimePhase.War:
                    timeChangeText.text = "God of War is Ruling";
                    break;
                case TimePhase.Deceive:
                    timeChangeText.text = "God of Deception is Ruling";
                    break;
                case TimePhase.Vanity:
                    timeChangeText.text = "God of Vanity is Ruling";
                    break;
                case TimePhase.Lie:
                    timeChangeText.text = "God of Lies is Ruling";
                    break;
            }

            StartCoroutine(ShowTextForSeconds());
        }

        private IEnumerator ShowTextForSeconds() {
            timePhaseGameObject.SetActive(true);

            yield return new WaitForSeconds(timeChangeTextDuration);

            timePhaseGameObject.SetActive(false);
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

        private void SetFlaskAmountText(int amount) {
            flaskAmountText.text = amount.ToString();
        }

        private void UpdateCombatState(PlayerCombatState combatState)
        {
            hpBar.value = combatState.CurrentHealth / combatState.MaxHealth;
            
            manaBar.value = combatState.CurrentMana / combatState.MaxMana;

            hpBar.transform.DOScale(1.1f, 0.2f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                hpBar.transform.DOScale(1, 0.3f).SetEase(Ease.InQuad);
            });
        }

        private void OpenGameOverUi(Empty empty) {
            gameOverParent.SetActive(true);
        }
    }
}