using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerWeapon : Weapon
{
    private List<GameObject> enemiesHit = new List<GameObject>();
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !enemiesHit.Contains(other.gameObject))
        {
            EntityCombatManager hitTarget = other.GetComponent<EntityCombatManager>();
            if(hitTarget){
                enemiesHit.Add(hitTarget.gameObject);
                hitTarget.TakeDamage(new DamageDealtArgs()
                {
                    damage = 5,
                    isCrit = false,
                    staggerValue = 10,
                    attackedEntity = hitTarget.transform
                });
                InvokeHit();
            }
        }
    }

    public override void DisableColliders()
    {
        base.DisableColliders();
        enemiesHit.Clear();
    }

    
}