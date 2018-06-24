﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField]
    Transform[] spawnPoint; // Enemy가 소환될 좌표에 위치한 EmptyObject

    [SerializeField]
    EnemyFactory factory;

    [SerializeField]
    int spawnCount = 5;

    int stageLevel = 1;
    Random random;

    [SerializeField]
    float SPAWNDELAY = 0.3f, WAVEDELAY = 0.5f;
    
    void Start()
    {
        factory = FindObjectOfType<EnemyFactory>();
    }

    public void StartSpawn()
    {
        switch (stageLevel)
        {
            case 1: StartCoroutine("Stage3"); break;
            case 2: StartCoroutine("Stage3"); break;
            case 3: StartCoroutine("Stage3"); break;
        }
    }

    void SetSpawn(Enemy e, Direction direction)
    {
        e.SetDirection(direction);

        if (e is AttackingEnemy)
        {
            AttackingEnemy ae = e as AttackingEnemy;
            ae.StartCoroutine("Attack");
        }
    }

    void SpawnEnemy(InvaderType type, Vector3 spawnPoint, Direction moveDirection)
    {
        GameObject enemy = factory.GetEnemy(type);
        if (!enemy) // pool이 비었을 때는 실행하지 않음
        {
            Debug.Log("enemy를 받아오지 못했습니다.");
            return;
        }

        Enemy e = enemy.GetComponent<Enemy>();
        if (!e)
        {
            Debug.Log("enemy 스크립트를 찾을 수 없습니다.");
            return;
        }

        enemy.transform.position = spawnPoint;
        SetSpawn(e, moveDirection);
    }

    IEnumerator Stage1()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnEnemy(InvaderType.Normal, spawnPoint[1].position, Direction.Zigzag_LeftToRight);
            yield return new WaitForSeconds(SPAWNDELAY);
        }
        yield return new WaitForSeconds(WAVEDELAY * 2);

        for (int i = 0; i < spawnCount; i++)
        {
            SpawnEnemy(InvaderType.Normal, spawnPoint[2].position, Direction.Curve_LeftDown);
            SpawnEnemy(InvaderType.Normal, spawnPoint[3].position, Direction.Curve_LeftDown);
            yield return new WaitForSeconds(SPAWNDELAY);
        }

        for (int i = 0; i < spawnCount; i++)
        {
            SpawnEnemy(InvaderType.Normal, spawnPoint[3].position, Direction.Zigzag_RightToLeft);
            yield return new WaitForSeconds(SPAWNDELAY);
        }
        yield return new WaitForSeconds(WAVEDELAY * 2);

        for (int i = 0; i < spawnCount; i++)
        {
            SpawnEnemy(InvaderType.Attacking, spawnPoint[0].position, Direction.Line_LeftToRight);
            SpawnEnemy(InvaderType.Normal, spawnPoint[3].position, Direction.Line_RightToLeft);
            yield return new WaitForSeconds(SPAWNDELAY);
        }
        yield return new WaitForSeconds(WAVEDELAY * 2);

        for (int i = 0; i < spawnCount; i++)
        {
            SpawnEnemy(InvaderType.Attacking, spawnPoint[1].position, Direction.Curve_LeftDown);
            yield return new WaitForSeconds(SPAWNDELAY);
        }
        yield return new WaitForSeconds(WAVEDELAY * 2);

        for (int i = 0; i < spawnCount; i++)
        {
            SpawnEnemy(InvaderType.Normal, spawnPoint[0].position, Direction.Curve_RightDown);
            SpawnEnemy(InvaderType.Normal, spawnPoint[1].position, Direction.Curve_RightDown);
            yield return new WaitForSeconds(SPAWNDELAY);
        }
        yield return new WaitForSeconds(WAVEDELAY * 2);
        
        StartSpawn();
    }
    IEnumerator Stage2()
    {
        yield return null;

        StartSpawn();
    }
    IEnumerator Stage3()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnEnemy((InvaderType)Random.Range(0, 2), spawnPoint[4].position, Direction.Circle_Clockwise);
            yield return new WaitForSeconds(SPAWNDELAY);
        }
        yield return new WaitForSeconds(WAVEDELAY * 2);

        for (int i = 0; i < spawnCount; i++)
        {
            SpawnEnemy((InvaderType)Random.Range(0, 2), spawnPoint[0].position, Direction.Line_LeftToRight);
            SpawnEnemy((InvaderType)Random.Range(0, 2), spawnPoint[3].position, Direction.Line_RightToLeft);
            yield return new WaitForSeconds(SPAWNDELAY);
        }
        yield return new WaitForSeconds(WAVEDELAY * 2);

        for (int i = 0; i < spawnCount; i++)
        {
            SpawnEnemy(InvaderType.Attacking, spawnPoint[1].position, Direction.Curve_RightDown);
            SpawnEnemy(InvaderType.Attacking, spawnPoint[3].position, Direction.Curve_LeftDown);
            yield return new WaitForSeconds(SPAWNDELAY);
        }
        yield return new WaitForSeconds(WAVEDELAY * 2);

        for (int i = 0; i < spawnCount; i++)
        {
            SpawnEnemy(InvaderType.Normal, spawnPoint[5].position, Direction.Circle_Clockwise);
            SpawnEnemy(InvaderType.Normal, spawnPoint[2].position, Direction.Zigzag_RightToLeft);
            yield return new WaitForSeconds(SPAWNDELAY);
        }
        yield return new WaitForSeconds(WAVEDELAY * 2);

        for (int i = 0; i < spawnCount; i++)
        {
            SpawnEnemy((InvaderType)Random.Range(0, 2), spawnPoint[4].position, Direction.Circle_CounterClockwise);
            yield return new WaitForSeconds(SPAWNDELAY);
        }
        yield return new WaitForSeconds(WAVEDELAY * 2);

        for (int i = 0; i < spawnCount; i++)
        {
            SpawnEnemy((InvaderType)Random.Range(0, 2), spawnPoint[5].position, Direction.Circle_CounterClockwise);
            SpawnEnemy(InvaderType.Attacking, spawnPoint[0].position, Direction.Zigzag_LeftToRight);
            yield return new WaitForSeconds(SPAWNDELAY);
        }
        yield return new WaitForSeconds(WAVEDELAY * 2);

        StartSpawn();
    }
}
