using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainController : MonoBehaviour {
    [SerializeField]
    Text FuelScoreText;
    [SerializeField]
    Text RestTimeText;
    [SerializeField]
    Text WelcomeText;


    [SerializeField]
    GameObject[] SpaceShips;

    int playerSelectSpaceShipNumber;
    string UserNickname;

    [SerializeField]
    string myFuel;
    int restTime;
    int stageData;

    // Use this for initialization
    void Start()
    {
        
        //수정중//
        playerSelectSpaceShipNumber = GamePlayManager.Instance.PlayerShipNum;
        UserNickname = GamePlayManager.Instance.PlayerName;


        stageData = int.Parse(AccountInfo.Instance.StageData);
        Debug.Log(stageData);

        myFuel = AccountInfo.Instance.Fuel;
        restTime = int.Parse(AccountInfo.Instance.RestTime);

        DisplayText();
        HideSpaceShips();
        ShowSpaceShip();
    }

    void DisplayText()
    {
        FuelScoreText.text = myFuel;
        RestTimeText.text = restTime.ToString();
        WelcomeText.text = UserNickname + "님 환영합니다.";
    }

    void FixedUpdate()
    {
       
    }

    void HideSpaceShips()
    {
        for(int i = 0; i < SpaceShips.Length; i++)
        {
            SpaceShips[i].SetActive(false);
        }
    }

    void ShowSpaceShip()
    {
        switch (playerSelectSpaceShipNumber)
        {
            case 1:
                SpaceShips[0].SetActive(true);

                break;
            case 2:
                SpaceShips[1].SetActive(true);
                break;
            case 3:
                SpaceShips[2].SetActive(true);

                break;
            default:
                break;
        }
    }

}
