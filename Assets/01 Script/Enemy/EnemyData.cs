using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyData : MonoBehaviour
{
    E_PoolingManager poolManager;
    private StatsManager StatisticsManager;
    [SerializeField] private float Speed;
    [SerializeField] private float Hp;
    [SerializeField] private float Damage;
    public delegate void EnemyDefeatedHandler();
    public event EnemyDefeatedHandler OnEnemyDefeated;
    public float SpeedValue => Speed;
    public float DamageValue => Damage;
    public float HpValue => Hp;
    public void DecreaseHp(float Damage)
    {
        StatisticsManager.Hp -= Damage;
        if (StatisticsManager.Hp <= 0) 
        {
            OnEnemyDefeated?.Invoke();            // ���� óġ�Ǿ��� �� OnEnemyDefeated �̺�Ʈ ȣ��
            OnEnemyDefeated = null;               // �̺�Ʈ �ʱ�ȭ
            E_PoolingManager.E_Pooling.ReturnObject(this.gameObject);
        }
    }
    private void OnEnable()
    {
        StatisticsManager = new StatsManager(Speed, Hp, Damage);
    }

}