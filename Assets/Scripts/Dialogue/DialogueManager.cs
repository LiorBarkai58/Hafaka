using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour {
    [Header("Project references")]

    [SerializeField] private ScriptableDialogue defaultDialogue;

    [SerializeField] private InputReader inputReader;

    [SerializeField] private InputBlocker playerBlocker;

    [Header("Dialogue UI")]

    [SerializeField] private RectTransform UIWindow;

    [SerializeField] private TextMeshProUGUI leftSpeaker;

    [SerializeField] private Image leftSplash;

    [SerializeField] private TextMeshProUGUI rightSpeaker;

    [SerializeField] private Image rightSplash;

    [SerializeField] private TextMeshProUGUI dialogueBody;

    [Header("Debug")]

    public ScriptableDialogue currentDialogue;

    #region Events
    public event UnityAction OnDialogueStart;
    public event UnityAction OnDialogueEnd;

    #endregion

    private static readonly string DialogueBlocker = "Dialogue";


    void Start(){
        inputReader.Dialogue += OnDialgoue;
    }


    [ContextMenu("Start default")]
    public void DefaultDialogue(){
        StartDialogue(defaultDialogue);
    }

    [ContextMenu("Debug next entry")]
    public void DebugNextEntry(){
        TryNextEntry();
    }


    private void OnDialgoue()
    {
        TryNextEntry();
    }

    private void StartDialogue(ScriptableDialogue dialogue){
        UIWindow.gameObject.SetActive(true);
        OnDialogueStart?.Invoke();
        playerBlocker.AddBlocker(DialogueBlocker);
        TryNextEntry();
    }

    private void TryNextEntry(){
        DialogueEntry currentEntry = currentDialogue.GetCurrentEntry();
        if(currentEntry == null) {
            FinishDialogue();
            return;
        }
        switch (currentEntry.speaker)
        {
            case SpeakerType.leftSpeaker:
                leftSpeaker.SetText(currentEntry.LeftCharacter.Name);
                leftSplash.sprite = currentEntry.LeftCharacter.CharacterSplash;
                rightSpeaker.SetText(currentEntry.RightCharacter.Name);
                rightSplash.sprite = currentEntry.RightCharacter.CharacterSplash;
                break;
            case SpeakerType.rightSpeaker:
                rightSpeaker.SetText(currentEntry.RightCharacter.Name);
                rightSplash.sprite = currentEntry.RightCharacter.CharacterSplash;
                leftSpeaker.SetText(currentEntry.LeftCharacter.Name);
                leftSplash.sprite = currentEntry.LeftCharacter.CharacterSplash;
                break;
            default:
                break;
        }
        dialogueBody.SetText(currentEntry.dialogue);


    }

    private void FinishDialogue()
    {
        //End dialogue logic
        OnDialogueEnd?.Invoke();
        playerBlocker.RemoveBlocker(DialogueBlocker);
        UIWindow.gameObject.SetActive(false);
    }


}