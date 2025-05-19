using UnityEngine;


public class FallingState : PlayerState
{
    public FallingState(PlayerController playerController, Animator animator, PlayerStates stateIdentifier) : base(playerController, animator, stateIdentifier)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Falling State Entered");
    }

    public override void FixedUpdate()
    {
        playerController.HandleMovement();
        playerController.HandleGravity(1.5f);

    }
}