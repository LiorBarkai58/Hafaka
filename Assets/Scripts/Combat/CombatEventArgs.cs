using UnityEngine;

public struct DamageDealtArgs
{
    public float damage;

    public bool isCrit;

    public float staggerValue;

    public EntityCombatManager attackingEntity;
    
    public Transform attackedEntity;
}