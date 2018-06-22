using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniP : MonoBehaviour
{
    [SerializeField]
    GameState nowGameState;
    [SerializeField]
    GameObject player;


    public GameObject Shot;
    public Transform ShotSpawn;


    public float fireDelta = 0.35f;

    float nextFire = 0.35f;
    float myTime = 0.0f;
    float rotateSpeed = 5.0f;

    Rigidbody rigid;

    void Start()
    {
        StartCoroutine("checkGameState");
        player = GameObject.FindWithTag("Player");

    }
    void FixedUpdate()
    {
        if (nowGameState == GameState.Started)
        {
            //고정//
            gameObject.transform.Rotate(new Vector3(0f, player.transform.position.y, 0f), rotateSpeed);

            Shoot(ShotSpawn);
        }
    }

    IEnumerator checkGameState()
    {
        nowGameState = GamePlayManager.Instance.NowGameState;
        yield return new WaitForSeconds(1.0f);
        StartCoroutine("checkGameState");
    }

    void Shoot(Transform AnySpawn)
    {
        myTime = myTime + Time.deltaTime;

        if (myTime > nextFire)
        {
            nextFire = myTime + fireDelta;
            //GameObject shot = PoolController.instance.GetFromPool(PoolType.BoltPool);
            

            nextFire = nextFire - myTime;
            myTime = 0.0f;
        }
    }
}
