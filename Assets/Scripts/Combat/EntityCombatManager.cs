using UnityEngine;


public class EntityCombatManager : MonoBehaviour
{
    [SerializeField] protected float maxHealth;

    public float MaxHealth => maxHealth;

    protected float currentHealth;

    public float CurrentHealth => currentHealth;
    void OnEnable()
    {
        currentHealth = MaxHealth;
    }

    public virtual void TakeDamage(DamageDealtArgs damageDealtArgs)
    {
        currentHealth -= damageDealtArgs.damage;

        Debug.Log($"Took {damageDealtArgs.damage}");

        if (currentHealth <= 0) Death();
    }

    protected virtual void Death() {
        Debug.Log("Entity died");
    }
}