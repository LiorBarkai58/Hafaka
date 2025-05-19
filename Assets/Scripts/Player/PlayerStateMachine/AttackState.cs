using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utilities;


public class AttackState : PlayerState
{
    private CountdownTimer currentAttackTimer;

    private int comboIndex = 0;

    public event UnityAction OnComboEnd;

    private bool nextAttackQueued = false;

    private bool isAttacking;

    public AttackState(PlayerController playerController, Animator animator, PlayerStates stateIdentifier) : base(playerController, animator, stateIdentifier)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        comboIndex = 0;
        nextAttackQueued = false;
        TryQueueAttack();
        Debug.Log("Attack State Entered");
    }

    public override void FixedUpdate()
    {

    }
    public override void Update()
    {
        if (currentAttackTimer != null)
        {
            currentAttackTimer.Tick(Time.deltaTime);
        }
    }

    public void TryQueueAttack()
    {
        if (!CanQueueNextAttack() || nextAttackQueued) return;
        if (comboIndex > 2) comboIndex = 0;

        animator.SetInteger(AttackHash, comboIndex);
        nextAttackQueued = false;
        comboIndex++;
    }


    bool CanQueueNextAttack()
    {
        if (!isAttacking) return true;
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        return info.normalizedTime >= 0.3;

    }
    public void ComboEnd()
    {
        isAttacking = false;
        comboIndex = 0;
    }

    public void ComboStart()
    {
        isAttacking = true;
    }

    public void AttackEntered()
    {
        nextAttackQueued = false;
    }
    

    


    
}