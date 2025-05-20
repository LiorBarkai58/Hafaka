using UnityEngine;
public enum PlayerStates
{
    Locomotion, Jumping, Falling, Attacking
}

public abstract class PlayerState : BaseState
{
    protected PlayerController playerController;
    protected Animator animator;

    protected PlayerStates stateIdentifier = PlayerStates.Locomotion;

    protected static readonly int stateHash = Animator.StringToHash("PlayerState");

    protected static readonly int AttackHash = Animator.StringToHash("AttackState");

    protected static readonly int SpellHash = Animator.StringToHash("Spell");




    protected const float crossfadeDuration = 0.1f;

    public PlayerState(PlayerController playerController, Animator animator, PlayerStates stateIdentifier)
    {
        this.playerController = playerController;
        this.animator = animator;
        this.stateIdentifier = stateIdentifier;
    }
    public override void OnEnter()
    {
        animator.SetInteger(stateHash, (int)stateIdentifier);        
    }
    

}