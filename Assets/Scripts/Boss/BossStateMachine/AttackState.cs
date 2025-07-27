using UnityEngine;

public class BossAttackState : BossState
{
    public BossAttackState(BossController bossController, Animator animator, BossStates stateIdentifier) : base(bossController, animator, stateIdentifier)
    {
    }
}
