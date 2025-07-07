using UnityEngine;

public class AttackBehavior : StateMachineBehaviour
{
    private PlayerAttackManager playerAttackManager;

    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        FindPlayerAttackManager(animator);
        if(playerAttackManager){
            playerAttackManager.ComboEntered();
        }
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        FindPlayerAttackManager(animator);
        if (playerAttackManager)
        {
            playerAttackManager.ComboEnd();
        }
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FindPlayerAttackManager(animator);
        if (playerAttackManager)
        {
            playerAttackManager.AttackEntered();
        }
    }

    private void FindPlayerAttackManager(Animator animator)
    {
        if (!playerAttackManager)
        {
            playerAttackManager = animator.GetComponent<PlayerAttackManager>();
        }
    }
}
