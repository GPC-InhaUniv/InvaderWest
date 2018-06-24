using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    public delegate void NotifyEnemy();
    public static event NotifyEnemy OnUseItem;

    public GameObject AddMissileItem;

    public Boundary Boundary;

    [SerializeField]
    int playerLife;
    [SerializeField]
    int playerRestTime;
    [SerializeField]
    int playerfirerapid;
    [SerializeField]
    int addMissileItem;
    [SerializeField]
    int assistantItem;
    bool assistant;
    [SerializeField]
    int lastBombItem;
    [SerializeField]
    float nextFire = 0.2f;
    [SerializeField]
    float speed;
    [SerializeField]
    Transform shotSpawn, addedSpawn;
    [SerializeField]
    GameState nowGameState;
    [SerializeField]
    AudioSource shotAudioSource;
    [SerializeField]
    InGameController inGameController;



    public float fireDelta = 0.2f;

    float myTime = 0.0f;
    Rigidbody rigid;
    Vector3 movement, prevPosition, currentPosition;
    bool hasDoubleMissile = false;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        StartCoroutine(CheckGameState());

        inGameController = GameObject.Find("GameController").GetComponent<InGameController>();

        //Test 끝나면 주석제거하기//
        addMissileItem = int.Parse(AccountInfo.Instance.AddMissileItem);
        assistantItem = int.Parse(AccountInfo.Instance.AssistantItem);
        lastBombItem = int.Parse(AccountInfo.Instance.LastBombItem);
        playerRestTime = int.Parse(AccountInfo.Instance.RestTime);

        //Test 플레이어//
        if (GamePlayManager.Instance.PlayerShipNum == 1)
            playerLife = 3;
        else if (GamePlayManager.Instance.PlayerShipNum == 2)
            playerLife = 3;
        else if (GamePlayManager.Instance.PlayerShipNum == 3)
            playerLife = 4;
        //Test//
        if (addMissileItem == (int)DataBoolean.TRUE) 
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
        StartCoroutine("LoseTime");
    }

    void FixedUpdate()
    {
        if (nowGameState == GameState.Started)
            MovePlayer();
    }

    public void GetItem(ItemList itemKind)
    {
        switch (itemKind)
        {
            case ItemList.AddMissileItem:
                AddMissile();
                break;
            case ItemList.IncreasingShotSpeedItem:
                IncreasingShotSpeed();
                break;
            case ItemList.AssistantItem:
                break;
        }
    }

    void AddMissile()
    {
        hasDoubleMissile = true;
        Shoot(addedSpawn);
    }

    void IncreasingShotSpeed()
    {
        fireDelta = 0.3f;
    }
    void Shoot(Transform AnySpawn)
    {
        myTime = myTime + Time.deltaTime;
        if (myTime > nextFire)
        {
            nextFire = myTime + fireDelta;
            GameObject shot =  PoolController.instance.GetFromPool(PoolType.BoltPool);
            if (shot == null)
            {
                Debug.Log("Pool에 남은 미사일이 부족합니다.");
                return;
            }
            shot.transform.position = shotSpawn.transform.position;
            //shot.SetActive(true);
            shot = null; // 초기화
            nextFire = nextFire - myTime;
            myTime = 0.0f;
            //샷 오디오 추가//
            shotAudioSource.Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
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
                if (notifyLifeToObserver != null && playerLife >= 0) 
                {
                    notifyLifeToObserver(playerLife);
                }
            }
        }
    }
    void Update()
    {
        if (nowGameState == GameState.Started)
        {
            if (hasDoubleMissile == true)
            {
                Shoot(shotSpawn);
                Shoot(addedSpawn);

            }
            else
                Shoot(shotSpawn);

            //if (lastBombItem == (int)DataBoolean.TRUE && Input.GetKeyDown(KeyCode.F))
            if (Input.GetKeyDown(KeyCode.F)) 
            {
                UseLastBombItem();
                Debug.Log("UseLastBombItem");
                
            }

            if (playerLife <= 0)
            {
                playerLife = 0;
                GamePlayManager.Instance.NowGameState = GameState.Lose;
            }
        }
        if(nowGameState == GameState.Win)
        {
            StartCoroutine("isGameOver");
        }

    }
    //게임오버 -> 플레이어 위치 원점으로//
    IEnumerator isGameOver()
    {
        rigid.constraints = RigidbodyConstraints.FreezeAll;
        //게임종료시 모든 비행선 폭파//
        yield return new WaitForSeconds(0.2f);

        Vector3 startPosition = new Vector3(0.0f, -4.0f, 0.0f);
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, startPosition, 0.1f);
        //rigid.constraints = RigidbodyConstraints.None;
    }


    /* 지용 */
    void MovePlayer()
    {
        if (nowGameState == GameState.Started)
        {
            if (Input.GetMouseButton(0))
            {
                currentPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
                transform.position = Vector3.Lerp(transform.position, currentPosition, Time.deltaTime * speed);
            }

            //if (Input.GetMouseButton(0))
            //{
            //    currentPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            //    movement = currentPosition - prevPosition;
            //    Debug.Log("movement : " + movement);
            //    if (DistanceCompare(movement))
            //    {
            //        movement = movement.normalized;
            //        transform.Translate(new Vector3(movement.x, 0.0f, movement.y) * Time.deltaTime * speed);
            //    }
            //    prevPosition = currentPosition;
            //}

            //if (Input.GetMouseButton(0))
            //{
            //    currentPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            //    movement = currentPosition - prevPosition;
            //    transform.position = Vector3.Lerp(transform.position, transform.position + movement, Time.deltaTime * speed);
            //    prevPosition = currentPosition;
            //}

            transform.position = new Vector3
            (
                Mathf.Clamp(transform.position.x, Boundary.xMin, Boundary.xMax),
                Mathf.Clamp(transform.position.y, Boundary.yMin, Boundary.yMax),
                0.0f
            );
        }
    }
    //[SerializeField]
    //float gap = 1.0f;
    //bool DistanceCompare(Vector3 movement)
    //{
    //    float distance = movement.x * movement.x + movement.y * movement.y;
    //    if (distance >= gap) return true;
    //    else return false;
    //}

    IEnumerator LoseTime()
    {
        if (nowGameState == GameState.Started)
        {
            playerRestTime = playerRestTime - 10;
            if (notifyRestTimeObserver != null)
                notifyRestTimeObserver(playerRestTime);
        }
        yield return new WaitForSeconds(1.5f);
        StartCoroutine("LoseTime");

    }
    
    //미사일 아이템 사용
    void UseAddMissileItem()
    {
        if (AddMissileItem.gameObject.activeSelf != true)
            AddMissileItem.gameObject.SetActive(true);

        AccountInfo.ChangeAddMissileItemData((int)DataBoolean.FALSE);

    }
    void UseAssistantItem()
    {
        assistant = true;
        AccountInfo.ChangeAssistantItemData(0);
    }
    void UseLastBombItem()
    {
        OnUseItem();
        lastBombItem = 0;
        AccountInfo.ChangeLastBombItemData((int)DataBoolean.FALSE);
    }
    IEnumerator CheckGameState()
    {
        nowGameState = GamePlayManager.Instance.NowGameState;
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(CheckGameState());
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

