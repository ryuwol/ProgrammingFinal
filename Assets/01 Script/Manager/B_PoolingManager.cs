using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Player,
    Enemy,
    Follow,
    Wave,
}

public class B_PoolingManager : MonoBehaviour
{
    public static B_PoolingManager Instance { get; private set; }

    [SerializeField] private GameObject playerBulletPrefab;
    [SerializeField] private GameObject enemyBulletPrefab;
    [SerializeField] private GameObject FollowBulletPrefab;
    [SerializeField] private GameObject WaveBulletPrefab;

    private Dictionary<BulletType, Queue<Bullet>> bulletPoolQueues = new Dictionary<BulletType, Queue<Bullet>>();

    private void Awake()
    {
        Instance = this;
        Initialize(BulletType.Player, 10);
        Initialize(BulletType.Enemy, 10);
    }

    private void Initialize(BulletType type, int initCount)
    {
        bulletPoolQueues[type] = new Queue<Bullet>();

        for (int i = 0; i < initCount; i++)
        {
            bulletPoolQueues[type].Enqueue(CreateNewObject(type));
        }
    }

    private Bullet CreateNewObject(BulletType type)
    {
        GameObject prefab = type == BulletType.Player ? playerBulletPrefab : enemyBulletPrefab;
        var newObj = Instantiate(prefab).GetComponent<Bullet>();
        newObj.gameObject.SetActive(false);
        newObj.BulletType = type;
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public static Bullet GetObject(BulletType type)
    {
        if (Instance.bulletPoolQueues[type].Count > 0)
        {
            var obj = Instance.bulletPoolQueues[type].Dequeue();
            obj.gameObject.SetActive(true);
            obj.transform.SetParent(null);
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

    public static void ReturnObject(Bullet obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.bulletPoolQueues[obj.BulletType].Enqueue(obj);
    }
}