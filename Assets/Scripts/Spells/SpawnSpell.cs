using UnityEngine;

namespace Spells
{
    public class SpawnSpell : Spell
    {
        [SerializeField] private Transform shootingPoint;

        [SerializeField] private DamagingArea projectilePrefab;
        public override void Activate()
        {
            DamagingArea current = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.identity).WithDamage(Damage).WithDirection(shootingPoint.forward);
        }
    }
}