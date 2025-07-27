using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Combat
{
    public class EnemyWeapon : Weapon
    {
        private List<GameObject> playerHit = new List<GameObject>();
            void OnTriggerEnter(Collider other)
            {
                if (other.CompareTag("Player") && !playerHit.Contains(other.gameObject))
                {
                    EntityCombatManager hitTarget = other.GetComponent<EntityCombatManager>();
                    if(hitTarget){
                        playerHit.Add(hitTarget.gameObject);
                        hitTarget.TakeDamage(new DamageDealtArgs()
                        {
                            damage = 5,
                            isCrit = false,
                            staggerValue = 10,
                        });
                    }
                }
            }
        
            public override void DisableColliders()
            {
                base.DisableColliders();
                playerHit.Clear();
                Debug.Log("Disable");
            }
    }
}