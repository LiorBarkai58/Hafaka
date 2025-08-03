using EventSystem;
using UnityEngine;

namespace Managers {
    public class GameManager : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private UiManager uiManager;
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private DialogueManager dialogueManager;

        [Header("Events")]
        [SerializeField] private EmptyEventChannel onGameOver;
    
        private void Start()
        {
            dialogueManager.OnDialogueStart += () => playerManager.UpdateDialogueState(true);
            dialogueManager.OnDialogueEnd += () => playerManager.UpdateDialogueState(false);
        }
    
        public void GameOver() {
            onGameOver?.Invoke(new Empty());
        }


#if UNITY_EDITOR
        void OnValidate()
        {
            playerManager = FindFirstObjectByType<PlayerManager>();
            dialogueManager = FindFirstObjectByType<DialogueManager>();
        }
#endif

    }
}

