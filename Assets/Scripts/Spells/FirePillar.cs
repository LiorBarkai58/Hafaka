using System.Collections;
using UnityEngine;

namespace Spells
{
    public class FirePillar : SpawnSpell
    {
        [SerializeField] private ParticleSystem Anticipation;
        
        [SerializeField] private float activationDelay = 1f; // Delay time in seconds
        
        public override void Activate()
        {
            StartCoroutine(ActivateWithDelay());
        }

        private IEnumerator ActivateWithDelay()
        {
            Instantiate(Anticipation, shootingPoint.position, Quaternion.identity).Play();
            yield return new WaitForSeconds(activationDelay); // Wait for the specified delay
            DamagingArea current = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.identity).WithDamage(Damage).WithDirection(shootingPoint.up);
        }

    }
}