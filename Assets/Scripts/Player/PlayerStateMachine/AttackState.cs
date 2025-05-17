using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utilities;


public class AttackState : PlayerState
{
    private List<AnimationStateSO> comboAttacks;
    private CountdownTimer currentAttackTimer;

    private int comboIndex = 0;

    public event UnityAction OnAttackEnd;

    private bool nextAttackQueued = false;
    public AttackState(PlayerController playerController, Animator animator, List<AnimationStateSO> comboAttacks) : base(playerController, animator)
    {
        this.comboAttacks = comboAttacks;
    }

    public override void OnEnter()
    {
        comboIndex = 0;
        TryQueueAttack();
        Debug.Log("Attack State Entered");
    }

    public override void FixedUpdate()
    {

    }
    public override void Update()
    {
        if(currentAttackTimer != null){
            currentAttackTimer.Tick(Time.deltaTime);
        }
    }

    public void TryQueueAttack(){
        if(currentAttackTimer != null && !CanQueueNextAttack()) return;

        if(comboAttacks == null || comboAttacks.Count == 0) return;
        if(nextAttackQueued) return;
        if(currentAttackTimer != null)
        {
            if(!currentAttackTimer.IsRunning) PlayAttack();
            else {
                currentAttackTimer.OnTimerStop -= AttackEnd;
                currentAttackTimer.OnTimerStop += PlayAttack;
                nextAttackQueued = true;
            }
        }
        else{
            PlayAttack();
        }
        
        
    }

    private void PlayAttack(){
        if(comboIndex >= comboAttacks.Count) comboIndex = 0;

        AnimationStateSO currentAttack = comboAttacks[comboIndex];

        animator.CrossFade(currentAttack.hash, crossfadeDuration);

        currentAttackTimer = new CountdownTimer(currentAttack.duration);

        currentAttackTimer.Start();

        currentAttackTimer.OnTimerStop += AttackEnd;

        comboIndex++;
        nextAttackQueued = false;
    }
    bool CanQueueNextAttack()
    {
        if(currentAttackTimer == null) return true;
        else {
            if(currentAttackTimer.IsRunning){
                return currentAttackTimer.Progress >= 0.4f;
            }
            return true;
        }
    }
    private void AttackEnd(){
        playerController.OnAttackEnd();
    }
    

    


    
}