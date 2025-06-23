using UnityEngine;

public class IdleState : BossState
{
    public IdleState(BossController bossController, Animator animator, PlayerStates stateIdentifier) : base(bossController, animator, stateIdentifier)
    {
        
    }
}