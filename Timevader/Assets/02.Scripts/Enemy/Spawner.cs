using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public Transform[] SpawnPoint; // Enemy가 소환될 좌표에 위치한 EmptyObject
    public int SpawnCount = 3;
    public int StageLevel = 1;

    public EnemyFactory factory;

    void Start()
    {
        factory = FindObjectOfType<EnemyFactory>();
        StartSpawn();
    }

    void StartSpawn()
    {
        switch (StageLevel)
        {
            case 1: StartCoroutine("Stage1"); break;
            case 2: StartCoroutine("Stage2"); break;
            case 3: StartCoroutine("Stage3"); break;
        }
    }

    void SpawnEnemy(InvaderType type, Vector3 spawnPoint, Direction moveDirection)
    {
        GameObject enemy = factory.GetEnemy(type);
        enemy.transform.position = spawnPoint;
        enemy.transform.rotation = Quaternion.identity;
        enemy.SetActive(true);

        if (enemy.GetComponent<NormalEnemy>())
        {
            //Debug.Log("NormalEnemy 스크립트");
            enemy.GetComponent<NormalEnemy>().MoveDirection = moveDirection;
        }
        else if (enemy.GetComponent<AttackingEnemy>())
        {
            //Debug.Log("AttackingEnemy 스크립트");
            enemy.GetComponent<AttackingEnemy>().MoveDirection = moveDirection;
            enemy.GetComponent<AttackingEnemy>().StartCoroutine("Attack");
        }
        else Debug.Log("enemy 스크립트를 찾을 수 없습니다.");
    }

    private IEnumerator Stage1()
    {
        
        for (int i = 0; i < SpawnCount; i++)
        {
            SpawnEnemy(InvaderType.Attacking, SpawnPoint[0].position, Direction.Zigzag_LeftToRight);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.7f);

        for (int i = 0; i < SpawnCount; i++)
        {
            SpawnEnemy(InvaderType.Attacking, SpawnPoint[1].position, Direction.Zigzag_RightToLeft);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < SpawnCount; i++)
        {
            SpawnEnemy(InvaderType.Attacking, SpawnPoint[2].position, Direction.Circle_Clockwise);
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(2.0f);
        StartSpawn();
    }
    private IEnumerator Stage2()
    {
        yield return null;
    }
    private IEnumerator Stage3()
    {
        yield return null;
    }
}
