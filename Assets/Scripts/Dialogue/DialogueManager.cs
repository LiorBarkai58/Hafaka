using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour {
    [Header("Project references")]

    [SerializeField] private ScriptableDialogue defaultDialogue;

    [SerializeField] private InputReader inputReader;

    [Header("Dialogue UI")]

    [SerializeField] private RectTransform UIWindow;

    [SerializeField] private TextMeshProUGUI leftSpeaker;

    [SerializeField] private Image leftSplash;

    [SerializeField] private TextMeshProUGUI rightSpeaker;

    [SerializeField] private Image rightSplash;

    [SerializeField] private TextMeshProUGUI dialogueBody;

    [Header("Debug")]

    public ScriptableDialogue currentDialogue;


    void Start(){
        inputReader.Dialogue += OnDialgoue;
    }


    [ContextMenu("Start default")]
    public void DefaultDialogue(){
        StartDialogue(defaultDialogue);
    }


    private void OnDialgoue(){
        TryNextEntry();
    }

    private void StartDialogue(ScriptableDialogue dialogue){
        UIWindow.gameObject.SetActive(true);
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
                leftSpeaker.SetText(currentEntry.character.Name);
                leftSplash.sprite = currentEntry.character.CharacterSplash;
                break;
            case SpeakerType.rightSpeaker:
                rightSpeaker.SetText(currentEntry.character.Name);
                rightSplash.sprite = currentEntry.character.CharacterSplash;
                break;
            default:
                break;
        }
        dialogueBody.SetText(currentEntry.dialogue);


    }

    private void FinishDialogue(){
        //End dialogue logic
    }


}