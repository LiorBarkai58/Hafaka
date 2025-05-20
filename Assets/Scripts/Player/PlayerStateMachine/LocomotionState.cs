using UnityEngine;

public class LocomotionState : PlayerState
{
    public LocomotionState(PlayerController playerController, Animator animator, PlayerStates stateIdentifier) : base(playerController, animator, stateIdentifier)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Locomotion State Entered");
    }
    public override void FixedUpdate()
    {
        playerController.HandleMovement();
        playerController.HandleGravity();
        playerController.HandleRunSpeed();
    }
}