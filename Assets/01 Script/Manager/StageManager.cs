using System;
using System.Collections.Generic;
using UnityEngine;
using static E_PoolingManager;

[Serializable]
public class StageData
{
    public int stageNumber;
    public List<EnemySpawnInfo> enemySpawns;
    public float stageSpawnDelay;
}

[Serializable]
public class EnemySpawnInfo
{
    public E_PoolInfo PoolInfo;  // �� Ÿ�� (enum���� ���� �ʿ�)
    public int count;  // �ش� Ÿ�� ���� ��
    public float spawnDelay;  // �� �� Ÿ�Ժ� ���� ��ȯ ������
    public float spawnInterval;  // ���� Ÿ�� �� ������ ����
}


public class StageManager : MonoBehaviour
{
     public E_PoolInfo PoolInfo;
    public List<StageData> stages = new List<StageData>();

    private void Awake()
    {
        CreateStages();
    }

    private void CreateStages()
    {
        for (int i = 1; i <= 40; i++)
        {
            StageData stage = new StageData
            {
                stageNumber = i,
                stageSpawnDelay = 1f + (i * 0.05f),  // ������������ �ణ�� �����ϴ� ������
                enemySpawns = GenerateEnemySpawns(i)
            };
            stages.Add(stage);
        }
    }

    private List<EnemySpawnInfo> GenerateEnemySpawns(int stageNumber)
    {
        /*List<EnemySpawnInfo> spawns = new List<EnemySpawnInfo>
        {
            new EnemySpawnInfo
            {
                PoolInfo = PoolInfo.DPS,
                count = Mathf.Clamp(stageNumber/2, 1, 3),
                spawnDelay = 1f,  // ���� �� ��ȯ ���� �ð�
                spawnInterval = 0.7f  // ���� �� ���� ����
            },
            new EnemySpawnInfo
            {
                PoolInfo = PoolInfo.Epic,
                count = Mathf.Clamp(stageNumber / 10, 0, 2),
                spawnDelay = 2f,  // ���� �� ��ȯ ���� �ð�
                spawnInterval = 5f  // ���� �� ���� ����
            },
            new EnemySpawnInfo
            {
                PoolInfo = PoolInfo.Normal,
                count = Mathf.Clamp(stageNumber/2 , 1, 5),
                spawnDelay = 0.5f,  // �⺻ �� ��ȯ ���� �ð�
                spawnInterval = 0.5f  // �⺻ �� ���� ����
            },
            new EnemySpawnInfo
            {
                PoolInfo = PoolInfo.Name,
                count = Mathf.Clamp(stageNumber / 5, 0, 5),
                spawnDelay = 0.7f,  // ��� �� ��ȯ ���� �ð�
                spawnInterval = 1.0f  // ��� �� ���� ����
            }
        };
        return spawns;*/
        return null;
    }
}