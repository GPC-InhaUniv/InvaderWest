using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShipChoiceNum : MonoBehaviour {

    [SerializeField]
    GameObject[] LifeImages;
    [SerializeField]
    GameObject[] playerShips;
    [SerializeField]
    GameObject WarningPopUpPanel;

    [Header("SpaceShipStatus")]
    [SerializeField]
    Text NoticeText, SpaceShipSelectNumText, SpaceShipNameText, SpaceShipSpeedText;

    [Header("Image")]
    [SerializeField]
    GameObject LockImage, FuelImage;

    [Header("Start and Buy")]
    [SerializeField]
    GameObject StartButton, BuyButton;
    [SerializeField]
    Text StartText, BuyText;

    int playerSelectSpaceShipNumber; // 초기값 or 플레이어가 고른 값 

    const int initValue = 1; 

    int playerShipAmount;

    List<string> playerShipsName = new List<string>();
    List<string> playerShipsSpeed = new List<string>();
    List<int> playerShipsLife = new List<int>();
    List<int> playerShipsPrice = new List<int>();

    //수정한사람 황윤우 //
    [Header("Time and Fuel")]
    [SerializeField]
    Text FuelScoreText;
    [SerializeField]
    Text RestTimeText;

    int myFuel;
    int restTime;
    int blackHawk;
    int raptor;

    void Start ()
    {
        GetPlayerShipAmount();
        SetSpaceShipStatus();

        /////////정보 받아오기
        SetGamePlayManagerData();
        //////////
        ChangeInventory();

        HideSpaceShip();
        playerShips[playerSelectSpaceShipNumber].SetActive(true);

        ChangeStatusText(initValue);
        OnLifeImage(initValue);
        CheckSpaceShipLock(initValue);
    }

    void ChangeInventory()    //수정한사람 황윤우 
    {
        FuelScoreText.text = myFuel.ToString();
        RestTimeText.text = restTime.ToString();
    }


    int GetPlayerShipAmount()
    {
        playerShipAmount = playerShips.Length - initValue; //플레이어가 가진 우주선 갯수
        return playerShipAmount;
    }
    void SetGamePlayManagerData()
    {
        playerSelectSpaceShipNumber = GamePlayManager.Instance.PlayerShipNum;
        blackHawk = int.Parse(AccountInfo.Instance.BlackHawk); //개선할 것
        raptor = int.Parse(AccountInfo.Instance.Raptor);
        myFuel = int.Parse(AccountInfo.Instance.Fuel);
        restTime = int.Parse(AccountInfo.Instance.RestTime);
    }

    void SetSpaceShipStatus()
    {
        for (int i = 0; i < playerShips.Length; i++)
        {
            playerShipsName.Add(playerShips[i].GetComponent<SpaceShipStatus>().SpaceShipName);
            playerShipsSpeed.Add(playerShips[i].GetComponent<SpaceShipStatus>().Speed.ToString());
            playerShipsLife.Add(playerShips[i].GetComponent<SpaceShipStatus>().Life);
            playerShipsPrice.Add(playerShips[i].GetComponent<SpaceShipStatus>().SpaceShipPrice);
        }
    }

    void ChangeStatusText(int choiceNum)
    {
        SpaceShipSelectNumText.text = "(" + playerSelectSpaceShipNumber + " / " + playerShipAmount + ")";
        SpaceShipNameText.text = playerShipsName[playerSelectSpaceShipNumber];
        SpaceShipSpeedText.text = playerShipsSpeed[playerSelectSpaceShipNumber];
    }

    void OnLifeImage(int choiceNum)
    {
        for(int i = 0; i < playerShipsLife[choiceNum]; i++)
        {
            LifeImages[i].SetActive(true);
        }
    }

    void OffLifeImage()
    {
        for (int i = 0; i < LifeImages.Length; i++)
        {
            LifeImages[i].SetActive(false);
        }
    }

    void HideSpaceShip()
    {
        for(int i =0; i < playerShips.Length; i++)
        {
            playerShips[i].SetActive(false);
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
        NoticeText.text = playerShipsPrice[choiceNum].ToString();
    }
    void DisplayStart()
    {
        StartButton.SetActive(true);
        LockImage.SetActive(false);
        NoticeText.text = "출격 가능합니다.";
    }

    public void ShipSelectButtonClick(int click) //
    {
        // click = -1 이면 왼쪽 버튼이 눌린 것, click = +1 이면 오른쪽 버튼이 눌린 것으로 판단한다.
        int next = playerSelectSpaceShipNumber + click;

        if (next < 1) //  선택값이 1보다는 커야함
            return;
        if (next > playerShips.Length - 1) //선택값 > 3(선택창 최대길이)
            return;

        if (click < 0) //-1, 1 눌림 검사
            Debug.Log("왼쪽 버튼 눌림");
        else
            Debug.Log("오른쪽 버튼 눌림");

        // 우주선, 이미지 setactive(false);
        HideSpaceShip();
        OffLifeImage();
        playerShips[playerSelectSpaceShipNumber].SetActive(false); //버튼 누른 당시 화면 우주선 사라짐
        //

        playerSelectSpaceShipNumber = next; //선택 값 저장

        playerShips[next].SetActive(true); //선택된 우주선 보여줌
        CheckSpaceShipLock(next); //우주선이 소유했는가? (아닐 시 잠금 UI출력)

        //소유여부와 무관하게 출력
        ChangeStatusText(next); //선택된 우주선 스탯을 기반으로 UI출력
        OnLifeImage(next); //선택된 우주선 기반 라이프 UI 출력

        SaveSeletedSpaceShipNumber(); //싱글톤 저장, 소유여부 검사
    }

    public void OnClickedBuySpaceShipButton()//구매 버튼 온클릭
    {
        if (blackHawk == 1 && raptor == 1)
            return;
        if (!CheckEnoughFuel(playerSelectSpaceShipNumber))
            WarningPopUpPanel.SetActive(true);
        else
        {
            myFuel = myFuel - playerShipsPrice[playerSelectSpaceShipNumber];
            ChangeSpaceShipData(playerSelectSpaceShipNumber);
            CheckSpaceShipLock(playerSelectSpaceShipNumber);

            AccountInfo.ChangeFuelData(myFuel);
            SaveSeletedSpaceShipNumber();
            ChangeInventory();
        }
    }

    bool CheckEnoughFuel(int choiceNum) //가진 재화가 선택한 기체의 재화보다 많은가?
    {
        if (myFuel < playerShipsPrice[choiceNum]) //재화 부족
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void SaveSeletedSpaceShipNumber()
    {
        switch (playerSelectSpaceShipNumber)
        {
            case 1:
                GamePlayManager.Instance.PlayerShipNum = playerSelectSpaceShipNumber;
                break;
            case 2:
                if (blackHawk == 1)
                    GamePlayManager.Instance.PlayerShipNum = playerSelectSpaceShipNumber;
                break;
            case 3:
                if (raptor == 1)
                    GamePlayManager.Instance.PlayerShipNum = playerSelectSpaceShipNumber;
                break;
            default:
                break;
        }
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

    public void OnClickedChangeSceneToSelectStageButton() //스타트 버튼 온클릭
    {
        SceneManager.LoadScene("StageSelect");
    }
}
