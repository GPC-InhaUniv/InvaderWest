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
    [SerializeField]
    private GameObject DeciedToBuySpaceShipPanel;

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

    private const int ChoiceValue = 1; //버튼 클릭시 빼는 값, 상수

    private int ChoiceNum;

    private int playerShipAmount;

    private List<string> playerShipName = new List<string>();
    private List<string> playerShipSpeed = new List<string>();
    private List<int> playerShipLife = new List<int>();
    private List<int> playerShipPrice = new List<int>();

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

    void Awake()
    {
        playerSelectSpaceShipNumber = GamePlayManager.Instance.PlayerShipNum;
        GetPlayerShipAmount();
        GetSpaceShipStatus();
    }

    void Start ()
    {
        blackHawk = int.Parse(AccountInfo.Instance.BlackHawk);
        raptor = int.Parse(AccountInfo.Instance.Raptor);

        myFuel = int.Parse(AccountInfo.Instance.Fuel);
        restTime = int.Parse(AccountInfo.Instance.RestTime);

        ChangeInventory();

        HideSpaceShip();      

        playerShip[playerSelectSpaceShipNumber].SetActive(true);

        ChangeStatusText(ChoiceValue);
        OnLifeImage(ChoiceValue);
        CheckSpaceShipLock(ChoiceValue);
    }

    void GetPlayerShipAmount()
    {
        playerShipAmount = playerShip.Length-ChoiceValue; //플레이어가 가진 우주선 갯수
    }

    void GetSpaceShipStatus()
    {
        for (int i = 0; i < playerShipAmount + ChoiceValue; i++)
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

    void CheckSpaceShipLock(int choiceNum) //수정필요합니다.
    {
        BuyButton.SetActive(false);
        StartButton.SetActive(false);
        FuelImage.SetActive(false);
        switch (choiceNum)
        {
            case 1:
                if (choiceNum == 1)
                {
                    //소유한 상태
                    StartButton.SetActive(true);
                    CheckEnoughFuel(choiceNum);
                    LockImage.SetActive(false);
                    NoticeText.text = "출격 가능합니다.";
                    return;
                }
                break;
            case 2:
                if (blackHawk == 0)
                {
                    //소유하지 않은 상태
                    FuelImage.SetActive(true);
                    BuyButton.SetActive(true);
                    CheckEnoughFuel(choiceNum);
                    LockImage.SetActive(true);
                    NoticeText.text = playerShipPrice[choiceNum].ToString();
                }
                else
                {
                    //소유한 상태
                    StartButton.SetActive(true);
                    CheckEnoughFuel(choiceNum);
                    LockImage.SetActive(false);
                    NoticeText.text = "출격 가능합니다.";
                }
                break;
            case 3:
                if (raptor == 0)
                {
                    //소유하지 않은 상태
                    FuelImage.SetActive(true);
                    BuyButton.SetActive(true);
                    CheckEnoughFuel(choiceNum);
                    LockImage.SetActive(true);
                    NoticeText.text = playerShipPrice[choiceNum].ToString();
                }
                else
                {
                    //소유한 상태
                    StartButton.SetActive(true);
                    CheckEnoughFuel(choiceNum);
                    LockImage.SetActive(false);
                    NoticeText.text = "출격 가능합니다.";
                }
                break;
            default:
                break;
        }
    }

    bool CheckEnoughFuel(int choiceNum) //가진 재화가 선택한 기체의 재화보다 많은가?
    {
        if (myFuel < playerShipPrice[choiceNum]) //구매 버튼 누르면 여기서 오류남 인덱스 넘어감
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
        //
        playerShip[playerSelectSpaceShipNumber].SetActive(false); //버튼 누른 당시 화면 우주선 사라짐

        playerSelectSpaceShipNumber = next; //선택 값 저장

        playerShip[next].SetActive(true); //선택된 우주선 보여줌
        CheckSpaceShipLock(next); //우주선이 소유했는가? (아닐 시 잠금 UI출력)
        ChangeStatusText(next); //선택된 우주선 스탯을 기반으로 UI출력
        OnLifeImage(next); //선택된 우주선 기반 라이프 UI 출력

        SaveSeletedSpaceShipNumber(); //싱글톤 저장, 소유여부 검사
    }

    void ChoiceSpaceShip(int choiceNum)
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
        switch (playerSelectSpaceShipNumber)
        {
            case 1:
                break;
            case 2:
                if (CheckEnoughFuel(playerSelectSpaceShipNumber) && blackHawk ==0) //소유했으면 못사게해야함. 수정필요합니다.
                {
                    myFuel = myFuel - playerShipPrice[playerSelectSpaceShipNumber];
                    ChoiceSpaceShip(playerSelectSpaceShipNumber);
                    CheckSpaceShipLock(playerSelectSpaceShipNumber);
                    AccountInfo.ChangeFuelData(myFuel);
                    ChangeInventory();
                }
                else WarningPopUpPanel.SetActive(true);
                break;
            case 3:
                if (CheckEnoughFuel(playerSelectSpaceShipNumber) && raptor == 0) //소유했으면 못사게해야함. 수정필요합니다.
                {
                    myFuel = myFuel - playerShipPrice[playerSelectSpaceShipNumber];
                    ChoiceSpaceShip(playerSelectSpaceShipNumber);
                    CheckSpaceShipLock(playerSelectSpaceShipNumber);
                    AccountInfo.ChangeFuelData(myFuel);
                    ChangeInventory();
                }
                else WarningPopUpPanel.SetActive(true);
                break;
            default:
                break;
        }
    }
    public void ShipSelectLeftButtonClick() //미사용
    {
        if (playerSelectSpaceShipNumber == ChoiceValue)
        {
            Debug.Log("오류 : << 플레이어 선택이 0");
            return;
        }
        if (playerSelectSpaceShipNumber >= ChoiceValue + 1)
        {
            Debug.Log("왼쪽 버튼 눌림");
            HideSpaceShip();
            OffLifeImage();

            playerShip[playerSelectSpaceShipNumber].SetActive(false); //버튼 누른 당시 화면 우주선 사라짐
            ChoiceNum = playerSelectSpaceShipNumber - ChoiceValue;

            playerSelectSpaceShipNumber = ChoiceNum; //플레이어쉽 선택 넘버에 선택 번호 저장

            playerShip[ChoiceNum].SetActive(true); //선택된 우주선 보여줌
            CheckSpaceShipLock(ChoiceNum); //우주선이 소유했는가? (잠금 UI출력)

            ChangeStatusText(ChoiceNum); //선택된 우주선 스탯을 기반으로 UI출력

            OnLifeImage(ChoiceNum); //선택된 우주선 기반 라이프 UI 출력
            SaveSeletedSpaceShipNumber(); //싱글톤 저장
        }
    }
    public void ShipSelectRightButtonClick() //미사용
    {
        if (playerSelectSpaceShipNumber == playerShip.Length - ChoiceValue)
        {
            Debug.Log("오류 : 플레이어 선택 범위 벗어남 >>");
            return;
        }
        if (playerSelectSpaceShipNumber < playerShip.Length - ChoiceValue)
        {
            Debug.Log("오른쪽 버튼 눌림");
            HideSpaceShip();
            OffLifeImage();

            playerShip[playerSelectSpaceShipNumber].SetActive(false); //버튼 누른 당시 화면 우주선 사라짐
            ChoiceNum = playerSelectSpaceShipNumber + ChoiceValue;

            playerSelectSpaceShipNumber = ChoiceNum; //플레이어쉽 선택 넘버에 선택 번호 저장

            playerShip[ChoiceNum].SetActive(true); //선택된 우주선 보여줌
            CheckSpaceShipLock(ChoiceNum); //우주선이 소유했는가? (잠금 UI출력)

            ChangeStatusText(ChoiceNum); //선택된 우주선 스탯을 기반으로 UI출력

            OnLifeImage(ChoiceNum); //선택된 우주선 기반 라이프 UI 출력
            SaveSeletedSpaceShipNumber(); //싱글톤 저장
        }
    }
}
