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

    public Image[] LifeImage;


    [SerializeField]
    private int playerLife;
    [SerializeField]
    private int playerRestTime;
    [SerializeField]
    public ISubjectable player;




    // Use this for initialization
    void Start () {
        //GameWinResultPanel.gameObject.SetActive(false);

        NowGameState = GameState.Ready;

        Invoke("GameStart", 3.0f);

        //playerLife = GamePlayManager.Instance.PlayerShipNum;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerShip>();
        player.RegisterObserver(this);

        //player
        GameStart();
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
    public void GetPlayerData(int playerLife)
    {
        if (playerLife == 3)
        {
            LifeImage[0].gameObject.SetActive(false);
        }

    }

    public void UpdateData(int playerLife, int playerRestTime)
    {
        if (NowGameState == GameState.Start &&playerLife>0)
        {
            if (playerLife < 4)
                LifeImage[0].gameObject.SetActive(false);
            this.playerLife = playerLife;
            this.playerRestTime = playerRestTime;
            Debug.Log("Observer Success  " + playerLife);
        }
        else
        {
            NowGameState=GameState.GameOver;
            GameLoseResultPanel.gameObject.SetActive(true);
        }
        DisPlay();

    }

    public void DisPlay()
    {
        //Debug.Log("go");
        // 옵저버 끊어버리기 player.RemoveObserver(this);

        //Life Image Change//
        for (int i = 0; i < LifeImage.Length; i++)
        {
            if (LifeImage[i].gameObject.activeSelf == true)
            {
                LifeImage[i].gameObject.SetActive(false);

                return;
            }
        }
    }

    
}
