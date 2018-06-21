using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStatus : MonoBehaviour {
    //윤우//
    delegate void NotifyObserver(float bossLife, float maxBossLife);

    NotifyObserver notifyLifeToObserver;
    public InGameController InGameController;

    [SerializeField]
    GameState nowGameState;
    //윤우//

    public float BossHp;

    public float MaxHp; //정해야함

    [SerializeField]
    int decreaseHp, ScoreValue;

    [SerializeField]
    bool isdead;

    [SerializeField]
    GameObject explosion;

    int moveVlaue = 1;

    float leftLimitX, rightLimitX;

    LeftRightMover leftRightMover;

    void Start()
    {
        StartCoroutine("checkGameState");


        leftRightMover = GetComponent<LeftRightMover>();

        
        BossHp = MaxHp;

   
        ScoreValue = 10;

        isdead = false;

        //게임컨트롤러에게 알리기 보스가 맞았다고//
        notifyLifeToObserver = new NotifyObserver(InGameController.UpdateBossLife);
        //if (notifyLifeToObserver != null)
        //    notifyLifeToObserver(BossHp, MaxHp);
        Debug.Log(BossHp / MaxHp);
    }

    void FixedUpdate()
    {
        if(nowGameState == GameState.Ready)
        {
            AppearBoss();
        }

        if (nowGameState == GameState.Started)
        {
            //leftRightMover.LeftRightMove(); LeftRightMover로 이동
        }
    }
    void AppearBoss() //보스가 서서히 등장
    {
        if(transform.position.y > 4.0f)
        {
            transform.Translate(new Vector3(0, -0.05f, 0));
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {
            return;
        }
        else if (other.tag == "Bolt")
        {
            //Instantiate(explosion, transform.position, transform.rotation); //오브젝트 풀로 수정 예정

            BossHp -= 1;
            //DestroyObject(other.gameObject);

            //게임컨트롤러에게 알리기 보스가 맞았다고//
            if (notifyLifeToObserver != null)
                notifyLifeToObserver(BossHp, MaxHp);

            if (BossHp == 0)
            {
                GamePlayManager.Instance.NowGameState = GameState.Win;
                Destroy(gameObject);
                isdead = true;
            }
        }
    }

    IEnumerator checkGameState()
    {
        nowGameState = GamePlayManager.Instance.NowGameState;
        Debug.Log(nowGameState);
        yield return new WaitForSeconds(1.0f);
        StartCoroutine("checkGameState");
    }
}
