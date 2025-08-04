using System.Collections;
using UnityEngine;

namespace Spells
{
    public class FirePillar : SpawnSpell
    {
        [SerializeField] private ParticleSystem anticipation;
        
        [SerializeField] private float activationDelay = 1f; // Delay time in seconds
        
        public override void Activate(float comboCounter)
        {
            StartCoroutine(ActivateWithDelay(comboCounter));
        }

        private IEnumerator ActivateWithDelay(float comboCounter)
        {
            ParticleSystem anticipiationCurrent = Instantiate(anticipation, shootingPoint.position, Quaternion.identity);
            anticipiationCurrent.Play();
            anticipiationCurrent.transform.rotation = Quaternion.Euler(90, 0, 0);
            yield return new WaitForSeconds(activationDelay); // Wait for the specified delay
            DamagingArea current = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.identity).WithDamage(Damage * comboCounter).WithDirection(shootingPoint.forward);
            current.OnHit += InvokeHit;
            
        }

    }
}