using Unity.VisualScripting;
using UnityEngine;


public class PlayerWeapon : Weapon
{

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EntityCombatManager hitTarget = other.GetComponent<EntityCombatManager>();
            if(hitTarget){
                hitTarget.TakeDamage(new DamageDealtArgs()
                {
                    damage = 5,
                    isCrit = false,
                    staggerValue = 10,
                });
            }
        }
    }
}