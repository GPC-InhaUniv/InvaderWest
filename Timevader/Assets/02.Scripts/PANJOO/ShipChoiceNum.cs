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

    private int playerSelectSpaceShipNumber=1; // 초기값 or 플레이어가 고른 값 

    private const int ChoiceValue = 1; //버튼 클릭시 빼는 값, 상수

    private int ChoiceNum;

    private int playerShipAmount;

    private List<string> playerShipName = new List<string>();
    private List<string> playerShipSpeed = new List<string>();
    private List<int> playerShipLife = new List<int>();
    private List<int> playerShipPrice = new List<int>();
    private List<bool> playerShipIsProperty = new List<bool>();

    //수정한사람 황윤우 //
    [Header("Time and Fuel")]
    [SerializeField]
    private Text FuelScoreText;
    [SerializeField]
    private Text RestTimeText;

    private int myFuel;
    private int restTime;

    void ChangeInventory()
    {
        FuelScoreText.text = myFuel.ToString();
        RestTimeText.text = restTime.ToString();
    }
    //수정한사람 황윤우 //

    void Awake()
    {
        GetPlayerShipAmount();
        GetSpaceShipStatus();
    }

    void Start ()
    {
        myFuel = int.Parse(AccountInfo.Instance.Fuel);
        restTime = int.Parse(AccountInfo.Instance.RestTime);
        ChangeInventory();

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
            playerShipIsProperty.Add(playerShip[i].GetComponent<SpaceShipStatus>().IsProperty);
        }
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

    void CheckSpaceShipLock(int choiceNum)
    {
        BuyButton.SetActive(false);
        StartButton.SetActive(false);
        FuelImage.SetActive(false);
        
        if (playerShipIsProperty[choiceNum]==false)//소유하지 않았다면?
        {
            FuelImage.SetActive(true);
            BuyButton.SetActive(true);
            CheckEnoughFuel(choiceNum);
            LockImage.SetActive(true);
            NoticeText.text = playerShipPrice[choiceNum].ToString();
        }
        else
        {
            StartButton.SetActive(true);
            CheckEnoughFuel(choiceNum);
            LockImage.SetActive(false);
            NoticeText.text = "출격 가능합니다.";
        }
    }

    bool CheckEnoughFuel(int choiceNum) //가진 재화가 선택한 기체의 재화보다 많은가?
    {
        if (myFuel < playerShipPrice[choiceNum]) //여기서 오류남 인덱스 넘어감
        {
            return false;
        }
        else
        {          
            return true;
        }
    }

    void SaveSeletedSpaceShipNumber(int choiceNum)
    {
        if (!playerShipIsProperty[choiceNum])
        {
            return;
        }
        else GamePlayManager.Instance.PlayerShipNum = playerSelectSpaceShipNumber;
    }

 
    public void ShipSelectLeftButtonClick()
    {
        if (playerSelectSpaceShipNumber == ChoiceValue)
        {
            Debug.Log("오류 : << 플레이어 선택이 0");
            return;
        }
        if (playerSelectSpaceShipNumber >= ChoiceValue+1)
        {
            Debug.Log("왼쪽 버튼 눌림");
            OffLifeImage();

            playerShip[playerSelectSpaceShipNumber].SetActive(false); //버튼 누른 당시 화면 우주선 사라짐
            ChoiceNum = playerSelectSpaceShipNumber - ChoiceValue;

            playerSelectSpaceShipNumber = ChoiceNum; //플레이어쉽 선택 넘버에 선택 번호 저장

            playerShip[playerSelectSpaceShipNumber].SetActive(true); //선택된 우주선 보여줌
            CheckSpaceShipLock(playerSelectSpaceShipNumber); //우주선이 소유했는가? (잠금 UI출력)

            ChangeStatusText(playerSelectSpaceShipNumber); //선택된 우주선 스탯을 기반으로 UI출력

            OnLifeImage(playerSelectSpaceShipNumber); //선택된 우주선 기반 라이프 UI 출력

            SaveSeletedSpaceShipNumber(playerSelectSpaceShipNumber); //싱글톤 저장
        }
    }
    public void ShipSelectRightButtonClick()
    {
        if (playerSelectSpaceShipNumber == playerShip.Length-ChoiceValue)
        {
            Debug.Log("오류 : 플레이어 선택 범위 벗어남 >>");
            return;
        }
        if (playerSelectSpaceShipNumber < playerShip.Length - ChoiceValue)
        {
            Debug.Log("오른쪽 버튼 눌림");
            OffLifeImage();

            playerShip[playerSelectSpaceShipNumber].SetActive(false); //버튼 누른 당시 화면 우주선 사라짐
            ChoiceNum = playerSelectSpaceShipNumber + ChoiceValue;

            playerSelectSpaceShipNumber = ChoiceNum; //플레이어쉽 선택 넘버에 선택 번호 저장

            playerShip[playerSelectSpaceShipNumber].SetActive(true); //선택된 우주선 보여줌
            CheckSpaceShipLock(playerSelectSpaceShipNumber); //우주선이 소유했는가? (잠금 UI출력)

            ChangeStatusText(playerSelectSpaceShipNumber); //선택된 우주선 스탯을 기반으로 UI출력

            OnLifeImage(playerSelectSpaceShipNumber); //선택된 우주선 기반 라이프 UI 출력

            SaveSeletedSpaceShipNumber(playerSelectSpaceShipNumber); //싱글톤 저장
        }
    }


    public void ChangeSceneToSelectStage()
    {
        SceneManager.LoadScene("StageSelect");
    }
    public void BuySpaceShip()
    {
        //bool형으로 재화가 많은지 검사
        if (CheckEnoughFuel(playerSelectSpaceShipNumber))
        {
            myFuel -= playerShipPrice[playerSelectSpaceShipNumber];
            playerShip[playerSelectSpaceShipNumber].GetComponent<SpaceShipStatus>().IsProperty = true; //수정 필요 직접 못 설정함..
        }
        else return;
    }
}
