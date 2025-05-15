using UnityEngine;

public abstract class PlayerState : BaseState {
    protected PlayerController playerController;
    protected Animator animator;

    protected static readonly int LocomationHash = Animator.StringToHash("Locomation");
    protected static readonly int JumpHash = Animator.StringToHash("Jump");

    protected static readonly int FallHash = Animator.StringToHash("Fall");

    protected const float crossfadeDuration = 0.1f;

    public PlayerState(PlayerController playerController, Animator animator) {
        this.playerController = playerController;
        this.animator = animator;
    }

}