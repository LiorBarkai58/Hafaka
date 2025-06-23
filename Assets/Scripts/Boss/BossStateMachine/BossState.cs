using UnityEngine;

public abstract class BossState : BaseState
{
    protected BossController BossController;
    protected Animator Animator;

    protected PlayerStates StateIdentifier;

    public BossState(BossController bossController, Animator animator, PlayerStates stateIdentifier)
    {
        this.BossController = bossController; // Replace playerController with bossController
        this.Animator = animator;
        this.StateIdentifier = stateIdentifier;
    }

    public override void OnEnter()
    {
        // animator.SetInteger(stateHash, (int)stateIdentifier);        
    }
}