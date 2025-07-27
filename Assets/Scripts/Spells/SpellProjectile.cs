using System;
using UnityEngine;

public class SpellProjectile : MonoBehaviour {
    [SerializeField] private float ProjectileSpeed;

    [SerializeField] private float LifeTime;

    private Vector3 direction;

    private float _damage;
    

    public SpellProjectile WithDirection(Vector3 direction){
        this.direction = direction;
        return this;
    }

    public SpellProjectile WithDamage(float Damage)
    {
        _damage = Damage;
        return this;
    }

    private void Start()
    {
        Destroy(gameObject, LifeTime);
    }

    public void FixedUpdate()
    {
        transform.position += direction * ProjectileSpeed * Time.fixedDeltaTime;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EntityCombatManager combatManager = collision.GetComponent<EntityCombatManager>();
            if (combatManager)
            {
                combatManager.TakeDamage(new DamageDealtArgs()
                {
                    damage = _damage
                });
                Destroy(gameObject);
            }
        }
    }
}