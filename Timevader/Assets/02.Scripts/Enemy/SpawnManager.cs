using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    public Transform[] SpawnPoint; // Enemy가 소환될 좌표에 위치한 EmptyObject
    public int SpawnCount = 3;
    public int StageLevel = 1;

    public EnemyFactory factory;

    void Start()
    {
        factory = FindObjectOfType<EnemyFactory>();
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
        if (enemy.GetComponent<NormalEnemy>())
        {
            Debug.Log("NormalEnemy 스크립트");
            enemy.GetComponent<NormalEnemy>().MoveDirection = moveDirection;
        }
        else if (enemy.GetComponent<AttackingEnemy>())
        {
            Debug.Log("AttackingEnemy 스크립트");
            enemy.GetComponent<AttackingEnemy>().MoveDirection = moveDirection;
        }
        else Debug.Log("enemy 스크립트를 찾을 수 없습니다.");

        enemy.transform.position = spawnPoint;
        enemy.transform.rotation = Quaternion.identity;
        enemy.SetActive(true);
    }

    private IEnumerator Stage1()
    {
        //yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < SpawnCount; i++)
        {
            SpawnEnemy(InvaderType.Normal, SpawnPoint[0].position, Direction.Line_LeftToRight);
        }
        yield return new WaitForSeconds(3.0f);

        for (int i = 0; i < SpawnCount; i++)
        {
            SpawnEnemy(InvaderType.Attacking, SpawnPoint[1].position, Direction.Line_RightToLeft);
        }
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
