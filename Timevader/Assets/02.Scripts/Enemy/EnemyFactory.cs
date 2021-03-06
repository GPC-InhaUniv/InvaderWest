﻿using UnityEngine;

public class EnemyFactory : MonoBehaviour {
    public static EnemyFactory instance = null;

    public GameObject GetEnemy(InvaderType type)
    {
        GameObject invader = null;
        switch(type)
        {
            case InvaderType.Enemy1:
                invader = PoolController.instance.GetFromPool(PoolType.Enemy1Pool); break;
            case InvaderType.Enemy2:
                invader = PoolController.instance.GetFromPool(PoolType.Enemy2Pool); break;
            case InvaderType.Enemy3:
                invader = PoolController.instance.GetFromPool(PoolType.Enemy3Pool); break;
            default: break;
        }
        return invader;
    }
}