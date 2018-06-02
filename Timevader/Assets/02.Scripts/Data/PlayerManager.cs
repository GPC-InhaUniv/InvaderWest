using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {


    private static PlayerManager instance;
    public static PlayerManager Instance
    {
        get { return instance; }
        set { instance = value; }
    }

    private int playerShipNum;
    public int PlayerShipNum
    {
        get { return playerShipNum; }
        set { playerShipNum = value; }
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
    }

}
