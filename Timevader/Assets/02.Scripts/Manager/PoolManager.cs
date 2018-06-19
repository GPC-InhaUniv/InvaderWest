using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {
    Queue<GameObject> normalPool, attackingPool, boltPool, enemyBoltPool, wreckedPool, itemPool, explosionPool;
	
    [SerializeField]
    int normalPoolSize, attackingPoolSize, boltPoolSize, enemyBoltPoolSize, wreckedPoolSize, itemPoolSize, explosionPoolSize;

    [SerializeField]
    GameObject normalPrefab, attackingPrefab, boltPrefab, enemyPrefab, wreckedPrefab, itmePrefab, explosionPrefab;

    public static PoolManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    void CreatePool()
    {
        normalPool = new Queue<GameObject>();


        attackingPool = new Queue<GameObject>();
        boltPool = new Queue<GameObject>();
        enemyBoltPool = new Queue<GameObject>();
        wreckedPool = new Queue<GameObject>();
        itemPool = new Queue<GameObject>();
        explosionPool = new Queue<GameObject>();

    }

}
