using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [System.Serializable]
    public class EnemyInfo
    {
        public string name; // 소환할 적의 이름
        public int count;              // 소환할 적의 수
        public float spawnDelay;       // 소환 간 딜레이
    }

    [System.Serializable]
    public class StageInfo
    {
        public List<EnemyInfo> enemies;  // 스테이지에서 소환할 적들의 정보
    }
    [SerializeField] private List<StageInfo> stages; // 여러 스테이지 정보들
    public int currentStageIndex = 0; // 현재 진행중인 스테이지 인덱스
    public E_PoolingManager enemyPoolingManager; // 적 풀링 매니저
    public SpawnManager enemySpawner; // EnemySpawner를 참조하여 적 소환
    public int CurrentStageIndex => currentStageIndex; // 외부에서 현재 스테이지 인덱스를 조회할 수 있도록
    public TextMeshProUGUI StageUi;
    private void Start()
    {
        // 첫 번째 스테이지를 시작
        StartStage(currentStageIndex);
    }

    // 스테이지 시작 메서드
    public void StartStage(int stageIndex)
    {
        if (stageIndex < 0 || stageIndex >= stages.Count) return;
        int StageNumber = stageIndex+1;
        var stage = stages[stageIndex];
        // 스테이지의 적들을 소환하도록 EnemySpawner에 전달
        enemySpawner.StartStage(stageIndex, stage.enemies);
        StageUi.text = "Stage : " + (StageNumber).ToString();
    }

    public StageInfo GetStageInfo(int stageIndex)
    {
        if (stageIndex < 0 || stageIndex >= stages.Count)
            return null;

        return stages[stageIndex];
    }

    // 스테이지를 넘기는 메서드
    public void MoveToNextStage()
    {
        if (currentStageIndex + 1 < stages.Count)
        {
            currentStageIndex++;
            StartStage(currentStageIndex); // 다음 스테이지 시작
        }
        else
        {
            Debug.Log("끝");
        }
    }
}