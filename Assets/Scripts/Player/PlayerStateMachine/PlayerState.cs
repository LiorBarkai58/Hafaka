using UnityEngine;

public abstract class PlayerState : BaseState {
    protected PlayerController playerController;
    protected Animator animator;

    protected static readonly int LocomationHash = Animator.StringToHash("Locomotion");
    protected static readonly int JumpHash = Animator.StringToHash("Jump");

    protected static readonly int FallHash = Animator.StringToHash("Fall");

    protected static readonly int AttackHash = Animator.StringToHash("Attack");


    protected const float crossfadeDuration = 0.1f;

    public PlayerState(PlayerController playerController, Animator animator) {
        this.playerController = playerController;
        this.animator = animator;
    }

}