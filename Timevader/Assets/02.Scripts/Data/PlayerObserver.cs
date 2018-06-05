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
    void UpdateData(int playerLife, int playerRestTime, int playerfirerapid);
}
public interface IDisplayable
{
    void DisPlay();
}

public class PlayerObserver : MonoBehaviour , ISubjectable
{

    private int playerLife;
    private int playerRestTime;
    private int playerfirerapid;

    List<IObserverable> observerList = new List<IObserverable>();

    void Start()
    {
        if (GamePlayManager.Instance.PlayerShipNum == 1)
        {
            playerLife = 3;

        }
        if (GamePlayManager.Instance.PlayerShipNum == 2)
        {
            playerLife = 6;

        }
        if (GamePlayManager.Instance.PlayerShipNum == 3)
        {
            playerLife = 9;

        }

    }


    public void NotifyObservers()
    {
        for (int i = 0; i < observerList.Count; i++)
        {
           // observerList[i].UpdateData(playerLife , playerRestTime);
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



}
