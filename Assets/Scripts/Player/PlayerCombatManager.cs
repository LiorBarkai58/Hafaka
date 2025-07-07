using EventSystem;
using Player;
using UnityEngine;
using UnityEngine.Serialization;


public class PlayerCombatManager : EntityCombatManager
{
    [SerializeField] private float maxMana;
    
    private float _currentMana;

    [SerializeField] private PlayerCombatStateChannel combatStateChannel;

    protected override void OnEnable()
    {
        base.OnEnable();
        _currentMana = maxMana;
        UpdatePlayerState();
        
    }

    public override void TakeDamage(DamageDealtArgs damageDealtArgs)
    {
        base.TakeDamage(damageDealtArgs);
        UpdatePlayerState();
    }

    private void UpdatePlayerState()
    {
        combatStateChannel.Invoke(new PlayerCombatState()
        {
            CurrentHealth = currentHealth/2,
            MaxHealth = maxHealth,
            MaxMana = maxMana,
            CurrentMana = _currentMana/2
        });
    }
}