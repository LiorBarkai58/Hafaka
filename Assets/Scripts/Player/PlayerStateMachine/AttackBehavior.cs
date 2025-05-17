using UnityEngine;

public class AttackBehavior : StateMachineBehaviour
{
    private PlayerController playerController;
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
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
