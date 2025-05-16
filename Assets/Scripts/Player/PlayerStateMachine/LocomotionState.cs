using UnityEngine;

public class LocomotionState : PlayerState
{
    public LocomotionState(PlayerController playerController, Animator animator) : base(playerController, animator)
    {
    }

    public override void OnEnter()
    {
        animator.CrossFade(LocomationHash, crossfadeDuration);
        Debug.Log("Locomotion State Entered");
    }
    public override void FixedUpdate()
    {
        playerController.HandleMovement();
        playerController.HandleGravity();
        playerController.HandleRunSpeed();
    }
}