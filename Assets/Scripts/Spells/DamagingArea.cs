using System;
using UnityEngine;

namespace Spells
{
    public class DamagingArea : MonoBehaviour
    {
        [SerializeField] private float LifeTime = 2;

        [SerializeField] private ParticleSystem effect;
        private float _damage;
        
        public DamagingArea WithDamage(float damage)
        {
            _damage = damage;
            return this;
        }

        public DamagingArea WithDirection(Vector3 direction)
        {
            transform.forward = direction;
            return this;
        }

        private void Start()
        {
            effect.Play();
            Destroy(gameObject, LifeTime);
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
}