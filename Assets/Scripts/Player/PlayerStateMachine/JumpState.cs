using UnityEngine;

public class JumpState : PlayerState
{
    public JumpState(PlayerController playerController, Animator animator, PlayerStates stateIdentifier) : base(playerController, animator, stateIdentifier)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Jump State Entered");
    }
    public override void FixedUpdate()
    {
        playerController.HandleMovement();
        playerController.HandleGravity();

    }
}