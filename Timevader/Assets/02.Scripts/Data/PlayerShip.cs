using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    Ready,
    Start,
    GameOver
}

public interface ISubjectable
{
    void RegisterObserver(IObserverable o);
    void RemoveObserver(IObserverable o);
    void NotifyPlayerLifeToObservers();
    void NotifyPlayerRestTimeToObservers();
}
public interface IObserverable
{
    void UpdatePlayerLife(int playerLife);
    void GetPlayerLife(int playerLife);
    void UpdatePlayerRestTime(int playerRestTime);
}
public interface IDisplayable
{
    void DisPlayPlayerLife();
    void DisplayPlayerRestTime();
}

/// <summary>
/// 설님꺼 합치기
/// </summary>
[System.Serializable]
public class Boundary1
{
    public float xMin, xMax, yMin, yMax;
}


public class PlayerShip : MonoBehaviour , ISubjectable
{
    [SerializeField]
    private int playerLife;
    [SerializeField]
    private int playerRestTime;
    [SerializeField]
    private int playerfirerapid;
    [SerializeField]
    private int addMissileitem;
    public GameObject AddMissileItem;
    [SerializeField]
    private int assistantitem;
    private bool assistant;
    [SerializeField]
    private int lastBombitem;


    List<IObserverable> observerList = new List<IObserverable>();

    GameState NowGameState;



    /// <summary>
    /// 설님꺼
    /// </summary>
    [SerializeField]
    private float speed;

    public Boundary Boundary;

    public GameObject Shot;
    public Transform ShotSpawn;
    public Transform AddedSpawn;

    public float fireDelta = 0.2f;
    [SerializeField]
    private float nextFire = 0.2f;
    private float myTime = 0.0f;

    Rigidbody rigid;
    Vector3 movement;



    void Shoot(Transform AnySpawn)
    {
        myTime = myTime + Time.deltaTime;

        if (myTime > nextFire)
        {
            nextFire = myTime + fireDelta;
            Instantiate(Shot, AnySpawn.position, AnySpawn.rotation);

            nextFire = nextFire - myTime;
            myTime = 0.0f;
        }
    }
    /// <summary>
    /// 설님꺼
    /// </summary>

    void Start()
    {
        ///설님꺼
        rigid = GetComponent<Rigidbody>();
        ///설님꺼
        NowGameState = GameState.Ready;

        Invoke("GameStart", 2.0f);

        //addMissileitem = int.Parse(AccountInfo.Instance.AddMissileitem);
        //assistantitem = int.Parse(AccountInfo.Instance.Assistantitem);
        //lastBombitem = int.Parse(AccountInfo.Instance.LastBombitem);
       fireDelta = 0.7f;

    GamePlayManager.Instance.PlayerShipNum = 2;


        if (GamePlayManager.Instance.PlayerShipNum == 1)
        {
            playerLife = 3;
            playerRestTime = 5000;

        }
        if (GamePlayManager.Instance.PlayerShipNum == 2)
        {
            playerLife = 3;
            playerRestTime = 5000;

        }

        if (addMissileitem == 1)
        {
            UseAddMissileitem();
            Debug.Log("UseAddMissileitem");

        }
        if (assistantitem == 1)
        {
            UseAssistantitem();
            Debug.Log("UseAssistantitem");
        }
        StartCoroutine("LoseTime");

        NotifyStartDataToObservers();

    }

    void GameStart()
    {
        NowGameState = GameState.Start;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Factory"))
        {
            if (assistant == true)
            {
                assistant = false;
                Debug.Log(assistant);
            }
            else
            {
                playerLife = playerLife - 1;
                if (AddMissileItem.activeSelf == true)
                {
                    AddMissileItem.gameObject.SetActive(false);
                }
                NotifyPlayerLifeToObservers();

            }
        }
    }


    private void Update()
    {
        if (NowGameState == GameState.Start)
        {
            ///설님꺼
            Shoot(ShotSpawn);
            ///설님꺼

            if (lastBombitem == 1)
            {
                UseLastBombitem();
                Debug.Log("UseLastBombitem");
            }
        }
        if (playerLife <= 0)
        {
            playerLife = 0;
            NowGameState = GameState.GameOver;
        }

    }
    private void FixedUpdate()
    {
        if (NowGameState == GameState.Start)
        {
            {
                float moveHorizontal = Input.GetAxis("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");

                movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
                rigid.velocity = movement * speed;

                rigid.position = new Vector3
                (
                    Mathf.Clamp(rigid.position.x, Boundary.xMin, Boundary.xMax),
                    Mathf.Clamp(rigid.position.y, Boundary.yMin, Boundary.yMax),
                    0.0f
                );
            }
        }

    }

    public void NotifyStartDataToObservers()
    {
        for (int i = 0; i < observerList.Count; i++)
        {
            observerList[i].GetPlayerLife(playerLife);
        }
    }

    public void NotifyPlayerLifeToObservers()
    {
        for (int i = 0; i < observerList.Count; i++)
        {
            observerList[i].UpdatePlayerLife(playerLife);
        }

    }
    public void NotifyPlayerRestTimeToObservers()
    {
        for (int i = 0; i < observerList.Count; i++)
        {
            observerList[i].UpdatePlayerRestTime(playerRestTime);
        }
    }
    

    public void RegisterObserver(IObserverable o)
    {
        observerList.Add(o);           
    }
    public void RemoveObserver(IObserverable o)
    {
        observerList.Remove(o);
    }


    private IEnumerator LoseTime()
    {
        playerRestTime = playerRestTime - 10;
        NotifyPlayerRestTimeToObservers();

        yield return new WaitForSeconds(1.5f);

        StartCoroutine("LoseTime");
    }

    //미사일 아이템 사용
    void UseAddMissileitem()
    {
        AddMissileItem.gameObject.SetActive(true);
        AccountInfo.ChangeAddMissileitemData(0);

    }
    void UseAssistantitem()
    {
        assistant = true;
        AccountInfo.ChangeAssistantitemData(0);

    }
    void UseLastBombitem()
    {
        AccountInfo.ChangeLastBombitemData(0);

    }
}
