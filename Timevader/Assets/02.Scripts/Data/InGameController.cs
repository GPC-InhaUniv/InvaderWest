using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public enum GameState
{
    Ready,
    Start,
    GameOver
}

public class InGameController : MonoBehaviour, IObserverable , IDisplayable
{


    public GameState NowGameState;

    public GameObject GameWinResultPanel;
    public GameObject GameLoseResultPanel;

    public Text RestTimeScoreText;
    public Text StageText;

    public Image Life3Image;
    public Image Life2Image;
    public Image Life1Image;


    private int playerLife;
    private int playerRestTime;


    // Use this for initialization
    void Start () {
        //GameWinResultPanel.gameObject.SetActive(false);

        NowGameState = GameState.Ready;

        Invoke("GameStart", 3.0f);

        playerLife = GamePlayManager.Instance.PlayerShipNum;
        

    }
    void GameStart()
    {
        NowGameState = GameState.Start;
    }
	
	// Update is called once per frame
	void Update () {

        if(NowGameState == GameState.Start)
        {
            


        }
		
	}

    public void UpdateData(int playerLife, int playerRestTime, int playerfirerapid)
    {
        this.playerLife = playerLife;
        this.playerRestTime = playerRestTime;
    }

    public void DisPlay()
    {
        throw new System.NotImplementedException();
    }
}
