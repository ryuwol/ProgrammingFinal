using UnityEngine;
using System;
using static E_PoolingManager;

public class Enemy : MonoBehaviour
{
    [SerializeField] private StatsManager statisticsManager;
    private Bullet Bullet;
    EnemyData enemyData;
    EnemyController EnemyController;
    public delegate void EnemyDefeatedHandler();
    public event EnemyDefeatedHandler OnEnemyDefeated;
    private void OnEnable()
    {
        EnemyController = gameObject.GetComponent<EnemyController>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "P_Bullet")
        {
            Bullet = collision.gameObject.GetComponent<Bullet>();
            enemyData = this.gameObject.GetComponent<EnemyData>();
            if (Bullet != null)
            {
                enemyData.DecreaseHp(Bullet.PublicDamage);
            }
        }
        else if (collision.gameObject.tag == "Player")
        {
            E_PoolingManager.E_Pooling.ReturnObject(this.gameObject);
            OnEnemyDefeated?.Invoke();            // 적이 처치되었을 때 OnEnemyDefeated 이벤트 호출
            OnEnemyDefeated = null;               // 이벤트 초기화
        }
        else if (collision.gameObject.tag == "Border")
        {
            EnemyController.SetTransform();
        }
    }
}