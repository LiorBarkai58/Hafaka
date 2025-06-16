using UnityEngine;

public class SpellProjectile : MonoBehaviour {
    [SerializeField] private float ProjectileSpeed;

    [SerializeField] private float LifeTime;

    private Vector3 direction;

    public void SetDirection(Vector3 direction){
        this.direction = direction;
    }

    public void FixedUpdate()
    {
        transform.position += direction * ProjectileSpeed * Time.fixedDeltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}