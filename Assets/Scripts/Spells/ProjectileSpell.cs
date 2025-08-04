using UnityEngine;


public class ProjectileSpell : Spell
{
    [SerializeField] private Transform shootingPoint;

    [SerializeField] private SpellProjectile projectilePrefab;
    public override void Activate(float comboCounter)
    {
        SpellProjectile current = 
            Instantiate(projectilePrefab, shootingPoint.position, Quaternion.identity)
            .WithDamage(5f * comboCounter)
            .WithDirection(transform.forward);

        current.OnHit += InvokeHit;
    }
    
}