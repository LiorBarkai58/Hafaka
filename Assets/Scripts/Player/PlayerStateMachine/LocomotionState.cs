using UnityEngine;

public class LocomotionState : PlayerState
{
    public LocomotionState(PlayerController playerController, Animator animator, PlayerStates stateIdentifier) : base(playerController, animator, stateIdentifier)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override void FixedUpdate()
    {
        playerController.HandleMovement();
        playerController.HandleGravity();
        playerController.HandleRunSpeed();
    }
}