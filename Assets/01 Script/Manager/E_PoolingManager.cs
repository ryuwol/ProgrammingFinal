using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class E_PoolingManager : MonoBehaviour
{
    public enum EnemyType
    {
        DPS,
        Epic,
        Normal,
        Defence
    }
    public static E_PoolingManager Instance { get; private set; }

    [SerializeField] private GameObject dpsEnemyPrefab;
    [SerializeField] private GameObject epicEnemyPrefab;
    [SerializeField] private GameObject normalEnemyPrefab;
    [SerializeField] private GameObject defenceEnemyPrefab;
    private Dictionary<EnemyType, Queue<Enemy>> enemyPoolQueues = new Dictionary<EnemyType, Queue<Enemy>>();

    private void Awake()
    {
        Instance = this;
        Initialize(EnemyType.DPS, 5);
        Initialize(EnemyType.Epic, 2);
        Initialize(EnemyType.Normal, 7);
        Initialize(EnemyType.Defence, 5);
    }

    private void Initialize(EnemyType type, int initCount)
    {
        enemyPoolQueues[type] = new Queue<Enemy>();

        for (int i = 0; i < initCount; i++)
        {
            enemyPoolQueues[type].Enqueue(CreateNewObject(type));
        }
    }

    private Enemy CreateNewObject(EnemyType type)
    {
        GameObject prefab = type switch
        {
            EnemyType.DPS => dpsEnemyPrefab,
            EnemyType.Epic => epicEnemyPrefab,
            EnemyType.Normal => normalEnemyPrefab,
            EnemyType.Defence => defenceEnemyPrefab,
            _ => null
        };
        var newObj = Instantiate(prefab).GetComponent<Enemy>();
        newObj.gameObject.SetActive(false);
        newObj.EnemyType = type;
        newObj.transform.SetParent(transform);
        return newObj;
    }
    public static Enemy GetObject(EnemyType type)
    {
        if (Instance.enemyPoolQueues[type].Count > 0)
        {
            var obj = Instance.enemyPoolQueues[type].Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject(type);
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }
    public static void ReturnObject(Enemy obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.enemyPoolQueues[obj.EnemyType].Enqueue(obj);
    }
}