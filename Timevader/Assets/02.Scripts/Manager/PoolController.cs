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
    Queue<GameObject> enemy1Pool = null, enemy2Pool = null, enemy3Pool = null, boltPool = null, enemyBoltPool = null, 
        item1Pool = null, item2Pool = null, hitEffectPool = null, explosionPool = null, drainPool = null;

    [SerializeField]
    int enemy1PoolSize = 10, enemy2PoolSize = 10, enemy3PoolSize = 10, boltPoolSize = 10, enemyBoltPoolSize = 10, 
        itemPoolSize = 10, hitEffectPoolSize = 10, explosionPoolSize = 10, drainPoolSize = 10;

    [SerializeField]
    GameObject enemy1Prefab = null, enemy2Prefab = null, enemy3Prefab = null, boltPrefab = null, enemyBoltPrefab = null, 
        item1Prefab = null, item2Prefab = null, hitEffectPrefab = null, explosionPrefab = null, drainPrefab = null;

    public static PoolController instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

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
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
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
