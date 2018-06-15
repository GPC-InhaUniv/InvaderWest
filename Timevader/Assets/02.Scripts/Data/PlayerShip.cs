using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Ready,
    Start,
    GameOver
}
/// <summary>
/// 설님꺼 합치기
/// </summary>
[System.Serializable]
public class Boundary1
{
    public float xMin, xMax, yMin, yMax;
}


public class PlayerShip : MonoBehaviour
{

    public delegate void NotifyObserver(int PlayerLife);
    NotifyObserver notifyLifetoObserver;
    NotifyObserver notifyRestTimeObserver;
    public InGameController inGameController;


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



    GameState nowGameState;



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
        nowGameState = GameState.Ready;

        Invoke("GameStart", 2.0f);

        //addMissileitem = int.Parse(AccountInfo.Instance.AddMissileitem);
        //assistantitem = int.Parse(AccountInfo.Instance.Assistantitem);
        //lastBombitem = int.Parse(AccountInfo.Instance.LastBombitem);
        //inGameController = new InGameController();


        GamePlayManager.Instance.PlayerShipNum = 1;


        if (GamePlayManager.Instance.PlayerShipNum == 1)
        {
            playerLife = 3;
            playerRestTime = 5000;
        }
        else if (GamePlayManager.Instance.PlayerShipNum == 2)
        {
            playerLife = 4;
            playerRestTime = 3800;
        }

        if (addMissileitem == (int)DataBoolean.TRUE)
        {
            UseAddMissileitem();
            Debug.Log("UseAddMissileitem");
        }
        if (assistantitem == (int)DataBoolean.TRUE)
        {
            UseAssistantitem();
            Debug.Log("UseAssistantitem");
        }

        //NotifyStartDataToObservers();


        notifyLifetoObserver = new NotifyObserver(inGameController.UpdatePlayerLife);
        if (notifyLifetoObserver != null)
            notifyLifetoObserver(playerLife);
        notifyRestTimeObserver = new NotifyObserver(inGameController.UpdatePlayerRestTime);
        if (notifyRestTimeObserver != null)
            notifyRestTimeObserver(playerRestTime);

        StartCoroutine("LoseTime");

    }

    void GameStart()
    {
        nowGameState = GameState.Start;
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
                //NotifyPlayerLifeToObservers();

                notifyLifetoObserver(playerLife);
            }
        }
    }
    private void Update()
    {
        if (nowGameState == GameState.Start)
        {
            ///설님꺼
            Shoot(ShotSpawn);
            ///설님꺼
            if (lastBombitem == (int)DataBoolean.TRUE)
            {
                UseLastBombitem();
                Debug.Log("UseLastBombitem");
            }
        }
        if (playerLife <= 0)
        {
            playerLife = 0;
            nowGameState = GameState.GameOver;
        }
    }
    private void FixedUpdate()
    {
        if (nowGameState == GameState.Start)
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
    private IEnumerator LoseTime()
    {
        playerRestTime = playerRestTime - 10;
        //NotifyPlayerRestTimeToObservers();
        notifyRestTimeObserver(playerRestTime);

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

    ///observer Ex///
    //public interface ISubjectable
    //{
    //    void RegisterObserver(IObserverable o);
    //    void RemoveObserver(IObserverable o);
    //    void NotifyPlayerLifeToObservers();
    //    void NotifyPlayerRestTimeToObservers();
    //}
    //public interface IObserverable
    //{
    //    void UpdatePlayerLife(int playerLife);
    //    void GetPlayerLife(int playerLife);
    //    void UpdatePlayerRestTime(int playerRestTime);
    //}
    //public interface IDisplayable
    //{
    //    void DisPlayPlayerLife();
    //    void DisplayPlayerRestTime();
    //}
    /////////////////////////////////////////////////
    //public void NotifyStartDataToObservers()
    //{
    //    for (int i = 0; i < observerList.Count; i++)
    //    {
    //        observerList[i].GetPlayerLife(playerLife);
    //    }
    //}
    //public void NotifyPlayerLifeToObservers()
    //{
    //    for (int i = 0; i < observerList.Count; i++)
    //    {
    //        observerList[i].UpdatePlayerLife(playerLife);
    //    }
    //}
    //public void NotifyPlayerRestTimeToObservers()
    //{
    //    for (int i = 0; i < observerList.Count; i++)
    //    {
    //        observerList[i].UpdatePlayerRestTime(playerRestTime);
    //    }
    //}
    //public void RegisterObserver(IObserverable o)
    //{
    //    observerList.Add(o);           
    //}
    //public void RemoveObserver(IObserverable o)
    //{
    //    observerList.Remove(o);
    //}

