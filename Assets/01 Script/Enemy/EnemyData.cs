using System.Collections.Generic;
using UnityEngine;
using System;
using static E_PoolingManager;

public class EnemyData : MonoBehaviour
{
    Enemy Enemy;
    E_PoolingManager poolManager;
    private static List<EnemyData> allEnemyData = new List<EnemyData>();
    private StatisticsManager StatisticsManager;
    [SerializeField] private float Speed;
    [SerializeField] private float Hp;
    [SerializeField] private float Damage;
    public event Action OnEnemyDefeated;
    public float SpeedValue => Speed;
    public float DamageValue => Damage;
    public float HpValue => Hp;
    void Awake()
    {
        allEnemyData.Add(this);
        Enemy = this.gameObject.GetComponent<Enemy>();
    }
    public static void IncreaseAllEnemyStats(float speedMultiplier, float hpMultiplier, float damageMultiplier)
    {
        // 활성/비활성 상관없이 모든 적 데이터에 대해 스탯 증가
        foreach (var enemyData in allEnemyData)
        {
            enemyData.Speed += speedMultiplier;
            enemyData.Hp += hpMultiplier;
            enemyData.Damage += damageMultiplier;
        }
    }
    public void DecreaseHp(float Damage)
    {
        StatisticsManager.Hp -= Damage;
        if (StatisticsManager.Hp <= 0) 
        {
            OnEnemyDefeated?.Invoke();
            E_PoolingManager.ReturnObject(Enemy);
        }
    }
    private void OnEnable()
    {
        StatisticsManager = new StatisticsManager(Speed, Hp, Damage);
    }
    private void OnDisable()
    {
        // 오브젝트 비활성화 시 모든 이벤트 구독 해제
        OnEnemyDefeated = null;
    }
}