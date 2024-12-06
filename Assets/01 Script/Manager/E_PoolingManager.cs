using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class E_PoolInfo
{
    public GameObject prefab;
    public Queue<GameObject> pool;
    public int poolSize;
}

public class E_PoolingManager : MonoBehaviour
{
    [SerializeField] private List<E_PoolInfo> E_pools;
    public static E_PoolingManager E_Pooling { get; private set; }
    private void Awake()
    {
        E_Pooling = this;
        Initialize();
    }

    private void Initialize()
    {
        for (int i = 0; i < E_pools.Count; i++)
        {
            var pool = E_pools[i];
            pool.pool = new Queue<GameObject>();

            for (int j = 0; j < pool.poolSize; j++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.name = E_pools[i].prefab.name;
                obj.SetActive(false);
                obj.transform.SetParent(transform);
                pool.pool.Enqueue(obj);
            }
        }
    }

    private Enemy CreateNewObject(int order, string name)
    {
        if (name == E_pools[order].prefab.name)
        {
            var newObj = Instantiate(E_pools[order].prefab).GetComponent<Enemy>();
            newObj.name = E_pools[order].prefab.name;
            newObj.gameObject.SetActive(false);
            newObj.transform.SetParent(transform);
            return newObj;
        }
        return null;
    }
    public Enemy GetObject(string name)
    {
        for (int i = 0; i < E_pools.Count; i++)
        {
            if (E_pools[i].prefab.name != name) continue;

            if (E_pools[i].pool.Count > 0)
            {
                var obj = E_pools[i].pool.Dequeue();
                obj.transform.SetParent(null);
                obj.gameObject.SetActive(true);
                return obj.GetComponent<Enemy>();
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
        obj.transform.SetParent(E_Pooling.transform);
        for (int i = 0; i < E_pools.Count; i++)
        {
            if (E_pools[i].prefab.name == obj.gameObject.name)
                E_pools[i].pool.Enqueue(obj);
        }
    }
}