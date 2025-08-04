using System;
using System.Collections.Generic;
using Combat;
using UnityEngine;
using UnityEngine.Events;


public class PlayerWeaponManager : WeaponsManager
{


    public event UnityAction onHit;

    public void UpdateComboMultiplier(int combo)
    {
    }

    public void Start()
    {
        foreach (Weapon weapon in weapons)
        {
            weapon.OnHit += InvokeHit;
        }
    }

    private void InvokeHit()
    {
        onHit?.Invoke();
    }
}