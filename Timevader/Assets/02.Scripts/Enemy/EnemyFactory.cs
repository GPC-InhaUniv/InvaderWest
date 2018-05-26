using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour {
    public ObjectPool normalPool, AttackingPool;

    public GameObject GetEnemy(InvaderType type)
    {
        if (normalPool == null) return null;
        GameObject invader = null;
        switch(type) // new로 받아오지 않고 pool에서 받아오게 수정
        {
            case InvaderType.Normal:
                Debug.Log("normal 생성");
                invader = normalPool.GetFromPool(); break;
            case InvaderType.Attacking:
                invader = AttackingPool.GetFromPool(); break;
            default: break;
        }
        return invader;
    }
}