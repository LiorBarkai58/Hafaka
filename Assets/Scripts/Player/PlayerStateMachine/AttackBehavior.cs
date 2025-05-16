using UnityEngine;

public class AttackBehavior : StateMachineBehaviour
{
    private PlayerController playerController;
    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        FindPlayerController(animator);
        if(playerController){
            playerController.OnAttackEnd();
        }
    }

    private void FindPlayerController(Animator animator){
        if(!playerController) {
            playerController = animator.GetComponentInParent<PlayerController>();
        }
    }
}
