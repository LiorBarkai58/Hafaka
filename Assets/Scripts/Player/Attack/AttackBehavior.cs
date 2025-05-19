using UnityEngine;

public class AttackBehavior : StateMachineBehaviour
{
    private PlayerAttackManager playerAttackManager;

    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        findPlayerAttackManager(animator);
        if(playerAttackManager){
            playerAttackManager.ComboEntered();
        }
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        findPlayerAttackManager(animator);
        if (playerAttackManager)
        {
            playerAttackManager.ComboEnd();
        }
    }

    private void findPlayerAttackManager(Animator animator)
    {
        if (!playerAttackManager)
        {
            playerAttackManager = animator.GetComponentInParent<PlayerAttackManager>();
        }
    }
}
