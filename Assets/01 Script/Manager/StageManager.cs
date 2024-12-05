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
    public EnemyType enemyType;  // �� Ÿ�� (enum���� ���� �ʿ�)
    public int count;  // �ش� Ÿ�� ���� ��
    public float spawnDelay;  // �� �� Ÿ�Ժ� ���� ��ȯ ������
    public float spawnInterval;  // ���� Ÿ�� �� ������ ����
}


public class StageManager : MonoBehaviour
{
    public EnemyType EnemyType { get; set; }
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
        List<EnemySpawnInfo> spawns = new List<EnemySpawnInfo>
        {
            new EnemySpawnInfo
            {
                enemyType = EnemyType.DPS,
                count = Mathf.Clamp(stageNumber/2, 1, 3),
                spawnDelay = 1f,  // ���� �� ��ȯ ���� �ð�
                spawnInterval = 0.7f  // ���� �� ���� ����
            },
            new EnemySpawnInfo
            {
                enemyType = EnemyType.Epic,
                count = Mathf.Clamp(stageNumber / 10, 0, 2),
                spawnDelay = 2f,  // ���� �� ��ȯ ���� �ð�
                spawnInterval = 5f  // ���� �� ���� ����
            },
            new EnemySpawnInfo
            {
                enemyType = EnemyType.Normal,
                count = Mathf.Clamp(stageNumber/2 , 1, 5),
                spawnDelay = 0.5f,  // �⺻ �� ��ȯ ���� �ð�
                spawnInterval = 0.5f  // �⺻ �� ���� ����
            },
            new EnemySpawnInfo
            {
                enemyType = EnemyType.Defence,
                count = Mathf.Clamp(stageNumber / 5, 0, 5),
                spawnDelay = 0.7f,  // ��� �� ��ȯ ���� �ð�
                spawnInterval = 1.0f  // ��� �� ���� ����
            }
        };
        return spawns;
    }
}