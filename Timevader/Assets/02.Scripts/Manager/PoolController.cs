using System.Collections.Generic;
using UnityEngine;

public enum PoolType
{
    NormalPool,
    AttackingPool,
    BoltPool,
    EnemyBoltPool,
    ItemPool,
    HitEffectPool,
    ExplosionPool,
    DrainPool,
}

public class PoolController : MonoBehaviour {
    Queue<GameObject> normalPool, attackingPool, boltPool, enemyBoltPool, itemPool, hitEffectPool, explosionPool, drainPool;

    [SerializeField]
    int normalPoolSize, attackingPoolSize, boltPoolSize, enemyBoltPoolSize, itemPoolSize, hitEffectPoolSize, explosionPoolSize, drainPoolSize;

    [SerializeField]
    GameObject normalPrefab, attackingPrefab, boltPrefab, enemyBoltPrefab, itemPrefab, hitEffectPrefab, explosionPrefab, drainPrefab;

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
        CreatePool(ref normalPool, normalPoolSize, normalPrefab);
        CreatePool(ref attackingPool, attackingPoolSize, attackingPrefab);
        CreatePool(ref boltPool, boltPoolSize, boltPrefab);
        CreatePool(ref enemyBoltPool, enemyBoltPoolSize, enemyBoltPrefab);
        CreatePool(ref itemPool, itemPoolSize, itemPrefab);
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
            case PoolType.NormalPool:
                return normalPool;
            case PoolType.AttackingPool:
                return attackingPool;
            case PoolType.BoltPool:
                return boltPool;
            case PoolType.EnemyBoltPool:
                return enemyBoltPool;
            case PoolType.ItemPool:
                return itemPool;
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
