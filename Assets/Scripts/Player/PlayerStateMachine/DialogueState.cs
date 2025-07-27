using UnityEngine;


public class DialogueState : PlayerState
{
    public DialogueState(PlayerController playerController, Animator animator, PlayerStates stateIdentifier) : base(playerController, animator, stateIdentifier)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void FixedUpdate()
    {

    }
}