using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour {


    private static GamePlayManager instance;
    public static GamePlayManager Instance
    {
        get { return instance; }
        set { instance = value; }
    }
    [SerializeField]
    private int playerShipNum;
    public int PlayerShipNum
    {
        get { return playerShipNum; }
        set { playerShipNum = value; }
    }
    private string playerName;
    public string PlayerName
    {
        get { return playerName; }
        set { playerName = value; }
    }

    private void Awake()
    {
        if (instance != this)
            instance = this;
        DontDestroyOnLoad(gameObject);
       
    }
    public void SelecPlayer()
    {
        Debug.Log(playerShipNum);
        Debug.Log(PlayerShipNum);
    }
}
