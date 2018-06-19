using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStatus : MonoBehaviour {
    //윤우//
    delegate void NotifyObserver(float bossLife, float maxBossLife);

    NotifyObserver notifyLifeToObserver;
    public InGameController inGameController;

    [SerializeField]
    PlayerShip playerShip;
    GameState nowGameState;
    //윤우//

    public float BossHp;
    public float MaxHp;

    [SerializeField]
    int decreaseHp, ScoreValue;

    [SerializeField]
    bool isdead;

    [SerializeField]
    GameObject explosion;

    [SerializeField]
    float moveSpeed = 3.5f;

    int moveVlaue = 1;

    float leftLimitX;
    float rightLimitX;


   

    void Start()
    {
        
        playerShip = GameObject.FindWithTag("Player").GetComponent<PlayerShip>();
        nowGameState = playerShip.NowGameState;

        leftLimitX = -2.5f;
        rightLimitX = 2.5f;

        BossHp = 100;
        MaxHp = 100;
        decreaseHp = 15;
        ScoreValue = 10;

        isdead = false;

        //게임컨트롤러에게 알리기 보스가 맞았다고//
        notifyLifeToObserver = new NotifyObserver(inGameController.UpdateBossLife);
        //if (notifyLifeToObserver != null)
        //    notifyLifeToObserver(BossHp, MaxHp);
        Debug.Log(BossHp / MaxHp);

    }

    void FixedUpdate()
    {
        if (nowGameState == GameState.Started)
        {
            BossMove();
        }
    }
    private void Update()
    {
        nowGameState = playerShip.NowGameState;
    }

    void OnTriggerEnter(Collider other)
    {
        if (nowGameState == GameState.Started)
        {
            if (other.tag == "Enemy")
            {
                return;
            }
            else if (other.tag == "Bolt")
            {
                Instantiate(explosion, transform.position, transform.rotation); //오브젝트 풀로 수정 예정

                BossHp -= 10;
                DestroyObject(other.gameObject);

                //게임컨트롤러에게 알리기 보스가 맞았다고//
                if (notifyLifeToObserver != null)
                    notifyLifeToObserver(BossHp, MaxHp);

                if (BossHp == 0)
                {
                    Destroy(gameObject);

                    isdead = true;
                }
            }
        }
    }

    void BossMove()
    {
        if (transform.localPosition.x < leftLimitX)
        {
            moveVlaue = -1;
        }
        else if (transform.localPosition.x > rightLimitX)
        {
            moveVlaue = 1;
        }
        transform.Translate(Vector3.left * moveVlaue * moveSpeed * Time.deltaTime); //지용님 수정사항 반영
    }
}
