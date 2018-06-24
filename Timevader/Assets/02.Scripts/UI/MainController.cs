using UnityEngine;
using UnityEngine.UI;


public class MainController : MonoBehaviour {
    [SerializeField]
    Text fuelScoreText;
    [SerializeField]
    Text restTimeText;
    [SerializeField]
    Text welcomeText;
    
    [SerializeField]
    GameObject[] spaceShips;

    int playerSelectSpaceShipNumber;
    string userNickname;

    [SerializeField]
    string myFuel;
    int restTime;
    int stageData;

    // Use this for initialization
    void Start()
    {
        //수정중//
        playerSelectSpaceShipNumber = GamePlayManager.Instance.PlayerShipNum;
        userNickname = GamePlayManager.Instance.PlayerName;

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
        fuelScoreText.text = myFuel;
        restTimeText.text = restTime.ToString();
        welcomeText.text = userNickname + "님 환영합니다.";
    }

    void HideSpaceShips()
    {
        for(int i = 0; i < spaceShips.Length; i++)
        {
            spaceShips[i].SetActive(false);
        }
    }

    void ShowSpaceShip()
    {
        switch (playerSelectSpaceShipNumber)
        {
            case 1: spaceShips[0].SetActive(true); break;
            case 2: spaceShips[1].SetActive(true); break;
            case 3: spaceShips[2].SetActive(true); break;
            default: break;
        }
    }
}
