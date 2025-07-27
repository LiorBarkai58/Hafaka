using System;
using UnityEngine;
using Utilities;

public abstract class BossAbility : MonoBehaviour
{
    [SerializeField]
    protected BossAbilityData abilityData;
    
    private CountdownTimer _cooldownTimer;
    private CountdownTimer _durationTimer;

    

    private void Start()
    {
        _cooldownTimer = new CountdownTimer(abilityData.Cooldown);
        _durationTimer = new CountdownTimer(abilityData.Duration);
    }

    private void Update()
    {
        _cooldownTimer?.Tick(Time.deltaTime);
        _durationTimer?.Tick(Time.deltaTime);

    } 

    public virtual void Activate()
    {
        _cooldownTimer.Start();
        _durationTimer.Start();
        
        //Implement effect
    }
    
    public virtual void OnEnd()
    {
    }
    
    
    
    
}
