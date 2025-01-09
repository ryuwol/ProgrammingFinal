using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
public class EnemyShotter : MonoBehaviour
{
    public ShootType ShootType;
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
            if(ShootType == ShootType.None)
            {
                yield break;
            }
            yield return new WaitForSeconds(shootInterval);
            Bullet bullet = B_PoolingManager.B_Pooling.GetObject("E_Bullet");
            bullet.transform.position = this.transform.position;
            bullet.Shoot(E_data.DamageValue, ShootType);
        }
    }
}