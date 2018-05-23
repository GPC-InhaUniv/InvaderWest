using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Line,
    Circle, // 특정 좌표를 기준으로 회전
    Zigzag, // 지그재그
    //diagonal Line // 대각선
}

public class Enemy : MonoBehaviour {

    public GameObject Invader;

    EnemyModel enemyBase;

	public Enemy()
    {
        enemyBase = new EnemyModel();
    }

    //public Enemy InvaderFactory(Direction dir, Vector3 destination)
    //{
    //    GameObject newObj = Instantiate(Invader);
    //}
    
}
