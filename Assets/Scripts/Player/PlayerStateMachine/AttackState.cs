using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

public enum AttackType
{
    Attack, Spell
}
public class AttackState : PlayerState
{
    private CountdownTimer currentAttackTimer;

    private int comboIndex = 0;

    public event UnityAction OnComboEnd;

    private bool nextAttackQueued = false;

    private bool isAttacking;

    private AttackType startAction = AttackType.Attack;

    public AttackState(PlayerController playerController, Animator animator, PlayerStates stateIdentifier) : base(playerController, animator, stateIdentifier)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        comboIndex = 0;
        nextAttackQueued = false;
        if(startAction == AttackType.Attack) TryQueueAttack();
        if(startAction == AttackType.Spell) TryQueueSpell();
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
        nextAttackQueued = true;
        comboIndex++;
    }

    public void TryQueueSpell()
    {
        if (!CanQueueNextAttack() || nextAttackQueued) return;

        animator.SetTrigger(SpellHash);
        nextAttackQueued = true;
    }


    bool CanQueueNextAttack()
    {
        if (!isAttacking) return true;
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        return info.normalizedTime >= 0.2;

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

    public void ChangeStartAction(AttackType attackType)
    {
        this.startAction = attackType;
    }
    

    


    
}