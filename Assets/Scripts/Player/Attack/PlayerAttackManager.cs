using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerAttackManager : MonoBehaviour
{

    public event UnityAction OnComboEntered;
    public event UnityAction OnComboEnd;
    private AttackState playerAttackState;

    [SerializeField] private List<Spell> spells;

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

    public void CastSpellAtIndex(int index)
    {
        if (index < spells.Count)
        {
            spells[index].Activate();
        }
    }
    
    


}