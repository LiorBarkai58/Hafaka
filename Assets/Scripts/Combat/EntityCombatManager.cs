using UnityEngine;


public class EntityCombatManager : MonoBehaviour
{
    [SerializeField] protected float MaxHealth;



    protected float currentHealth;
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