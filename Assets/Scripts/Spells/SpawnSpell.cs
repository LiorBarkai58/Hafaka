using UnityEngine;

namespace Spells
{
    public class SpawnSpell : Spell
    {
        [SerializeField] protected Transform shootingPoint;

        [SerializeField] protected DamagingArea projectilePrefab;
        public override void Activate(float comboCounter)
        {
            DamagingArea current = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.identity).WithDamage(Damage * comboCounter).WithDirection(shootingPoint.forward);
            current.OnHit += InvokeHit;
            
        }
    }
}