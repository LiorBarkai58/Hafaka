using System;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;
using UnityEngine.Events;


public class PlayerAttackManager : MonoBehaviour
{

    public event UnityAction OnComboEntered;
    public event UnityAction OnComboEnd;
    private AttackState playerAttackState;

    [SerializeField] private List<Spell> spells;
    [SerializeField] private PlayerWeaponManager weaponManager;

    private int currentComboCounter = 0;
    
    [SerializeField] private int maxCombo = 6;
    
    [SerializeField] private IntEventChannel comboCounterChannel;
    private void Start()
    {
        weaponManager.onHit += IncreaseComboIndex;
        foreach (Spell spell in spells)
        {
            spell.OnHit += IncreaseComboIndex;
        }
    }

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

    private void IncreaseComboIndex()
    {
        if (currentComboCounter < maxCombo)
        {
            currentComboCounter++;
            comboCounterChannel.Invoke(currentComboCounter);
        }
        else
        {
            currentComboCounter = 0;
        }
    }

    public void CastSpellAtIndex(int index)
    {
        if (index < spells.Count)
        {
            spells[index].Activate();
        }
    }
    
    


}