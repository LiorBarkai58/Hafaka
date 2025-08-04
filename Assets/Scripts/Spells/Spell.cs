using UnityEngine;
using UnityEngine.Events;


public abstract class Spell : MonoBehaviour {
    [SerializeField] private float damage;

    public float Damage => damage;
    
    public event UnityAction OnHit;
    public abstract void Activate(float comboCounter);

    protected void InvokeHit()
    {
        OnHit?.Invoke();
    }


}