using UnityEngine;
using System.Collections.Generic;

public enum SpeakerType {
    
    leftSpeaker,
    rightSpeaker
}
[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/Dialogue")]
public class ScriptableDialogue : ScriptableObject
{

    [SerializeField] private List<DialogueEntry> dialogues;


    public List<DialogueEntry> Dialogues => dialogues;

    private int currentIndex = 0;

    void OnEnable()
    {
        currentIndex = 0;
    }

    public DialogueEntry GetCurrentEntry()
    {
        if (currentIndex < dialogues.Count) return dialogues[currentIndex++];
        currentIndex = 0;
        return null;
    }
    
}


[System.Serializable]
public class DialogueEntry
{
    public string dialogue;
    public SpeakerType speaker;
    public DialogueCharacter LeftCharacter;
    public DialogueCharacter RightCharacter;
    

}