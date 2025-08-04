using EventSystem;
using MoreMountains.Feedbacks;
using Player;
using UnityEngine;
using UnityEngine.Serialization;


public class PlayerCombatManager : EntityCombatManager
{
    [SerializeField] private float maxMana;
    
    private float _currentMana;

    [SerializeField] private PlayerCombatStateChannel combatStateChannel;

    [SerializeField] private MMF_Player hitFeedbacks;
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
        hitFeedbacks.PlayFeedbacks();
    }

    private void UpdatePlayerState()
    {
        combatStateChannel.Invoke(new PlayerCombatState()
        {
            CurrentHealth = currentHealth,
            MaxHealth = maxHealth,
            MaxMana = maxMana,
            CurrentMana = _currentMana
        });
    }
}