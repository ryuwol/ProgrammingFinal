using System.Collections;
using UnityEngine;
using static E_PoolingManager;

public class EnemySpawner : MonoBehaviour
{
    public E_PoolingManager poolingManager;
    public StageManager stageManager;
    public GameObject[] enemyPrefabs;
    private bool isFirstStage = true;
    public int currentStage = 1;
    // 현재 스테이지의 적 카운트를 추적하기 위한 변수
    private int totalEnemiesSpawned = 0;
    private int enemiesDefeated = 0;

    private void Start()
    {
        StartCoroutine(SpawnStageEnemies(currentStage));
    }

    IEnumerator SpawnStageEnemies(int stageNumber)
    {
        // 스테이지 초기화
        enemiesDefeated = 0;
        totalEnemiesSpawned = 0;

        if (!isFirstStage)
        {
            OnStageClear(); // 이전 스테이지 종료 처리
        }
        isFirstStage = false;

        // 해당 스테이지 데이터 가져오기
        StageData stageData = stageManager.stages[stageNumber - 1];

        // 전체 스테이지 시작 딜레이
        yield return new WaitForSeconds(stageData.stageSpawnDelay);

        // 각 적 유형별 스폰
        foreach (var enemySpawn in stageData.enemySpawns)
        {
            // 해당 적 유형의 시작 딜레이
            yield return new WaitForSeconds(enemySpawn.spawnDelay);

            // 해당 적 스폰
            for (int i = 0; i < enemySpawn.count; i++)
            {
                SpawnEnemy(enemySpawn.enemyType);
                totalEnemiesSpawned++;
                // 다음 적 스폰 사이 간격
                yield return new WaitForSeconds(enemySpawn.spawnInterval);
            }
        }

        // 모든 적 스폰 완료 대기
        yield return StartCoroutine(WaitForStageClear());
    }

    IEnumerator WaitForStageClear()
    {
        // 모든 적이 처치될 때까지 대기
        while (enemiesDefeated < totalEnemiesSpawned)
        {
            yield return null;
        }

        // 다음 스테이지 진행
        currentStage++;
        StartCoroutine(SpawnStageEnemies(currentStage));
    }

    void SpawnEnemy(EnemyType type)
    {
        var Enemy = E_PoolingManager.GetObject(type);
        Vector3 EnemyPosition = new Vector3(8.5f, Random.Range(-4.3f, 4.3f));

        // 적 생성 (실제 위치는 원하는 대로 조정)
        Enemy.transform.position = EnemyPosition;

        // 적의 Enemy 컴포넌트에 콜백 추가
        var Enemydata = Enemy.GetComponent<EnemyData>();
        if (Enemydata != null)
        {
            Enemydata.OnEnemyDefeated += HandleEnemyDefeated;
        }
    }

    void HandleEnemyDefeated()
    {
        enemiesDefeated++;
    }

    public void OnStageClear()
    {
        // 스테이지 클리어 시 적의 스탯 증가
        EnemyData.IncreaseAllEnemyStats(0, 0.2f, 0.08f);
    }
}