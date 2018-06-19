using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour {
    public ObjectPool normalPool, AttackingPool;
    
    public GameObject GetEnemy(InvaderType type)
    {
        GameObject invader = null;
        switch(type)
        {
            case InvaderType.Normal:
                invader = normalPool.GetFromPool(); break;
            case InvaderType.Attacking:
                invader = AttackingPool.GetFromPool(); break;
            default: break;
        }
        return invader;
    }
}