using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
[System.Serializable]
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
    [SerializeField]
    private int assistantitem;
    [SerializeField]
    private int lastBombitem;

    List<IObserverable> observerList = new List<IObserverable>();

    void Awake()
    {


    }

    void Start()
    {
        //addMissileitem = int.Parse(AccountInfo.Instance.AddMissileitem);
        //assistantitem = int.Parse(AccountInfo.Instance.Assistantitem);
        //lastBombitem = int.Parse(AccountInfo.Instance.LastBombitem);

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


        DataStart();



    }
    public void DataStart()
    {
        NotifyStartDataToObservers();
        Debug.Log("gogogoo");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bolt"))
        {
            playerLife = playerLife - 1;
        }
        NotifyPlayerLifeToObservers();  
    }


    private void Update()
    {

        if (lastBombitem == 1)
        {
            UseLastBombitem();
            Debug.Log("UseLastBombitem");
        }

    }

    public void NotifyStartDataToObservers()
    {
        for (int i = 0; i < observerList.Count; i++)
        {
            observerList[i].GetPlayerLife(playerLife);
            Debug.Log("에너지 알려주기");
            Debug.Log(playerLife);
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
        Debug.Log("관찰시작");
            
    }

    public void RemoveObserver(IObserverable o)
    {
        observerList.Remove(o);
        Debug.Log("remove");
    }
    private IEnumerator LoseTime()
    {
        Debug.Log("코루틴돈다");
        playerRestTime = playerRestTime - 10;
        NotifyPlayerRestTimeToObservers();

        yield return new WaitForSeconds(1.5f);

        StartCoroutine("LoseTime");
    }

    void UseAddMissileitem()
    {

        AccountInfo.ChangeAddMissileitemData(0);

    }
    void UseAssistantitem()
    {
        AccountInfo.ChangeAssistantitemData(0);

    }
    void UseLastBombitem()
    {
        AccountInfo.ChangeLastBombitemData(0);

    }
}
