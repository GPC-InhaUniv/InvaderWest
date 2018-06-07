using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubjectable
{
    void RegisterObserver(IObserverable o);
    void RemoveObserver(IObserverable o);
    void NotifyObservers();
}
public interface IObserverable
{
    void UpdateData(int playerLife, int playerRestTime);
    void GetPlayerData(int playerLife);
}
public interface IDisplayable
{
    void DisPlay();
   
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
        GamePlayManager.Instance.PlayerShipNum = 1;

        if (GamePlayManager.Instance.PlayerShipNum == 1)
        {
            playerLife = 3;

        }
        if (GamePlayManager.Instance.PlayerShipNum == 2)
        {
            playerLife = 4;

        }
        if (GamePlayManager.Instance.PlayerShipNum == 3)
        {
            playerLife = 3;

        }
        NotifyStartDataToObservers();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bolt"))
        {
            playerLife = playerLife - 1;
        }
        NotifyObservers();

        Debug.Log("Enter check");
    }

    public void NotifyStartDataToObservers()
    {
        for (int i = 0; i < observerList.Count; i++)
        {
            observerList[i].GetPlayerData(playerLife);
        }
    }


    public void NotifyObservers()
    {
        for (int i = 0; i < observerList.Count; i++)
        {
            observerList[i].UpdateData(playerLife , playerRestTime);
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



}
