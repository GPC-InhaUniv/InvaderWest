using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniP : MonoBehaviour
{
    [SerializeField]
    PlayerShip playerShip;
    GameState nowGameState;

    public GameObject Player;


    public GameObject Shot;
    public Transform ShotSpawn;

    public GameObject RotationAxis;

    public float fireDelta = 0.35f;

    float nextFire = 0.35f;
    float myTime = 0.0f;
    float rotateSpeed = 5.0f;

    Rigidbody rigid;

    void Start()
    {
        playerShip = GameObject.FindWithTag("Player").GetComponent<PlayerShip>();
        nowGameState = playerShip.NowGameState;
    }

    // Update is called once per frame
    void Update()
    {
        nowGameState = playerShip.NowGameState;

        if (nowGameState == GameState.Started)
        {
            //고정//
            RotationAxis.transform.Rotate(new Vector3(0f, Player.transform.position.y, 0f), rotateSpeed);

            Shoot(ShotSpawn);
        }

    }

    void Shoot(Transform AnySpawn)
    {
        myTime = myTime + Time.deltaTime;

        if (myTime > nextFire)
        {
            nextFire = myTime + fireDelta;
            Instantiate(Shot, new Vector3(AnySpawn.position.x, AnySpawn.position.y, 0.0f), Quaternion.identity);

            nextFire = nextFire - myTime;
            myTime = 0.0f;
        }
    }
}
