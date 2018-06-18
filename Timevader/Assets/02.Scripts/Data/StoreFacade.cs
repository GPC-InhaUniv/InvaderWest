using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DataBoolean
{
    FALSE =0,
    TRUE =1,
}

public class StoreFacade : MonoBehaviour
{


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
    //구매 확인창//
    public GameObject ConfirmationPanal;

    int myFuel;
    int addMissileItem;
    int assistantItem;
    int lastBombItem;
    int raptor;
    int blackHawk;
    int restTime;

    private void Start()
    {
        //데이터 불러오기//
        myFuel = int.Parse(AccountInfo.Instance.Fuel);
        addMissileItem = int.Parse(AccountInfo.Instance.AddMissileItem);
        assistantItem = int.Parse(AccountInfo.Instance.AssistantItem);
        lastBombItem = int.Parse(AccountInfo.Instance.LastBombItem);
        raptor = int.Parse(AccountInfo.Instance.Raptor);
        blackHawk = int.Parse(AccountInfo.Instance.BlackHawk);
        restTime = int.Parse(AccountInfo.Instance.RestTime);
        MyInventoryExplainText.text = " ";

        CheckInventory();

        ChangeValue();

    }

    //ScrollRect 값 변경시 호출//
    public void ChangeValue()
    {

        if (ItemScrollRect.horizontalNormalizedPosition < 0.125f)
        {
            ItemExplain.text = "Item1 is AddMissileItem";
            ItemScrollRect.horizontalNormalizedPosition = 0.0f;
            if (addMissileItem == (int)DataBoolean.FALSE)
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
            ItemExplain.text = "Item2 is AssistantItem";
            ItemScrollRect.horizontalNormalizedPosition = 0.25f;
            if (assistantItem == (int)DataBoolean.FALSE)
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
            ItemExplain.text = "Item3 is LastBombItem";
            ItemScrollRect.horizontalNormalizedPosition = 0.5f;
            if (lastBombItem == (int)DataBoolean.FALSE)
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


    public void OnBoughtItem()
    {
        if (ItemScrollRect.horizontalNormalizedPosition < 0.125f)
        {
            if (addMissileItem == (int)DataBoolean.FALSE && myFuel >= 50)
            {
                BuyAddMissileItem(50, 1);
                ChangeValue();
            }
            else
            {
                Debug.Log("이미 보유중입니다.");
            }
        }
        else if (ItemScrollRect.horizontalNormalizedPosition >= 0.125f && ItemScrollRect.horizontalNormalizedPosition < 0.375)
        {
            if (assistantItem == (int)DataBoolean.FALSE && myFuel >= 50)
            {
                BuyAssistantItem(50, 1);
                ChangeValue();
            }
            else
            {
                Debug.Log("이미 보유중입니다.");
            }
        }
        else if (ItemScrollRect.horizontalNormalizedPosition >= 0.375f && ItemScrollRect.horizontalNormalizedPosition < 0.625f)
        {
            if (lastBombItem == (int)DataBoolean.FALSE && myFuel >= 50)
            {
                BuyLastBombItem(50, 1);
                ChangeValue();
            }
            else
            {
                Debug.Log("이미 보유중입니다.");
            }
        }
        else if (ItemScrollRect.horizontalNormalizedPosition >= 0.625f && ItemScrollRect.horizontalNormalizedPosition < 0.875f)
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
        else
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

        if (addMissileItem == (int)DataBoolean.FALSE)
            MyInventroy1.gameObject.SetActive(false);
        else
            MyInventroy1.gameObject.SetActive(true);

        if (assistantItem == (int)DataBoolean.FALSE)
            MyInventroy2.gameObject.SetActive(false);
        else
            MyInventroy2.gameObject.SetActive(true);

        if (lastBombItem == (int)DataBoolean.FALSE)
            MyInventroy3.gameObject.SetActive(false);
        else
            MyInventroy3.gameObject.SetActive(true);
    }

     void BuyAddMissileItem(int price, int itemCount)
    {
        myFuel = myFuel - price;
        addMissileItem = itemCount;
        AccountInfo.ChangeFuelData(myFuel);

        AccountInfo.ChangeAddMissileItemData(1);
        CheckInventory();
    }
     void BuyAssistantItem(int price, int itemCount)
    {
        myFuel = myFuel - price;
        assistantItem = itemCount;
        AccountInfo.ChangeFuelData(myFuel);

        AccountInfo.ChangeAssistantItemData(1);
        CheckInventory();


    }
     void BuyLastBombItem(int price, int itemCount)
    {
        myFuel = myFuel - price;
        lastBombItem = itemCount;
        AccountInfo.ChangeFuelData(myFuel);

        AccountInfo.ChangeLastBombItemData(1);
        CheckInventory();


    }
     void BuyRaptor(int price, int itemCount)
    {
        myFuel = myFuel - price;
        raptor = itemCount;
        AccountInfo.ChangeFuelData(myFuel);

        AccountInfo.ChangeRaptorData(1);
        CheckInventory();

    }
     void BuyBlackHawk(int price, int itemCount)
    {
        myFuel = myFuel - price;
        blackHawk = itemCount;
        AccountInfo.ChangeFuelData(myFuel);

        AccountInfo.ChangeBlackHawkData(1);
        CheckInventory();

    }
    public void OnShowConfirmPanal()
    {
        ConfirmationPanal.gameObject.SetActive(true);
    }
    public void OnBackToMenu()
    {
        ConfirmationPanal.gameObject.SetActive(false);

    }

}
