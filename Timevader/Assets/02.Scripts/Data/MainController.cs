using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainController : MonoBehaviour {
    [SerializeField]
    private Text FuelScoreText;
    [SerializeField]
    private Text RestTimeText;
    [SerializeField]
    private Text WelcomeText;


    [SerializeField]
    private GameObject[] SpaceShips;

    private int playerSelectSpaceShipNumber;
    private string UserNickname;

    [SerializeField]
    private string myFuel;
    private string resttime;

    // Use this for initialization
    void Start()
    { 

        UserNickname = GamePlayManager.Instance.PlayerName;
        playerSelectSpaceShipNumber = GamePlayManager.Instance.PlayerShipNum;
        myFuel = AccountInfo.Instance.Fuel;
        resttime = AccountInfo.Instance.Time;

        DisplayText();
        HideSpaceShips();
        ShowSpaceShip();
    }

    void DisplayText()
    {
        FuelScoreText.text = myFuel;
        RestTimeText.text = resttime;
        WelcomeText.text = UserNickname + "님 환영합니다.";
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
