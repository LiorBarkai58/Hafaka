using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager.OnDialogueStart += () => playerManager.UpdateDialogueState(true);
        dialogueManager.OnDialogueEnd += () => playerManager.UpdateDialogueState(false);
    }


#if UNITY_EDITOR
    void OnValidate()
    {
        playerManager = FindFirstObjectByType<PlayerManager>();
        dialogueManager = FindFirstObjectByType<DialogueManager>();
    }
#endif




}

