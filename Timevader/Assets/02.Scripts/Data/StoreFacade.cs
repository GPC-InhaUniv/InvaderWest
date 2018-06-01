using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreFacade : MonoBehaviour {
    //재화관련//
    public Text MyTimeText;
    public Text MyFuelText;

    private int MyFuel;
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


    private void Start()
    {
        MyInventoryExplainText.text = " ";

        MyFuel = int.Parse(AccountInfo.Instance.Fuel);

        CheckInventory();

        ChangeValue();

    }

    public void ChangeValue()
    {
        if (ItemScrollRect.horizontalNormalizedPosition < 0.125f)
        {
            ItemExplain.text = "Item1 is AddMissileitem";
            ItemScrollRect.horizontalNormalizedPosition = 0.0f;
            if (AccountInfo.Instance.AddMissileitem == "0")
            {
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
            if (AccountInfo.Instance.Assistantitem == "0")
            {
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
            if (AccountInfo.Instance.LastBombitem == "0")
            {
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
            if (AccountInfo.Instance.Raptor == "0")
            {
                BuyButton.interactable = true;
            }
            else
            {
                BuyExPlainText.text = "이미 보유중 입니다.";
                BuyButton.interactable = false;
            }
        }
        if (ItemScrollRect.horizontalNormalizedPosition >= 0.875)
        {
            ItemExplain.text = "Item5 is BlackHawk playership";
            ItemScrollRect.horizontalNormalizedPosition = 1.0f;
            if (AccountInfo.Instance.BlackHawk == "0")
            {
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
        horizonval = ItemScrollRect.horizontalNormalizedPosition;
    }

    public void BuyItem()
    {
        if (ItemScrollRect.horizontalNormalizedPosition < 0.125f)
        {
            BuyAddMissileitem(50);
        }
        if (ItemScrollRect.horizontalNormalizedPosition == 0.25f)
        {
            BuyAssistantitem(50);
        }
        if (ItemScrollRect.horizontalNormalizedPosition == 0.5f)
        {
            BuyAssistantitem(50);
        }
        if (ItemScrollRect.horizontalNormalizedPosition == 0.75f)
        {
            BuyRaptor(50);
        }
        if (ItemScrollRect.horizontalNormalizedPosition >= 0.9f)
        {
            BuyBlackHawk(50);
        }
    }
    private void CheckInventory()
    {
        //{"NextStage" , "1" },

        Debug.Log(int.Parse(AccountInfo.Instance.Fuel));
        MyFuelText.text = MyFuel.ToString();
        MyTimeText.text = AccountInfo.Instance.Time;

        if (AccountInfo.Instance.AddMissileitem == "0")
            MyInventroy1.gameObject.SetActive(false);
        else
            MyInventroy1.gameObject.SetActive(true);

        if (AccountInfo.Instance.Assistantitem == "0")
            MyInventroy2.gameObject.SetActive(false);
        else
            MyInventroy2.gameObject.SetActive(true);

        if (AccountInfo.Instance.LastBombitem == "0")
            MyInventroy3.gameObject.SetActive(false);
        else
            MyInventroy3.gameObject.SetActive(true);
    }

    public void BuyAddMissileitem(int price)
    {
            MyFuel = MyFuel - price;
            AccountInfo.ChangeFuelData(MyFuel);

            AccountInfo.ChangeAddMissileitemData(1);
            AccountInfo.GetUserData();
            CheckInventory();
    }
    public void BuyAssistantitem(int price)
    {
        MyFuel = MyFuel - price;
        AccountInfo.ChangeFuelData(MyFuel);

        AccountInfo.ChangeAssistantitemData(1);
        AccountInfo.GetUserData();
        CheckInventory();
    }
    public void BuyLastBombitem(int price)
    {
        MyFuel = MyFuel - price;
        AccountInfo.ChangeFuelData(MyFuel);

        AccountInfo.ChangeLastBombitemData(1);
        AccountInfo.GetUserData();
        CheckInventory();
    }
    public void BuyRaptor(int price)
    {
        MyFuel = MyFuel - price;
        AccountInfo.ChangeFuelData(MyFuel);

        AccountInfo.ChangeRaptoritemData(1);
        AccountInfo.GetUserData();
        CheckInventory();
    }
    public void BuyBlackHawk(int price)
    {
        MyFuel = MyFuel - price;
        AccountInfo.ChangeFuelData(MyFuel);

        AccountInfo.ChangeBlackHawkData(1);
        AccountInfo.GetUserData();
        CheckInventory();
    }



}
