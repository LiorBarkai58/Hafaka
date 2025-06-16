using UnityEngine;


public class ProjectileSpell : Spell
{
    [SerializeField] private Transform shootingPoint;

    [SerializeField] private SpellProjectile projectilePrefab;
    public override void Activate()
    {
        SpellProjectile current = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.identity);
        current.SetDirection(transform.forward);
    }
    
}