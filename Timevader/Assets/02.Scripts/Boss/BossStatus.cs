using System.Collections;
using UnityEngine;

public class BossStatus : MonoBehaviour {
    delegate void NotifyObserver(float bossLife, float maxBossLife);

    NotifyObserver notifyLifeToObserver;
    
    public InGameController InGameController;

    public float BossHp;
    public float MaxHp;

    [SerializeField]
    GameState nowGameState;
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

        leftRightMover = GetComponent<LeftRightMover>();

        BossHp = MaxHp;
        ScoreValue = 10;

        isdead = false;

        //게임컨트롤러에게 알리기 보스가 맞았다고//
        notifyLifeToObserver = new NotifyObserver(InGameController.UpdateBossLife);
        GamePlayManager.OnChangeGamestate += CheckGameState;

    }

    void FixedUpdate()
    {
        if(nowGameState == GameState.Ready)
        {
            AppearBoss();
        }
    }

    void AppearBoss() //보스가 서서히 등장
    {
        if(transform.position.y > 4.0f)
        {
            transform.Translate(new Vector3(0, -0.07f, 0));
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
            GameObject explosion = PoolController.instance.GetFromPool(PoolType.HitEffectPool);
            if(explosion != null) explosion.transform.position = transform.position;
            //explosion.SetActive(true);
            BossHp -= 1;

            //게임컨트롤러에게 알리기 보스가 맞았다고//
            if (notifyLifeToObserver != null)
                notifyLifeToObserver(BossHp, MaxHp);

            if (BossHp == 0)
            {
                explosion = PoolController.instance.GetFromPool(PoolType.ExplosionPool);
                //explosion.GetComponent<Particle>()
                explosion.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f); // 이펙트 크기를 4배로
                //explosion.SetActive(true);
                GamePlayManager.Instance.ChangeGameState();
                Destroy(gameObject);
                isdead = true;
            }
        }
    }
    public void CheckGameState()
    {
        nowGameState = GamePlayManager.Instance.NowGameState;
    }
}
