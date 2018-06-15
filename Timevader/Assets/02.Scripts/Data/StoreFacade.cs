using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DataBoolean
{
    FALSE =0,
    TRUE =1,
} 

public class StoreFacade : MonoBehaviour {

    //재화관련//
    public Text MyTimeText;
    public Text MyFuelText;


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

    private int myFuel;
    private int addMissileitem;
    private int assistantitem;
    private int lastBombitem;
    private int raptor;
    private int blackHawk;
    private int restTime;

    private void Start()
    {

        myFuel = int.Parse(AccountInfo.Instance.Fuel);
        addMissileitem = int.Parse(AccountInfo.Instance.AddMissileitem);
        assistantitem = int.Parse(AccountInfo.Instance.Assistantitem);
        lastBombitem = int.Parse(AccountInfo.Instance.LastBombitem);
        raptor = int.Parse(AccountInfo.Instance.Raptor);
        blackHawk = int.Parse(AccountInfo.Instance.BlackHawk);
        restTime = int.Parse(AccountInfo.Instance.RestTime);
        MyInventoryExplainText.text = " ";
        
        CheckInventory();
 
        ChangeValue();

    }


    public void ChangeValue()
    {
        if (ItemScrollRect.horizontalNormalizedPosition < 0.125f)
        {
            ItemExplain.text = "Item1 is AddMissileitem";
            ItemScrollRect.horizontalNormalizedPosition = 0.0f;
            if (addMissileitem == (int)DataBoolean.FALSE)
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
            if (assistantitem == (int)DataBoolean.FALSE) 
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
            if (lastBombitem == (int)DataBoolean.FALSE) 
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
            ItemExplain.text = "Item4 Will be added later";
            ItemScrollRect.horizontalNormalizedPosition = 0.75f;
            if (raptor == (int)DataBoolean.FALSE)  
            {
                BuyExPlainText.text = "Item4 Will be added later";

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
            ItemExplain.text = "Item5 Will be added later";
            ItemScrollRect.horizontalNormalizedPosition = 1.0f;
            if (blackHawk == (int)DataBoolean.FALSE) 
            {
                BuyExPlainText.text = "Item5 Will be added later";

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


    public void BuyItem()
    {
        if (ItemScrollRect.horizontalNormalizedPosition < 0.125f)
        {
            if (addMissileitem == (int)DataBoolean.FALSE && myFuel >= 50)
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
            if (assistantitem == (int)DataBoolean.FALSE && myFuel >= 50)
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
            if (lastBombitem == (int)DataBoolean.FALSE && myFuel >= 50)
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
            if (raptor == (int)DataBoolean.FALSE && myFuel >= 50) 
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
            if (blackHawk == (int)DataBoolean.FALSE && myFuel >= 50)
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

        if (addMissileitem == (int)DataBoolean.FALSE) 
            MyInventroy1.gameObject.SetActive(false);
        else
            MyInventroy1.gameObject.SetActive(true);

        if (assistantitem == (int)DataBoolean.FALSE) 
            MyInventroy2.gameObject.SetActive(false);
        else
            MyInventroy2.gameObject.SetActive(true);

        if (lastBombitem == (int)DataBoolean.FALSE) 
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
}
