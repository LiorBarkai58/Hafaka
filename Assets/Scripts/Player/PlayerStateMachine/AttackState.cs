using UnityEngine;
using UnityEngine.Events;


public class AttackState : PlayerState
{
    public AttackState(PlayerController playerController, Animator animator) : base(playerController, animator)
    {

    }

    public override void OnEnter()
    {
        animator.CrossFade(AttackHash, crossfadeDuration);
        playerController.AttackStart();
        Debug.Log("Attack State Entered");
    }

    public override void FixedUpdate()
    {

    }
}