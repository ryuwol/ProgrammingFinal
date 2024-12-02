using UnityEngine;
using System;
using static E_PoolingManager;

public class Enemy : MonoBehaviour
{
    [SerializeField] private StatisticsManager statisticsManager;
    private Bullet Bullet;
    EnemyData enemyData;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("P_Bullet"))
        {
            Bullet = collision.gameObject.GetComponent<Bullet>();
            enemyData = this.gameObject.GetComponent<EnemyData>();
            if (Bullet != null)
            {
                enemyData.DecreaseHp(Bullet.PublicDamage);
            }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            E_PoolingManager.ReturnObject(this);
        }
        else if (collision.gameObject.CompareTag("Border"))
        {
            this.gameObject.transform.position = new Vector3(8.5f, UnityEngine.Random.Range(-4.3f, 4.3f));
            Debug.Log("Go back");
        }
    }
}