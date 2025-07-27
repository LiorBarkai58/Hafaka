using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;


    public void UpdateDialogueState(bool state)
    {
        playerController.UpdateDialogueState(state);
    }

}