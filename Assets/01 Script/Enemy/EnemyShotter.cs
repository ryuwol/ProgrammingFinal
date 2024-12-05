using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyShotter : MonoBehaviour
{
    BulletType bulletType;
    [SerializeField] private float shootInterval = 1.0f;
    public EnemyData E_data;
    private void OnEnable()
    {
        StartCoroutine(ShootBulletRoutine());
    }

    IEnumerator ShootBulletRoutine()
    {
        while (gameObject.activeSelf)
        {
            GameObject Player;
            Player = GameObject.FindGameObjectWithTag("Player");
            Transform Playertransform= Player.transform;
            Vector3 direction = Vector3.up;
            yield return new WaitForSeconds(shootInterval);
            var bullet = B_PoolingManager.GetObject(this.bulletType);
            if (this.bulletType == BulletType.Enemy)
            {
                direction = Vector3.up;
            }
            else if(this.bulletType == BulletType.Wave)
            {
                direction = new Vector3(-1, Mathf.Cos(Mathf.PI * 2),0);
            }
            else if(bulletType == BulletType.Follow)
            {
                direction = (Playertransform.position.normalized);
            }
            bullet.transform.position = this.transform.position;
            bullet.Shoot(direction.normalized, E_data.DamageValue);
        }
    }
}