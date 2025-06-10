using UnityEngine;


[CreateAssetMenu(fileName = "DialogueCharacter", menuName = "Dialogue/DialogueCharacter")]
public class DialogueCharacter : ScriptableObject {
    [SerializeField] private Sprite characterSplash;

    public Sprite CharacterSplash => characterSplash;

    [SerializeField] private string characterName;

    public string Name => characterName;


}