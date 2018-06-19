using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Ready,
    Started,
    GameOver
}
[System.Serializable]
public class Boundary1
{
    public float xMin, xMax, yMin, yMax;
}
public class PlayerShip : MonoBehaviour
{
    delegate void NotifyObserver(int playerLife);

    NotifyObserver notifyLifeToObserver;
    NotifyObserver notifyRestTimeObserver;
    public InGameController inGameController;

    [SerializeField]
    int playerLife;
    [SerializeField]
    int playerRestTime;
    [SerializeField]
    int playerfirerapid;
    [SerializeField]
    int addMissileItem;

    public GameObject AddMissileItem;

    [SerializeField]
    int assistantItem;
    bool assistant;
    [SerializeField]
    int lastBombItem;

    public GameState NowGameState;


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

    void Start()
    {
        rigid = GetComponent<Rigidbody>();

        NowGameState = GameState.Ready;

        Invoke("GameStart", 2.0f);

        //Test 끝나면 주석제거하기//
        //addMissileItem = int.Parse(AccountInfo.Instance.AddMissileItem);
        //assistantItem = int.Parse(AccountInfo.Instance.AssistantItem);
        //lastBombItem = int.Parse(AccountInfo.Instance.LastBombItem);


        //GamePlayManager.Instance.PlayerShipNum = 1;

        //Test 플레이어//
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
        //Test//
        if (addMissileItem == (int)DataBoolean.TRUE || GamePlayManager.Instance.PlayerShipNum == 1)
        {
            UseAddMissileItem();
            Debug.Log("UseAddMissileItem");
        }
        if (assistantItem == (int)DataBoolean.TRUE)
        {
            UseAssistantItem();
            Debug.Log("UseAssistantItem");
        }

        //Delegate 사용해서 InGameController에 Life,RestTime 이미지 갱신//
        notifyLifeToObserver = new NotifyObserver(inGameController.UpdatePlayerLife);
        if (notifyLifeToObserver != null)
            notifyLifeToObserver(playerLife);
        notifyRestTimeObserver = new NotifyObserver(inGameController.UpdatePlayerRestTime);


        //시간 빼앗기 시작//
        StartCoroutine("LoseTime");
    }
    //게임시작//
    void GameStart()
    {
        NowGameState = GameState.Started;
    }

    void OnTriggerEnter(Collider other)
    {
        // Test Tag를 Enemy로 지정//
        if (other.gameObject.CompareTag("Enemy"))
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
                if (notifyLifeToObserver != null)
                    notifyLifeToObserver(playerLife);
            }
        }
    }
    void Update()
    {
        if (NowGameState == GameState.Started)
        {
            ///설님꺼
            Shoot(ShotSpawn);
            ///설님꺼
            if (lastBombItem == (int)DataBoolean.TRUE)
            {
                UseLastBombItem();
                Debug.Log("UseLastBombItem");
            }
        }
        if (playerLife <= 0)
        {
            playerLife = 0;
            NowGameState = GameState.GameOver;
        }
    }
    void FixedUpdate()
    {
        //설님꺼//
        if (NowGameState == GameState.Started)
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
        //설님꺼//
    }
    IEnumerator LoseTime()
    {
        playerRestTime = playerRestTime - 10;
        if (notifyRestTimeObserver != null)
            notifyRestTimeObserver(playerRestTime);

        yield return new WaitForSeconds(1.5f);
        StartCoroutine("LoseTime");
    }
    //미사일 아이템 사용
    void UseAddMissileItem()
    {
        if (AddMissileItem.gameObject.activeSelf != true)
            AddMissileItem.gameObject.SetActive(true);

        //AccountInfo.ChangeAddMissileItemData(0);

    }
    void UseAssistantItem()
    {
        assistant = true;
        AccountInfo.ChangeAssistantItemData(0);

    }
    void UseLastBombItem()
    {
        AccountInfo.ChangeLastBombItemData(0);
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

