using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerAttackManager : MonoBehaviour
{

    public event UnityAction OnAttackEntered;
    public event UnityAction OnComboEntered;
    public event UnityAction OnComboEnd;
    private AttackState playerAttackState;


    public void AssignAttackState(AttackState attackState)
    {
        this.playerAttackState = attackState;
    }
    public void AttackEntered()
    {
        playerAttackState.AttackEntered();
    }
    public void ComboEntered()
    {
        playerAttackState.ComboStart();
    }
    public void ComboEnd()
    {
        playerAttackState.ComboEnd();
        OnComboEnd?.Invoke();
    }
    
    


}