using UnityEngine;

public enum BossStates
{
    Chase, Idle, Attacking
}
public abstract class BossState : BaseState
{
    protected BossController BossController;
    protected Animator Animator;

    protected BossStates StateIdentifier;

    public BossState(BossController bossController, Animator animator, BossStates stateIdentifier)
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