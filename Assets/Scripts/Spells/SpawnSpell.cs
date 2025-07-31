using UnityEngine;

namespace Spells
{
    public class SpawnSpell : Spell
    {
        [SerializeField] protected Transform shootingPoint;

        [SerializeField] protected DamagingArea projectilePrefab;
        public override void Activate()
        {
            DamagingArea current = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.identity).WithDamage(Damage).WithDirection(shootingPoint.forward);
            current.OnHit += InvokeHit;
            
        }
    }
}