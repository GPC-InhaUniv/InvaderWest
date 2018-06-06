﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipChoiceNum : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private Text SpaceShipSelectNumText;

    [SerializeField]
    private Text SpaceShipNameText;

    [SerializeField]
    private Text SpaceShipSpeedText;

    [SerializeField]
    private GameObject[] LifeImage;

    private int initialSpaceShipNumber = 1; //우주선 초기값
    private int playerSelectSpaceShipNumber = 1; // 플레이어가 최종으로 고른 값
    private const int ChoiceValue = 1; //버튼 클릭시 빼는 값, 상수
    private int ChoiceNum;


    [SerializeField]
    private GameObject[] playerShip;

    private int playerShipAmount;

    private List<string> playerShipName = new List<string>();
    private List<string> playerShipSpeed = new List<string>();
    private List<int> playerShipLife = new List<int>();


    [Header("Button")]
    [SerializeField]
    private Button RightButton;
    [SerializeField]
    private Button LeftButton;

    
    private void Awake()
    {
        GetPlayerShipAmount();
        GetSpaceShipStatus();
    }

    void Start ()
    {
        ChangeStatusText(ChoiceValue);
        OnLifeImage(ChoiceValue);
    }

      
    void GetPlayerShipAmount()
    {
       playerShipAmount = playerShip.Length - ChoiceValue; //플레이어가 가진 우주선 갯수
    }

    void ChangeStatusText(int choiceNum)
    {
        SpaceShipSelectNumText.text = "(" + choiceNum + " / " + playerShipAmount + ")";
        SpaceShipNameText.text = playerShipName[choiceNum];
        SpaceShipSpeedText.text = playerShipSpeed[choiceNum];
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

    void GetSpaceShipStatus()
    {
        for(int i = 0; i <= playerShipAmount; i++)
        {
            playerShipName.Add(playerShip[i].GetComponent<SpaceShipStatus>().SpaceShipName);
            playerShipSpeed.Add(playerShip[i].GetComponent<SpaceShipStatus>().Speed.ToString());
            playerShipLife.Add(playerShip[i].GetComponent<SpaceShipStatus>().Life);
        }

    }

    public void ShipSelectLeftButtonClick()
    {
        if ( LeftButton.tag == "LeftButton")
        {
            if(playerSelectSpaceShipNumber == 1)
            {
                return;
            }
            if (playerSelectSpaceShipNumber >= 2)
            {
                Debug.Log("왼쪽 버튼 눌림");
                OffLifeImage();
                playerShip[playerSelectSpaceShipNumber].SetActive(false); //버튼 누른 당시 화면 우주선 사라짐
                ChoiceNum = playerSelectSpaceShipNumber - ChoiceValue;
                playerSelectSpaceShipNumber = ChoiceNum;
                playerShip[ChoiceNum].SetActive(true);

                ChangeStatusText(ChoiceNum);
                OnLifeImage(ChoiceNum);

                GamePlayManager.Instance.PlayerShipNum = playerSelectSpaceShipNumber;
            }
        }
        else if (LeftButton.tag == "LeftButton" && initialSpaceShipNumber == 0)
        {
            Debug.Log("플레이어 선택 오류입니다");
        }
    }
    public void ShipSelectRightButtonClick()
    {
        if (playerSelectSpaceShipNumber == playerShip.Length-ChoiceValue)
        {
            return;
        }
        if (RightButton.tag == "RightButton")
        {
            if (playerSelectSpaceShipNumber >= 1)
            {
                Debug.Log("오른쪽 버튼 눌림");
                OffLifeImage();
                playerShip[playerSelectSpaceShipNumber].SetActive(false); //버튼 누른 당시 화면 우주선 사라짐
                ChoiceNum = playerSelectSpaceShipNumber + ChoiceValue;
                playerSelectSpaceShipNumber = ChoiceNum;
                playerShip[ChoiceNum].SetActive(true);

                ChangeStatusText(ChoiceNum);
                OnLifeImage(ChoiceNum);

                GamePlayManager.Instance.PlayerShipNum = playerSelectSpaceShipNumber;
            }
        }
        else if (RightButton.tag == "RightButton" && initialSpaceShipNumber == 0)
        {
            Debug.Log("플레이어 선택 오류입니다");
        }

    }

}
