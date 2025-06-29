using UnityEngine;

public class BossAbilityData : ScriptableObject
{
    [SerializeField] private float cooldown;

    public float Cooldown => cooldown;
    
    [SerializeField] private float duration;

    public float Duration => duration;

    [SerializeField] private float damage;

    public float Damage => damage;

}