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
    // ���� ���������� �� ī��Ʈ�� �����ϱ� ���� ����
    private int totalEnemiesSpawned = 0;
    private int enemiesDefeated = 0;

    private void Start()
    {
        StartCoroutine(SpawnStageEnemies(currentStage));
    }

    IEnumerator SpawnStageEnemies(int stageNumber)
    {
        // �������� �ʱ�ȭ
        enemiesDefeated = 0;
        totalEnemiesSpawned = 0;

        if (!isFirstStage)
        {
            OnStageClear(); // ���� �������� ���� ó��
        }
        isFirstStage = false;

        // �ش� �������� ������ ��������
        StageData stageData = stageManager.stages[stageNumber - 1];

        // ��ü �������� ���� ������
        yield return new WaitForSeconds(stageData.stageSpawnDelay);

        // �� �� ������ ����
        foreach (var enemySpawn in stageData.enemySpawns)
        {
            // �ش� �� ������ ���� ������
            yield return new WaitForSeconds(enemySpawn.spawnDelay);

            // �ش� �� ����
            for (int i = 0; i < enemySpawn.count; i++)
            {
                SpawnEnemy(enemySpawn.enemyType);
                totalEnemiesSpawned++;
                // ���� �� ���� ���� ����
                yield return new WaitForSeconds(enemySpawn.spawnInterval);
            }
        }

        // ��� �� ���� �Ϸ� ���
        yield return StartCoroutine(WaitForStageClear());
    }

    IEnumerator WaitForStageClear()
    {
        // ��� ���� óġ�� ������ ���
        while (enemiesDefeated < totalEnemiesSpawned)
        {
            yield return null;
        }

        // ���� �������� ����
        currentStage++;
        StartCoroutine(SpawnStageEnemies(currentStage));
    }

    void SpawnEnemy(EnemyType type)
    {
        var Enemy = E_PoolingManager.GetObject(type);
        Vector3 EnemyPosition = new Vector3(8.5f, Random.Range(-4.3f, 4.3f));

        // �� ���� (���� ��ġ�� ���ϴ� ��� ����)
        Enemy.transform.position = EnemyPosition;

        // ���� Enemy ������Ʈ�� �ݹ� �߰�
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
        // �������� Ŭ���� �� ���� ���� ����
        EnemyData.IncreaseAllEnemyStats(0, 0.2f, 0.08f);
    }
}