using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletType
    {
        Player,
        Enemy,
        Follow,
        Wave,
    }
    public float BulletSpeed = 10f;
    public float PublicDamage;
    private Vector3 direction;

    public void Shoot(Vector3 direction, float _Damage)
    {
        this.direction = direction.normalized;
        this.PublicDamage = _Damage;
    }

    public void DestroyBullet()
    {
        B_PoolingManager.ReturnObject(this);
    }

    void Update()
    {
        // 계산된 방향으로 총알 이동
        transform.Translate(direction * BulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.BulletType == BulletType.Enemy && collision.gameObject.tag == "Player")
        {
            B_PoolingManager.ReturnObject(this);
        }
        else if (this.BulletType == BulletType.Player && collision.gameObject.tag == "Enemy")
        {
            B_PoolingManager.ReturnObject(this);
        }
        else if(collision.gameObject.tag=="Border" || collision.gameObject.tag == "UpDownBorder")
        {
            B_PoolingManager.ReturnObject(this);
        }
    }
}