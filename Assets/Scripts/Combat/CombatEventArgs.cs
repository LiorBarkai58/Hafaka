using UnityEngine;

public struct DamageDealtArgs
{
    public float damage;

    public bool isCrit;

    public float staggerValue;

    public GameObject attackingEntity;
    
    public Transform attackedEntity;
}