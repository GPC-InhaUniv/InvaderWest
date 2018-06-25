using System.Collections.Generic;
using UnityEngine;

public enum PoolType
{
    Enemy1Pool,
    Enemy2Pool,
    Enemy3Pool,
    BoltPool,
    EnemyBoltPool,
    Item1Pool,
    Item2Pool,
    HitEffectPool,
    ExplosionPool,
    DrainPool,
}

public class PoolController : MonoBehaviour {
    Queue<GameObject> enemy1Pool, enemy2Pool, enemy3Pool, boltPool, enemyBoltPool, item1Pool, item2Pool, hitEffectPool, explosionPool, drainPool;

    [SerializeField]
    int enemy1PoolSize, enemy2PoolSize, enemy3PoolSize, boltPoolSize, enemyBoltPoolSize, itemPoolSize, hitEffectPoolSize, explosionPoolSize, drainPoolSize;

    [SerializeField]
    GameObject enemy1Prefab, enemy2Prefab, enemy3Prefab, boltPrefab, enemyBoltPrefab, item1Prefab, item2Prefab, hitEffectPrefab, explosionPrefab, drainPrefab;

    public static PoolController instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        CreateAllPools();
    }

    void CreateAllPools()
    {
        CreatePool(ref enemy1Pool, enemy1PoolSize, enemy1Prefab);
        CreatePool(ref enemy2Pool, enemy2PoolSize, enemy2Prefab);
        CreatePool(ref enemy3Pool, enemy3PoolSize, enemy3Prefab);
        CreatePool(ref boltPool, boltPoolSize, boltPrefab);
        CreatePool(ref enemyBoltPool, enemyBoltPoolSize, enemyBoltPrefab);
        CreatePool(ref item1Pool, itemPoolSize, item1Prefab);
        CreatePool(ref item2Pool, itemPoolSize, item2Prefab);
        CreatePool(ref hitEffectPool, hitEffectPoolSize, hitEffectPrefab);
        CreatePool(ref explosionPool, explosionPoolSize, explosionPrefab);
        CreatePool(ref drainPool, drainPoolSize, drainPrefab);
    }

    void CreatePool(ref Queue<GameObject> pool, int size, GameObject prefab)
    {
        pool = new Queue<GameObject>();
        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetFromPool(PoolType type)
    {
        Queue<GameObject> pool = FindPool(type);
        if (pool.Count != 0)
        {
            Debug.Log(type.ToString() + pool.Count);
            GameObject obj = pool.Dequeue();
            if (obj != null)
            {
                obj.SetActive(true);
                return obj;
            }
            else Debug.Log("오브젝트 널");
        }
        Debug.Log("Pool에 남은 오브젝트가 부족합니다.");
        return null;
    }

    public void ReturnToPool(PoolType type, GameObject obj)
    {
        Queue<GameObject> pool = FindPool(type);
        pool.Enqueue(obj);
        obj.SetActive(false);
    }

    // type을 통해 해당 Pool을 반환하는 함수
    Queue<GameObject> FindPool(PoolType type)
    {
        switch (type)
        {
            case PoolType.Enemy1Pool:
                return enemy1Pool;
            case PoolType.Enemy2Pool:
                return enemy2Pool;
            case PoolType.Enemy3Pool:
                return enemy3Pool;
            case PoolType.BoltPool:
                return boltPool;
            case PoolType.EnemyBoltPool:
                return enemyBoltPool;
            case PoolType.Item1Pool:
                return item1Pool;
            case PoolType.Item2Pool:
                return item2Pool;
            case PoolType.HitEffectPool:
                return hitEffectPool;
            case PoolType.ExplosionPool:
                return explosionPool;
            case PoolType.DrainPool:
                return drainPool;
            default: return null;
        }
    }
}
