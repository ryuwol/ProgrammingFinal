using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using static StageManager;

public class SpawnManager : MonoBehaviour
{
    Enemy Enemy;
    EnemyData EnemyData;
    private List<EnemyInfo> currentEnemiesToSpawn; // 현재 스테이지에서 소환해야 할 적들
    private int enemiesRemaining; // 남은 적의 수
    public E_PoolingManager enemyPoolingManager;
    public StageManager stageManager; // StageManager를 참조하여 스테이지를 관리

    // 스테이지가 시작될 때 호출되는 메서드
    public void StartStage(int stageIndex, List<StageManager.EnemyInfo> enemiesToSpawn)
    {
        currentEnemiesToSpawn = enemiesToSpawn;

        // 모든 적의 총 개수를 계산
        enemiesRemaining = 0;
        foreach (var enemyInfo in currentEnemiesToSpawn)
        {
            enemiesRemaining += enemyInfo.count;
        }
        StartCoroutine(SpawnEnemies());
    }

    // 적들을 소환하는 메서드 (코루틴 사용)
    private IEnumerator SpawnEnemies()
    {
        foreach (var enemyInfo in currentEnemiesToSpawn)
        {
            for (int i = 0; i < enemyInfo.count; i++)
            {
                var spawnEnemy = enemyPoolingManager.GetObject(enemyInfo.name);
                EnemyData = spawnEnemy.gameObject.GetComponent<EnemyData>();
                Enemy = spawnEnemy.gameObject.GetComponent<Enemy>();
                spawnEnemy.gameObject.SetActive(true);
                // 딜레이 후 다음 적을 소환
                Enemy.OnEnemyDefeated += OnEnemyDefeated;// 이벤트 연결
                EnemyData.OnEnemyDefeated += OnEnemyDefeated; // 이벤트 연결
                yield return new WaitForSeconds(enemyInfo.spawnDelay);
            }
        }
    }

    // 적이 처치되었을 때 호출되는 메서드
    private void OnEnemyDefeated()
    {
        enemiesRemaining--;
        if (enemiesRemaining <= 0)
        {
            // 모든 적이 처치되면 다음 스테이지로 이동
            stageManager.MoveToNextStage();
        }
    }
}
