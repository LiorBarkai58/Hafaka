using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private List<float> damageOfCombo;

    public List<float> DamageOfCombo => damageOfCombo;

    [SerializeField] private List<Collider> WeaponColliders;

    public event UnityAction OnHit;
    public virtual void EnableColliders()
    {
        foreach (Collider collider in WeaponColliders)
        {
            collider.enabled = true;
        }
    }

    public virtual void DisableColliders()
    {
        foreach (Collider collider in WeaponColliders)
        {
            collider.enabled = false;
        }
    }

    protected void InvokeHit()
    {
        OnHit?.Invoke();
    }

}