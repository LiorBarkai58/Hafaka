using UnityEngine;

public class JumpState : PlayerState
{
    public JumpState(PlayerController playerController, Animator animator) : base(playerController, animator)
    {
    }

    public override void OnEnter()
    {
        animator.CrossFade(JumpHash, crossfadeDuration);
        Debug.Log("Jump State Entered");
    }
    public override void FixedUpdate()
    {
        playerController.HandleMovement();
        playerController.HandleGravity();

    }
}