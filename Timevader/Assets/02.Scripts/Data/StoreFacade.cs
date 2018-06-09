using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreFacade : MonoBehaviour {
    //재화관련//
    public Text MyTimeText;
    public Text MyFuelText;

    [SerializeField]
    //private int myFuel= int.Parse(AccountInfo.Instance.Fuel);
    //private int addMissileitem = int.Parse(AccountInfo.Instance.AddMissileitem);
    //private int assistantitem = int.Parse(AccountInfo.Instance.Assistantitem);
    //private int lastBombitem = int.Parse(AccountInfo.Instance.LastBombitem);
    //private int raptor = int.Parse(AccountInfo.Instance.Raptor);
    //private int blackHawk = int.Parse(AccountInfo.Instance.BlackHawk);
    //private int bestScore = int.Parse(AccountInfo.Instance.BestScore);
    //private int restTime = int.Parse(AccountInfo.Instance.RestTime);
    //private int nextStage = int.Parse(AccountInfo.Instance.NextStage);
    
    private int myFuel;
    private int addMissileitem;
    private int assistantitem;
    private int lastBombitem;
    private int raptor;
    private int blackHawk;
    private int bestScore;
    private int restTime;
    private int nextStage;
    //인벤토리관련//
    public Image MyInventroy1;
    public Image MyInventroy2;
    public Image MyInventroy3;

    public Text MyInventoryExplainText;
    //스크롤 관련//
    public ScrollRect ItemScrollRect;
    public Text ItemExplain;
    //구입 관련//
    public Text BuyExPlainText;
    public Button BuyButton;


    public float horizonval;

    //옵저버 도전중///
    //[SerializeField]
    //private int fuel, time, addMissileitem, assistantitem, lastBombitem, raptor, blackHawk, bestScore, restTime, nextStage;
    //private ISubjectable subjectable;



    private void Start()
    {

        myFuel = int.Parse(AccountInfo.Instance.Fuel);
        addMissileitem = int.Parse(AccountInfo.Instance.AddMissileitem);
        assistantitem = int.Parse(AccountInfo.Instance.Assistantitem);
        lastBombitem = int.Parse(AccountInfo.Instance.LastBombitem);
        raptor = int.Parse(AccountInfo.Instance.Raptor);
        blackHawk = int.Parse(AccountInfo.Instance.BlackHawk);
        bestScore = int.Parse(AccountInfo.Instance.BestScore);
        restTime = int.Parse(AccountInfo.Instance.RestTime);
        nextStage = int.Parse(AccountInfo.Instance.NextStage);
        MyInventoryExplainText.text = " ";

        CheckInventory();
        //여기서 부르질 않음//
        //AccountInfo.Instance.RegisterObserver(this);
        //AccountInfo.Instance.MeasureChangedData();
        

        ChangeValue();

    }


    public void ChangeValue()
    {
        if (ItemScrollRect.horizontalNormalizedPosition < 0.125f)
        {
            ItemExplain.text = "Item1 is AddMissileitem";
            ItemScrollRect.horizontalNormalizedPosition = 0.0f;
            if (addMissileitem == 0)
            {
                BuyExPlainText.text = "Item1 is very good to you";

                BuyButton.interactable = true;
            }
            else
            {
                BuyExPlainText.text = "이미 보유중 입니다.";
                BuyButton.interactable = false;
            }

        }
        if (ItemScrollRect.horizontalNormalizedPosition >= 0.125f && ItemScrollRect.horizontalNormalizedPosition < 0.375)
        {
            ItemExplain.text = "Item2 is Assistantitem";
            ItemScrollRect.horizontalNormalizedPosition = 0.25f;
            if (assistantitem == 0) 
            {
                BuyExPlainText.text = "Item2 is very good to you";

                BuyButton.interactable = true;
            }
            else
            {
                BuyExPlainText.text = "이미 보유중 입니다.";
                BuyButton.interactable = false;
            }
        }
        if (ItemScrollRect.horizontalNormalizedPosition >= 0.375f && ItemScrollRect.horizontalNormalizedPosition < 0.625f)
        {
            ItemExplain.text = "Item3 is LastBombitem";
            ItemScrollRect.horizontalNormalizedPosition = 0.5f;
            if (lastBombitem == 0) 
            {
                BuyExPlainText.text = "Item3 is very good to you";

                BuyButton.interactable = true;
            }
            else
            {
                BuyExPlainText.text = "이미 보유중 입니다.";
                BuyButton.interactable = false;
            }
        }
        if (ItemScrollRect.horizontalNormalizedPosition >= 0.625f && ItemScrollRect.horizontalNormalizedPosition < 0.875f)
        {
            ItemExplain.text = "Item4 is Raptor playership";
            ItemScrollRect.horizontalNormalizedPosition = 0.75f;
            if (raptor == 0)  
            {
                BuyExPlainText.text = "Item4 is very good to you";

                BuyButton.interactable = true;
            }
            else
            {
                BuyExPlainText.text = "이미 보유중 입니다.";
                BuyButton.interactable = false;
            }
        }
        if (ItemScrollRect.horizontalNormalizedPosition >= 0.875f)
        {
            ItemExplain.text = "Item5 is BlackHawk playership";
            ItemScrollRect.horizontalNormalizedPosition = 1.0f;
            if (blackHawk == 0) 
            {
                BuyExPlainText.text = "Item5 is very good to you";

                BuyButton.interactable = true;
            }
            else
            {
                BuyExPlainText.text = "이미 보유중 입니다.";
                BuyButton.interactable = false;
            }
        }
        //Debug.Log(ItemScrollRect.horizontalNormalizedPosition);

    }
    public void Update()
    {
     //   horizonval = ItemScrollRect.horizontalNormalizedPosition;
    }

    public void BuyItem()
    {
        if (ItemScrollRect.horizontalNormalizedPosition < 0.125f)
        {
            if (addMissileitem == 0)
            {
                BuyAddMissileitem(50, 1);
                ChangeValue();
            }
            else
            {
                Debug.Log("이미 보유중입니다.");
            }
        }
        if (ItemScrollRect.horizontalNormalizedPosition >= 0.125f && ItemScrollRect.horizontalNormalizedPosition < 0.375)
        {
            if (assistantitem == 0)
            {
                BuyAssistantitem(50, 1);
                ChangeValue();

            }
            else
            {
                Debug.Log("이미 보유중입니다.");
            }
        }
        if (ItemScrollRect.horizontalNormalizedPosition >= 0.375f && ItemScrollRect.horizontalNormalizedPosition < 0.625f)
        {
            if (lastBombitem == 0)
            {
                BuyLastBombitem(50, 1);
                ChangeValue();

            }
            else
            {
                Debug.Log("이미 보유중입니다.");
            }
        }
        if (ItemScrollRect.horizontalNormalizedPosition >= 0.625f && ItemScrollRect.horizontalNormalizedPosition < 0.875f)
        {
            if (raptor == 0)
            {
                BuyRaptor(50, 1);
                ChangeValue();

            }
            else
            {
                Debug.Log("이미 보유중입니다.");
            }
        }
        if (ItemScrollRect.horizontalNormalizedPosition >= 0.875f)
        {
            if (blackHawk == 0)
            {
                BuyBlackHawk(50, 1);
                ChangeValue();

            }
            else
            {
                Debug.Log("이미 보유중입니다.");
            }
        }
    }
    private void CheckInventory()
    {

        MyFuelText.text = myFuel.ToString();
        MyTimeText.text = restTime.ToString();

        if (addMissileitem == 0) 
            MyInventroy1.gameObject.SetActive(false);
        else
            MyInventroy1.gameObject.SetActive(true);

        if (assistantitem == 0) 
            MyInventroy2.gameObject.SetActive(false);
        else
            MyInventroy2.gameObject.SetActive(true);

        if (lastBombitem == 0) 
            MyInventroy3.gameObject.SetActive(false);
        else
            MyInventroy3.gameObject.SetActive(true);
    }

    public void BuyAddMissileitem(int price, int itemCount)
    {
        myFuel = myFuel - price;
        addMissileitem = itemCount;
        AccountInfo.ChangeFuelData(myFuel);

        AccountInfo.ChangeAddMissileitemData(1);
        CheckInventory();
    }
    public void BuyAssistantitem(int price, int itemCount)
    {
        myFuel = myFuel - price;
        assistantitem = itemCount;

        AccountInfo.ChangeFuelData(myFuel);

        AccountInfo.ChangeAssistantitemData(1);
        CheckInventory();


    }
    public void BuyLastBombitem(int price, int itemCount)
    {
        myFuel = myFuel - price;
        lastBombitem = itemCount;


        AccountInfo.ChangeFuelData(myFuel);

        AccountInfo.ChangeLastBombitemData(1);
        CheckInventory();


    }
    public void BuyRaptor(int price, int itemCount)
    {
        myFuel = myFuel - price;
        raptor = itemCount;


        AccountInfo.ChangeFuelData(myFuel);

        AccountInfo.ChangeRaptoritemData(1);
        CheckInventory();

    }
    public void BuyBlackHawk(int price, int itemCount)
    {
        myFuel = myFuel - price;
        blackHawk = itemCount;

        AccountInfo.ChangeFuelData(myFuel);

        AccountInfo.ChangeBlackHawkData(1);
        CheckInventory();

    }


    ////옵저버 도전중//
    //public void UpdateData(int fuel, int time, int addMissileitem, int assistantitem, int lastBombitem,
    //                       int raptor, int blackHawk, int bestScore, int restTime, int nextStage)
    //{
    //    this.fuel = fuel;
    //    this.time = time;
    //    this.addMissileitem = addMissileitem;
    //    this.assistantitem = assistantitem;
    //    this.lastBombitem = lastBombitem;
    //    this.raptor = raptor;
    //    this.blackHawk = blackHawk;
    //    this.bestScore = bestScore;
    //    this.restTime = restTime;
    //    this.nextStage = nextStage;

    //    DisPlay();

    //}

    //public void DisPlay()
    //{
    //    MyFuelText.text = fuel.ToString();
    //    MyTimeText.text = time.ToString();

    //    if (addMissileitem == 0)
    //        MyInventroy1.gameObject.SetActive(false);
    //    else
    //        MyInventroy1.gameObject.SetActive(true);

    //    if (assistantitem == 0)
    //        MyInventroy2.gameObject.SetActive(false);
    //    else
    //        MyInventroy2.gameObject.SetActive(true);

    //    if (lastBombitem == 0)
    //        MyInventroy3.gameObject.SetActive(false);
    //    else
    //        MyInventroy3.gameObject.SetActive(true);


    //}

}
