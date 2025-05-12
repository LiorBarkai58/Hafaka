using UnityEngine;


public class FallingState : PlayerState
{
    public FallingState(PlayerController playerController, Animator animator) : base(playerController, animator)
    {

    }

    public override void OnEnter()
    {
        animator.CrossFade(FallHash, crossfadeDuration);
        Debug.Log("Falling State Entered");
    }

    public override void FixedUpdate()
    {
        playerController.HandleMovement();
    }
}