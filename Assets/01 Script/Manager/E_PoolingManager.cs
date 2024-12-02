using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class E_PoolInfo
{
    public string Name;
    public GameObject prefab;
    public Queue<GameObject> pool;
    public int poolSize;
}

public class E_PoolingManager : MonoBehaviour
{
    [SerializeField] private List<E_PoolInfo> pools;
    public static E_PoolingManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        Initialize();
    }

    private void Initialize()
    {
        for (int i = 0; i < pools.Count; i++)
        { 
            var pool = pools[i];
            pool.pool = new Queue<GameObject>();

            for (int j = 0; j < pool.poolSize; j++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);

                pool.pool.Enqueue(obj);
            }
        }
    }

    private Enemy CreateNewObject(int order, string name)
    {
            if(name == pools[order].Name)
            {
                var newObj = Instantiate(pools[order].prefab).GetComponent<Enemy>();
                newObj.gameObject.SetActive(false);
                newObj.transform.SetParent(transform);
                return newObj;
            }
        return null;
    }
    public Enemy GetObject(string name)
    {
        for (int i = 0; i < pools.Count; i++)
        {
            if (pools[i].Name != name) continue;

            if (pools[i].pool.Count > 0)
            {
                var obj = pools[i].pool.Dequeue();
                obj.transform.SetParent(null);
                obj.gameObject.SetActive(true);
                return obj.GetComponent<Enemy>();
            }
            else
            {
                var newObj = CreateNewObject(i,name);
                newObj.gameObject.SetActive(true);
                newObj.transform.SetParent(null);
                return newObj;
            }
        }
        return null;
    }
    public static void ReturnObject(Enemy obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        pools[obj.name].pool.Enqueue(obj);
    }
}