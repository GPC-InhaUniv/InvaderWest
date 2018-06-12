using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 


public class InGameController : MonoBehaviour, IObserverable , IDisplayable
{



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

        //playerLife = GamePlayManager.Instance.PlayerShipNum;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerShip>();
        player.RegisterObserver(this);
        
        //GameStart();

    }


    // Use this for initialization
    void Start () {



    }

	
	// Update is called once per frame
	void Update () {


		
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
        if (playerLife>0)
        {
            this.playerLife = playerLife;
            Debug.Log("Observer Success  " + playerLife);
            DisPlayPlayerLife();
        }
        else
        {
            player.RemoveObserver(this);
            DisPlayPlayerLife();
            GameLoseResultPanel.gameObject.SetActive(true);
        }
    }
    //플레이어 시간//
    public void UpdatePlayerRestTime(int playerRestTime)
    {
        if (playerRestTime > 0)
        {
            this.playerRestTime = playerRestTime;
            DisplayPlayerRestTime();
        }
        else
        {

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
