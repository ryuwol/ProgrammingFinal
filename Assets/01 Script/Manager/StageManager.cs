using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [System.Serializable]
    public class EnemyInfo
    {
        public string name; // ��ȯ�� ���� �̸�
        public int count;              // ��ȯ�� ���� ��
        public float spawnDelay;       // ��ȯ �� ������
    }

    [System.Serializable]
    public class StageInfo
    {
        public List<EnemyInfo> enemies;  // ������������ ��ȯ�� ������ ����
    }
    [SerializeField] private List<StageInfo> stages; // ���� �������� ������
    public int currentStageIndex = 0; // ���� �������� �������� �ε���
    public E_PoolingManager enemyPoolingManager; // �� Ǯ�� �Ŵ���
    public SpawnManager enemySpawner; // EnemySpawner�� �����Ͽ� �� ��ȯ
    public int CurrentStageIndex => currentStageIndex; // �ܺο��� ���� �������� �ε����� ��ȸ�� �� �ֵ���
    public TextMeshProUGUI StageUi;
    private void Start()
    {
        // ù ��° ���������� ����
        StartStage(currentStageIndex);
    }

    // �������� ���� �޼���
    public void StartStage(int stageIndex)
    {
        if (stageIndex < 0 || stageIndex >= stages.Count) return;
        int StageNumber = stageIndex+1;
        var stage = stages[stageIndex];
        // ���������� ������ ��ȯ�ϵ��� EnemySpawner�� ����
        enemySpawner.StartStage(stageIndex, stage.enemies);
        StageUi.text = "Stage : " + (StageNumber).ToString();
    }

    public StageInfo GetStageInfo(int stageIndex)
    {
        if (stageIndex < 0 || stageIndex >= stages.Count)
            return null;

        return stages[stageIndex];
    }

    // ���������� �ѱ�� �޼���
    public void MoveToNextStage()
    {
        if (currentStageIndex + 1 < stages.Count)
        {
            currentStageIndex++;
            StartStage(currentStageIndex); // ���� �������� ����
        }
        else
        {
            Debug.Log("��");
        }
    }
}