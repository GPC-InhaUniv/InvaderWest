using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShipChoiceNum : MonoBehaviour {

    [SerializeField]
    private GameObject[] LifeImage;
    [SerializeField]
    private GameObject[] playerShip;
    [SerializeField]
    private GameObject WarningPopUpPanel;

    [Header("SpaceShipStatus")]
    [SerializeField]
    private Text NoticeText;
    [SerializeField]
    private Text SpaceShipSelectNumText;
    [SerializeField]
    private Text SpaceShipNameText;
    [SerializeField]
    private Text SpaceShipSpeedText;

    [Header("Image")]
    [SerializeField]
    private GameObject LockImage;
    [SerializeField]
    private GameObject FuelImage;

    [Header("Start and Buy")]
    [SerializeField]
    private GameObject StartButton;
    [SerializeField]
    private Text StartText;
    [SerializeField]
    private GameObject BuyButton;
    [SerializeField]
    private Text BuyText;

    private int playerSelectSpaceShipNumber; // 초기값 or 플레이어가 고른 값 

    private const int initValue = 1; 

    private int playerShipAmount;

    private List<string> playerShipName = new List<string>();
    private List<string> playerShipSpeed = new List<string>();
    private List<int> playerShipLife = new List<int>();
    private List<int> playerShipPrice = new List<int>();

    //private int ChoiceNum; // 버튼결합으로 인한 미사용

    //수정한사람 황윤우 //
    [Header("Time and Fuel")]
    [SerializeField]
    private Text FuelScoreText;
    [SerializeField]
    private Text RestTimeText;

    private int myFuel;
    private int restTime;
    private int blackHawk;
    private int raptor;

    void ChangeInventory()
    {
        FuelScoreText.text = myFuel.ToString();
        RestTimeText.text = restTime.ToString();
    }
    //수정한사람 황윤우 //

    void Start ()
    {
        GetPlayerShipAmount();
        GetSpaceShipStatus();
        /////////정보 받아오기
        playerSelectSpaceShipNumber = GamePlayManager.Instance.PlayerShipNum;
        blackHawk = int.Parse(AccountInfo.Instance.BlackHawk);
        raptor = int.Parse(AccountInfo.Instance.Raptor);
        myFuel = int.Parse(AccountInfo.Instance.Fuel);
        restTime = int.Parse(AccountInfo.Instance.RestTime);
        //////////
        ChangeInventory();

        HideSpaceShip();      
        playerShip[playerSelectSpaceShipNumber].SetActive(true);

        ChangeStatusText(initValue);
        OnLifeImage(initValue);
        CheckSpaceShipLock(initValue);
    }

    void GetPlayerShipAmount()
    {
        playerShipAmount = playerShip.Length - initValue; //플레이어가 가진 우주선 갯수
    }

    void GetSpaceShipStatus()
    {
        for (int i = 0; i < playerShip.Length; i++)
        {
            playerShipName.Add(playerShip[i].GetComponent<SpaceShipStatus>().SpaceShipName);
            playerShipSpeed.Add(playerShip[i].GetComponent<SpaceShipStatus>().Speed.ToString());
            playerShipLife.Add(playerShip[i].GetComponent<SpaceShipStatus>().Life);
            playerShipPrice.Add(playerShip[i].GetComponent<SpaceShipStatus>().SpaceShipPrice);
        }
    }

    void ChangeStatusText(int choiceNum)
    {
        SpaceShipSelectNumText.text = "(" + playerSelectSpaceShipNumber + " / " + playerShipAmount + ")";
        SpaceShipNameText.text = playerShipName[playerSelectSpaceShipNumber];
        SpaceShipSpeedText.text = playerShipSpeed[playerSelectSpaceShipNumber];
    }

    void OnLifeImage(int choiceNum)
    {
        for(int i = 0; i < playerShipLife[choiceNum]; i++)
        {
            LifeImage[i].SetActive(true);
        }
    }

    void OffLifeImage()
    {
        for (int i = 0; i < LifeImage.Length; i++)
        {
            LifeImage[i].SetActive(false);
        }
    }

    void HideSpaceShip()
    {
        for(int i =0; i < playerShip.Length; i++)
        {
            playerShip[i].SetActive(false);
        }
    }

    void CheckSpaceShipLock(int choiceNum)
    {
        BuyButton.SetActive(false);
        StartButton.SetActive(false);
        FuelImage.SetActive(false);

        switch (choiceNum)
        {
            case 1:
                if (choiceNum == 1)
                {
                    DisplayStart();
                }
                break;
            case 2:
                if (blackHawk == 1)
                {               
                    DisplayStart();
                }
                else
                {
                    DisplayLock(choiceNum);
                }
                break;
            case 3:
                if (raptor == 1)
                {
                    DisplayStart();
                }
                else
                {
                    DisplayLock(choiceNum);
                }
                break;
            default:
                break;
        }
    }

    void DisplayLock(int choiceNum)
    {
        FuelImage.SetActive(true);
        BuyButton.SetActive(true);
        LockImage.SetActive(true);
        NoticeText.text = playerShipPrice[choiceNum].ToString();
    }
    void DisplayStart()
    {
        StartButton.SetActive(true);
        LockImage.SetActive(false);
        NoticeText.text = "출격 가능합니다.";
    }

    bool CheckEnoughFuel(int choiceNum) //가진 재화가 선택한 기체의 재화보다 많은가?
    {
        if (myFuel < playerShipPrice[choiceNum]) //재화 부족
        {
            return false;
        }
        else
        {          
            return true;
        }
    }

    public void SaveSeletedSpaceShipNumber()
    {
        switch (playerSelectSpaceShipNumber)
        {
            case 1:
                GamePlayManager.Instance.PlayerShipNum = playerSelectSpaceShipNumber;
                break;
            case 2:
                if(blackHawk == 1)
                GamePlayManager.Instance.PlayerShipNum = playerSelectSpaceShipNumber;
                break;
            case 3:
                if(raptor == 1)
                GamePlayManager.Instance.PlayerShipNum = playerSelectSpaceShipNumber;
                break;
            default:
                break;
        }
    }

    public void ShipSelectButtonClick(int click) //
    {
        // click = -1 이면 왼쪽 버튼이 눌린 것, click = +1 이면 오른쪽 버튼이 눌린 것으로 판단한다.
        int next = playerSelectSpaceShipNumber + click;

        if (next < 1) //  선택값이 1보다는 커야함
            return;
        if (next > playerShip.Length - 1) //선택값 > 3(선택창 최대길이)
            return;

        if (click < 0) //-1, 1 눌림 검사
            Debug.Log("왼쪽 버튼 눌림");
        else
            Debug.Log("오른쪽 버튼 눌림");

        // 우주선, 이미지 setactive(false);
        HideSpaceShip();
        OffLifeImage();
        playerShip[playerSelectSpaceShipNumber].SetActive(false); //버튼 누른 당시 화면 우주선 사라짐
        //

        playerSelectSpaceShipNumber = next; //선택 값 저장

        playerShip[next].SetActive(true); //선택된 우주선 보여줌
        CheckSpaceShipLock(next); //우주선이 소유했는가? (아닐 시 잠금 UI출력)

        //소유여부와 무관하게 출력
        ChangeStatusText(next); //선택된 우주선 스탯을 기반으로 UI출력
        OnLifeImage(next); //선택된 우주선 기반 라이프 UI 출력

        SaveSeletedSpaceShipNumber(); //싱글톤 저장, 소유여부 검사
    }

    void ChangeSpaceShipData(int choiceNum)
    {
        switch (choiceNum)
        {
            case 1: return;// 기본
            case 2: AccountInfo.ChangeBlackHawkData(1);
                blackHawk = blackHawk + 1;
                break;
            case 3: AccountInfo.ChangeRaptoritemData(1);
                raptor = raptor + 1;
                break;
            default:
                break;
        }
    }

    public void ChangeSceneToSelectStage()
    {
        SceneManager.LoadScene("StageSelect");
    }

    public void BuySpaceShip()
    {
        if (blackHawk == 1 && raptor == 1)
            return;
        if (!CheckEnoughFuel(playerSelectSpaceShipNumber))
            WarningPopUpPanel.SetActive(true);
        else
        {
            myFuel = myFuel - playerShipPrice[playerSelectSpaceShipNumber];
            ChangeSpaceShipData(playerSelectSpaceShipNumber);
            CheckSpaceShipLock(playerSelectSpaceShipNumber);

            AccountInfo.ChangeFuelData(myFuel);
            SaveSeletedSpaceShipNumber();
            ChangeInventory();
        }
    }
}
