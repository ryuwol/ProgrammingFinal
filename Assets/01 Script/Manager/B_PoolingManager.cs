using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class B_PoolInfo
{
    public string Name;
    public GameObject prefab;
    public Queue<GameObject> pool;
    public int poolSize;
}

public class B_PoolingManager : MonoBehaviour
{
    [SerializeField] private List<B_PoolInfo> B_pools;
    public static B_PoolingManager B_Pooling { get; private set; }
    private void Awake()
    {
        B_Pooling = this;
        Initialize();
    }

    private void Initialize()
    {
        for (int i = 0; i < B_pools.Count; i++)
        {
            var pool = B_pools[i];
            pool.pool = new Queue<GameObject>();

            for (int j = 0; j < pool.poolSize; j++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.name=pool.Name;
                obj.SetActive(false);
                obj.transform.SetParent(transform);
                pool.pool.Enqueue(obj);
            }
        }
    }
    private Bullet CreateNewObject(int order, string name)
    {
        if (name == B_pools[order].Name)
        {
            var newObj = Instantiate(B_pools[order].prefab).GetComponent<Bullet>();
            newObj.gameObject.name = name;
            newObj.gameObject.SetActive(false);
            newObj.transform.SetParent(transform);
            return newObj;
        }
        return null;
    }

    public Bullet GetObject(string name)
    {
        for (int i = 0; i < B_pools.Count; i++)
        {
            if (B_pools[i].Name != name) continue;

            if (B_pools[i].pool.Count > 0)
            {
                var obj = B_pools[i].pool.Dequeue();
                obj.transform.SetParent(null);
                obj.gameObject.SetActive(true);
                return obj.GetComponent<Bullet>();
            }
            else
            {
                var newObj = CreateNewObject(i, name);
                newObj.gameObject.SetActive(true);
                newObj.transform.SetParent(null);
                return newObj;
            }
        }
        return null;
    }
    public void ReturnObject(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(B_Pooling.transform);
        for (int i = 0; i < B_pools.Count; i++)
        {
            if (B_pools[i].Name == obj.gameObject.name)
                B_pools[i].pool.Enqueue(obj);
        }
    }
}