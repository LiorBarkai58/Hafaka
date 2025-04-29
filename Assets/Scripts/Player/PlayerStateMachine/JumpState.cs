using UnityEngine;

public class JumpState : PlayerState
{
    public JumpState(PlayerController playerController, Animator animator) : base(playerController, animator)
    {
    }

    public override void OnEnter()
    {
        animator.CrossFade(JumpHash, crossfadeDuration);
    }
    public override void FixedUpdate()
    {
        playerController.HandleJump();
        playerController.HandleMovement();
    }
}