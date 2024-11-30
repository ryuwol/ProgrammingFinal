using UnityEngine;

public abstract class Soot : MonoBehaviour
{
    [SerializeField] protected float ShootSpeed;
    [SerializeField] protected float ShootDelay;
    protected float damage;
    public float shootdelay => ShootDelay;
    public float Damage => damage;
    public virtual void Init(float damage)
    {
        this.damage = damage;
    }
    public abstract void Shoot(Vector3 vec);
}