using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private List<float> damageOfCombo;

    public List<float> DamageOfCombo => damageOfCombo;

    [SerializeField] private List<Collider> WeaponColliders;


    public void EnableColliders()
    {
        foreach (Collider collider in WeaponColliders)
        {
            collider.enabled = true;
        }
    }

    public void DisableColliders()
    {
        foreach (Collider collider in WeaponColliders)
        {
            collider.enabled = false;
        }
    }

}