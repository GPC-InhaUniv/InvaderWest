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

public class PlayerShip : MonoBehaviour , ISubjectable
{
    [SerializeField]
    private int playerLife;
    [SerializeField]
    private int playerRestTime;
    [SerializeField]
    private int playerfirerapid;

    List<IObserverable> observerList = new List<IObserverable>();

    void Start()
    {
        GamePlayManager.Instance.PlayerShipNum = 2;

        if (GamePlayManager.Instance.PlayerShipNum == 1)
        {
            playerLife = 3;
            playerRestTime = 5000;

        }
        if (GamePlayManager.Instance.PlayerShipNum == 2)
        {
            playerLife = 4;
            playerRestTime = 2500;

        }
        if (GamePlayManager.Instance.PlayerShipNum == 3)
        {
            playerLife = 3;
            playerRestTime = 1000;

        }
        NotifyStartDataToObservers();

        StartCoroutine("LoseTime");
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bolt"))
        {
            playerLife = playerLife - 1;
        }
        NotifyPlayerLifeToObservers();

        //Debug.Log("Enter check");
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
        Debug.Log("remove");
    }
    private IEnumerator LoseTime()
    {
        playerRestTime = playerRestTime - 10;
        NotifyPlayerRestTimeToObservers();

        yield return new WaitForSeconds(1.5f);

        Debug.Log(playerRestTime);

        StartCoroutine("LoseTime");
        


    }


}
