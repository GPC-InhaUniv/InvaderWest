﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    
    Ready,
    CameraEffect,
    Started,
    Win,
    WinCameraEffect,
    Lose,
}

public class GamePlayManager : MonoBehaviour {

    public delegate void NotifyGameState();
    public static event NotifyGameState OnChangeGamestate;

    static GamePlayManager instance;
    public static GamePlayManager Instance
    {
        get { return instance; }
        set { instance = value; }
    }
    [SerializeField]
    int playerShipNum;
    public int PlayerShipNum
    {
        get { return playerShipNum; }
        set { playerShipNum = value; }
    }
    string playerName;
    public string PlayerName
    {
        get { return playerName; }
        set { playerName = value; }
    }
    [SerializeField]
    int stageData;
    public int StageData
    {
        get { return stageData; }
        set { stageData = value; }
    }
    [SerializeField]
    GameState nowGameState;
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
        PlayerShipNum = 1;
        DontDestroyOnLoad(gameObject);
       
    }
    public void SelecPlayer()
    {
        Debug.Log(playerShipNum);
        Debug.Log(PlayerShipNum);
    }
    public void ChangeGameState()
    {
        NowGameState += 1;
        OnChangeGamestate();
    }
    public void ChangeGameStateLose()
    {
        NowGameState = GameState.Lose;
        OnChangeGamestate();
    }
}
