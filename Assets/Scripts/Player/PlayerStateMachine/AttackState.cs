using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Utilities;


public class AttackState : PlayerState
{
    private CountdownTimer currentAttackTimer;

    private int comboIndex = 1;

    public event UnityAction OnAttackEnd;
    public AttackState(PlayerController playerController, Animator animator) : base(playerController, animator)
    {

    }

    public override void OnEnter()
    {
        comboIndex = 1;
        playerController.AttackStart();
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
            Debug.Log($"Running: {currentAttackTimer.IsRunning}, progress {currentAttackTimer.Progress}");
        }
    }

    public void TryQueueAttack(){
        if(currentAttackTimer != null && !CanQueueNextAttack()) return;

        if(currentAttackTimer != null) currentAttackTimer.OnTimerStop -= AttackEnd;
        animator.CrossFade($"Attack {comboIndex}", crossfadeDuration);

        AnimatorStateInfo info = animator.GetNextAnimatorStateInfo(0);

        currentAttackTimer = new CountdownTimer(info.length);

        currentAttackTimer.Start();

        currentAttackTimer.OnTimerStop += AttackEnd;

        comboIndex++;
        

    }

    bool CanQueueNextAttack()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        return info.normalizedTime >= 0.8f; // Allow next attack partway through
    }


    private void AttackEnd(){
        playerController.OnAttackEnd();
    }
}