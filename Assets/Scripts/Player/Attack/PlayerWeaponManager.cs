using System.Collections.Generic;
using UnityEngine;


public class PLayerWeaponManager : MonoBehaviour
{
    [SerializeField] private List<Weapon> weapons;


    public void EnableColliders()
    {
        foreach (Weapon weapon in weapons)
        {
            weapon.EnableColliders();
        }
    }

    public void DisableColliders()
    {
        foreach (Weapon weapon in weapons)
        {
            weapon.DisableColliders();
        }
    }
}