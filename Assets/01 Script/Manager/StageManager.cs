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
    public EnemyType enemyType;  // 적 타입 (enum으로 정의 필요)
    public int count;  // 해당 타입 적의 수
    public float spawnDelay;  // 각 적 타입별 고유 소환 딜레이
    public float spawnInterval;  // 같은 타입 적 사이의 간격
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
                stageSpawnDelay = 1f + (i * 0.05f),  // 스테이지마다 약간씩 증가하는 딜레이
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
                spawnDelay = 1f,  // 딜러 적 소환 시작 시간
                spawnInterval = 0.7f  // 딜러 적 사이 간격
            },
            new EnemySpawnInfo
            {
                enemyType = EnemyType.Epic,
                count = Mathf.Clamp(stageNumber / 10, 0, 2),
                spawnDelay = 2f,  // 에픽 적 소환 시작 시간
                spawnInterval = 5f  // 에픽 적 사이 간격
            },
            new EnemySpawnInfo
            {
                enemyType = EnemyType.Normal,
                count = Mathf.Clamp(stageNumber/2 , 1, 5),
                spawnDelay = 0.5f,  // 기본 적 소환 시작 시간
                spawnInterval = 0.5f  // 기본 적 사이 간격
            },
            new EnemySpawnInfo
            {
                enemyType = EnemyType.Defence,
                count = Mathf.Clamp(stageNumber / 5, 0, 5),
                spawnDelay = 0.7f,  // 방어 적 소환 시작 시간
                spawnInterval = 1.0f  // 방어 적 사이 간격
            }
        };
        return spawns;
    }
}