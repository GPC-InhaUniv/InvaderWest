using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Ready,
    Started,
    GameOver
}

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
    [SerializeField]
    private int StageData;
    public int stageData
    {
        get { return StageData; }
        set { StageData = value; }
    }
    private GameState nowGameState;

    public GameState NowGameState
    {
        get { return nowGameState; }
        set { nowGameState = value; }
    }


    private void Awake()
    {
        if (instance != this)
            instance = this;

        NowGameState = GameState.Ready;
        DontDestroyOnLoad(gameObject);
       
    }
    public void SelecPlayer()
    {
        Debug.Log(playerShipNum);
        Debug.Log(PlayerShipNum);
    }
}
