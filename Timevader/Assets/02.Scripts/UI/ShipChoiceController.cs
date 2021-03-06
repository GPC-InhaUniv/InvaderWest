﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShipChoiceController : MonoBehaviour {

    [SerializeField]
    GameObject[] lifeImages;
    [SerializeField]
    GameObject[] playerShips;
    [SerializeField]
    GameObject warningPopUpPanel;

    [Header("SpaceShipStatus")]
    [SerializeField]
    Text noticeText;
    [SerializeField]
    Text spaceShipSelectNumText;
    [SerializeField]
    Text spaceShipNameText;
    [SerializeField]
    Text spaceShipSpeedText;

    [Header("ImageObj")]
    [SerializeField]
    GameObject lockImage;
    [SerializeField]
    GameObject fuelImage;

    [Header("Start and Buy Btn")]
    [SerializeField]
    GameObject startButton;
    [SerializeField]
    GameObject buyButton;
    [SerializeField]
    Text startText;
    [SerializeField]
    Text buyText;

    //수정한사람 황윤우 //
    [Header("Time and Fuel")]
    [SerializeField]
    Text fuelScoreText;
    [SerializeField]
    Text restTimeText;

    List<ShipStatus> status = new List<ShipStatus>(); //구조체

    int myFuel, restTime, blackHawk, raptor;

    int playerSelectSpaceShipNumber; // 초기값 or 플레이어가 고른 값 

    int playerShipAmount; //플레이어가 가진 우주선의 총합

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

        ChangeStatusText(1);
        OnLifeImage(1);
        CheckSpaceShipLock(1);
    }

    void ChangeInventory()   //수정한사람 황윤우 
    {
        fuelScoreText.text = myFuel.ToString();
        restTimeText.text = restTime.ToString();
    }

    int GetPlayerShipAmount()
    {
        playerShipAmount = playerShips.Length - 1; // 배열에 FakeShip을 추가했기에 1을 뺍니다.
        return playerShipAmount;
    }
    void SetGamePlayManagerData() 
    {
        playerSelectSpaceShipNumber = GamePlayManager.Instance.PlayerShipNum;
        blackHawk = int.Parse(AccountInfo.Instance.BlackHawk); 
        raptor = int.Parse(AccountInfo.Instance.Raptor);
        myFuel = int.Parse(AccountInfo.Instance.Fuel);
        restTime = int.Parse(AccountInfo.Instance.RestTime);
    }

    void SetSpaceShipStatus()
    {
        for (int i = 0; i < playerShips.Length; i++)
        {
            status.Add(playerShips[i].GetComponent<SpaceShipStatus>().staus);
        }
    }

    void ChangeStatusText(int choiceNum)
    {
        spaceShipSelectNumText.text = "(" + playerSelectSpaceShipNumber + " / " + playerShipAmount + ")";
        spaceShipNameText.text = status[playerSelectSpaceShipNumber].SpaceShipName;
        spaceShipSpeedText.text = status[playerSelectSpaceShipNumber].Speed.ToString();
    }

    void OnLifeImage(int choiceNum)
    {
        for(int i = 0; i < status[choiceNum].Life; i++)
        {
            lifeImages[i].SetActive(true);
        }
    }

    void OffLifeImage()
    {
        for (int i = 0; i < lifeImages.Length; i++)
        {
            lifeImages[i].SetActive(false);
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
        buyButton.SetActive(false);
        startButton.SetActive(false);
        fuelImage.SetActive(false);

        switch (choiceNum)
        {
            case 1:
                DisplayStart();
                break;

            case 2:
                if (blackHawk == 1)             
                    DisplayStart();
                else
                    DisplayLock(choiceNum);
                break;

            case 3:
                if (raptor == 1)
                    DisplayStart();
                else
                    DisplayLock(choiceNum);
                break;
            default:
                break;
        }
    }

    void DisplayLock(int choiceNum)
    {
        fuelImage.SetActive(true);
        buyButton.SetActive(true);
        lockImage.SetActive(true);
        noticeText.text = status[choiceNum].SpaceShipPrice.ToString(); //구조체로 수정함
    }
    void DisplayStart()
    {
        startButton.SetActive(true);
        lockImage.SetActive(false);
        noticeText.text = "출격 가능합니다.";
    }

    public void ShipSelectButtonClick(int click) //
    {
        // click = -1 왼쪽 버튼, click = +1 오른쪽 버튼 판단
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
            warningPopUpPanel.SetActive(true);
        else
        {
            myFuel = myFuel - status[playerSelectSpaceShipNumber].SpaceShipPrice;
            ChangeSpaceShipData(playerSelectSpaceShipNumber);
            CheckSpaceShipLock(playerSelectSpaceShipNumber);

            AccountInfo.ChangeFuelData(myFuel);
            SaveSeletedSpaceShipNumber();
            ChangeInventory();
        }
    }

    bool CheckEnoughFuel(int choiceNum) //가진 재화가 선택한 기체의 재화보다 많은가?
    {
        if (myFuel < status[choiceNum].SpaceShipPrice) //재화 부족
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
            case 3: AccountInfo.ChangeRaptorData(1);
                raptor = raptor + 1;
                break;
            default:
                break;
        }
    }

    public void OnClickedChangeSceneToSelectStageButton() //스타트 버튼 온클릭
    {
        SceneManager.LoadScene("SelectStage");
    }
}
