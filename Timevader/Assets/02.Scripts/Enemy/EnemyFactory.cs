using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour {
    //public ObjectPool normalPool, AttackingPool;
    
    public GameObject GetEnemy(InvaderType type)
    {
        GameObject invader = null;
        switch(type)
        {
            case InvaderType.Normal:
                invader = PoolController.instance.GetFromPool(PoolType.NormalPool); break;
            case InvaderType.Attacking:
                invader = PoolController.instance.GetFromPool(PoolType.AttackingPool); break;
            default: break;
        }
        return invader;
    }
}