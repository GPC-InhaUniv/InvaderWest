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


    void Awake()
    {

        NowGameState = GameState.Ready;

        Invoke("GameStart", 3.0f);

        //playerLife = GamePlayManager.Instance.PlayerShipNum;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerShip>();
        player.RegisterObserver(this);

        //player
        GameStart();
    }

    // Use this for initialization
    void Start () {
        //GameWinResultPanel.gameObject.SetActive(false);

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
    //플레이어 시작라이프//
    public void GetPlayerLife(int playerLife)
    {
        if (playerLife == 3)
        {
            LifeImage[0].gameObject.SetActive(false);
            Debug.Log("라이프줄었다");
        }

    }
    //플레이어 라이프//
    public void UpdatePlayerLife(int playerLife)
    {
        if (NowGameState == GameState.Start &&playerLife>0)
        {
            this.playerLife = playerLife;
            Debug.Log("Observer Success  " + playerLife);
            DisPlayPlayerLife();
        }
        else
        {
            NowGameState=GameState.GameOver;
            player.RemoveObserver(this);
            DisPlayPlayerLife();
            GameLoseResultPanel.gameObject.SetActive(true);
        }
    }
    //플레이어 시간//
    public void UpdatePlayerRestTime(int playerRestTime)
    {
        if (NowGameState == GameState.Start && playerRestTime > 0)
        {
            this.playerRestTime = playerRestTime;
            DisplayPlayerRestTime();
        }
        else
        {
            NowGameState = GameState.GameOver;
            player.RemoveObserver(this);
            GameLoseResultPanel.gameObject.SetActive(true);
        }
        
    }
    public void DisplayPlayerRestTime()
    {
        RestTimeScoreText.text = playerRestTime.ToString();
        Debug.Log(playerRestTime);
    }

    public void DisPlayPlayerLife()
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
