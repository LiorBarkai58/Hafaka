using UnityEngine;


public abstract class Spell : MonoBehaviour {
    [SerializeField] private float damage;

    public float Damage => damage;
    public abstract void Activate();


}