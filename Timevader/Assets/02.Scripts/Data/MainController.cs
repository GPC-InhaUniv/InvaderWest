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
    private GameObject[] SpaceShips;

    private int playerSelectSpaceShipNumber;
    //[SerializeField]
    //private int myFuel = int.Parse(AccountInfo.Instance.Fuel);
    //private int resttime = int.Parse(AccountInfo.Instance.Time);



    // Use this for initialization
    void Start () {

        FuelScoreText.text = AccountInfo.Instance.Fuel;
        RestTimeText.text = AccountInfo.Instance.RestTime;

        playerSelectSpaceShipNumber = GamePlayManager.Instance.PlayerShipNum;
        HideSpaceShips();
        ShowSpaceShip();
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
