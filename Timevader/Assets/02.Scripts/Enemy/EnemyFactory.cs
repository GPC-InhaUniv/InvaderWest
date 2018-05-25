using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour {
    Enemy model;
    ObjectPool pool;

    private void Start()
    {
        model = GameObject.FindWithTag("GameController").GetComponent<Enemy>();
        pool = GameObject.FindWithTag("GameController").GetComponent<ObjectPool>();
    }

    public Enemy GetEnemy(InvaderType type)
    {
        Enemy invader = null;
        switch(type) // new로 받아오지 않고 pool에서 받아오게 수정
        {
            case InvaderType.Normal:
                invader = new NormalEnemy(); break;
            case InvaderType.Wrecked:
                invader = new WreckedEnemy(); break;
            case InvaderType.Attacking:
                invader = new AttackingEnemy(); break;
            default: break;
        }
        return invader;
    }
}