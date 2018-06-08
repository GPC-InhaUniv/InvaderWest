using System.Collections;
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





    //수정한사람 황윤우 //

    public Text FuelScoreText;
    public Text RestTimeText;

    private int myFuel = int.Parse(AccountInfo.Instance.Fuel);
    private int resttime = int.Parse(AccountInfo.Instance.Time);

    private void CheckInventory()
    {

        FuelScoreText.text = myFuel.ToString();
        RestTimeText.text = resttime.ToString();


    }


        //수정한사람 황윤우 //


        private void Awake()
    {
        GetPlayerShipAmount();
        GetSpaceShipStatus();
    }

    void Start ()
    {
        ChangeStatusText(ChoiceValue);
        OnLifeImage(ChoiceValue);

        //수정한사람 황윤우 //
        CheckInventory();
        //수정한사람 황윤우 //
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
        if (playerSelectSpaceShipNumber == ChoiceValue)
        {
            return;
        }
        if (playerSelectSpaceShipNumber >= playerShip.Length - ChoiceValue) //3-1= 2
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
    public void ShipSelectRightButtonClick()
    {
        if (playerSelectSpaceShipNumber == playerShip.Length - ChoiceValue)
        {
            return;
        }
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

}
