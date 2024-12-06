using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using static StageManager;

public class SpawnManager : MonoBehaviour
{
    Enemy Enemy;
    EnemyData EnemyData;
    private List<EnemyInfo> currentEnemiesToSpawn; // ���� ������������ ��ȯ�ؾ� �� ����
    private int enemiesRemaining; // ���� ���� ��
    public E_PoolingManager enemyPoolingManager;
    public StageManager stageManager; // StageManager�� �����Ͽ� ���������� ����

    // ���������� ���۵� �� ȣ��Ǵ� �޼���
    public void StartStage(int stageIndex, List<StageManager.EnemyInfo> enemiesToSpawn)
    {
        currentEnemiesToSpawn = enemiesToSpawn;

        // ��� ���� �� ������ ���
        enemiesRemaining = 0;
        foreach (var enemyInfo in currentEnemiesToSpawn)
        {
            enemiesRemaining += enemyInfo.count;
        }
        StartCoroutine(SpawnEnemies());
    }

    // ������ ��ȯ�ϴ� �޼��� (�ڷ�ƾ ���)
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
                // ������ �� ���� ���� ��ȯ
                Enemy.OnEnemyDefeated += OnEnemyDefeated;// �̺�Ʈ ����
                EnemyData.OnEnemyDefeated += OnEnemyDefeated; // �̺�Ʈ ����
                yield return new WaitForSeconds(enemyInfo.spawnDelay);
            }
        }
    }

    // ���� óġ�Ǿ��� �� ȣ��Ǵ� �޼���
    private void OnEnemyDefeated()
    {
        enemiesRemaining--;
        if (enemiesRemaining <= 0)
        {
            // ��� ���� óġ�Ǹ� ���� ���������� �̵�
            stageManager.MoveToNextStage();
        }
    }
}
