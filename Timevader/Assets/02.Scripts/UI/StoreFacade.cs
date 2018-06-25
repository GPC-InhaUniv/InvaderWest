using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DataBoolean
{
    FALSE = 0,
    TRUE = 1,
}


public class StoreFacade : MonoBehaviour
{
    enum ItemPrice
    {
        AddMissileItemPrice = 250,
        AssistantItemPrice = 300,
        LastBombItemPrice = 500,
    }
    float FIRSTITEMRANGE = 0.125f;
    float SECONDITEMRANGE = 0.375f;
    float THIRDITEMRANGE = 0.625f;
    float FOURTHITEMRANGE = 0.875f;

    //재화관련//
    public Text MyTimeText;
    public Text MyFuelText;

    //인벤토리관련//
    [SerializeField]
    List<Image> MyInventroy = new List<Image>();
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
    Color32 white = new Color32(255, 255, 255, 255);


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

        if (ItemScrollRect.horizontalNormalizedPosition < FIRSTITEMRANGE)
        {
            ItemExplain.text = "비행선에 보조 미사일을 추가합니다.";
            ItemScrollRect.horizontalNormalizedPosition = 0.0f;
            if (addMissileItem == (int)DataBoolean.FALSE)
            {
                BuyExPlainText.text = "필요한 연료량 " + (int)ItemPrice.AddMissileItemPrice + " 지금 바로 구입하세요";
                BuyButton.interactable = true;
            }
            else
            {
                BuyExPlainText.text = "이미 보유중 입니다.";
                BuyButton.interactable = false;
            }
        }
        else if (ItemScrollRect.horizontalNormalizedPosition >= FIRSTITEMRANGE
                 && ItemScrollRect.horizontalNormalizedPosition < SECONDITEMRANGE)
        {
            ItemExplain.text = "비행선에 보조 함선을 추가해 피해를 막아줍니다.";
            ItemScrollRect.horizontalNormalizedPosition = 0.25f;
            if (assistantItem == (int)DataBoolean.FALSE)
            {
                BuyExPlainText.text = "필요한 연료량 " + (int)ItemPrice.AssistantItemPrice + " 지금 바로 구입하세요";
                BuyButton.interactable = true;
            }
            else
            {
                BuyExPlainText.text = "이미 보유중 입니다.";
                BuyButton.interactable = false;
            }
        }
        else if (ItemScrollRect.horizontalNormalizedPosition >= SECONDITEMRANGE
                 && ItemScrollRect.horizontalNormalizedPosition < THIRDITEMRANGE)
        {
            ItemExplain.text = "보스를 제외한 모든 적을 섬멸 합니다.";
            ItemScrollRect.horizontalNormalizedPosition = 0.5f;
            if (lastBombItem == (int)DataBoolean.FALSE)
            {
                BuyExPlainText.text = "필요한 연료량 " + (int)ItemPrice.LastBombItemPrice + " 지금 바로 구입하세요";
                BuyButton.interactable = true;
            }
            else
            {
                BuyExPlainText.text = "이미 보유중 입니다.";
                BuyButton.interactable = false;
            }
        }
        else if (ItemScrollRect.horizontalNormalizedPosition >= THIRDITEMRANGE
                 && ItemScrollRect.horizontalNormalizedPosition < FOURTHITEMRANGE)
        {
            ItemExplain.text = "추후 아이템 추가 예정 입니다.";
            BuyExPlainText.text = "추후 아이템 추가 예정 입니다.";
        }
        else
        {
            ItemExplain.text = "추후 아이템 추가 예정 입니다.";
            BuyExPlainText.text = "추후 아이템 추가 예정 입니다.";
        }
    }

    public void OnBoughtItem()
    {
        if (ItemScrollRect.horizontalNormalizedPosition < FIRSTITEMRANGE)
        {
            if (addMissileItem == (int)DataBoolean.FALSE && myFuel >= 50)
            {
                BuyAddMissileItem((int)ItemPrice.AddMissileItemPrice, (int)DataBoolean.TRUE);
                ChangeValue();
            }
            else
                Debug.Log("이미 보유중입니다.");
        }
        else if (ItemScrollRect.horizontalNormalizedPosition >= FIRSTITEMRANGE
                 && ItemScrollRect.horizontalNormalizedPosition < SECONDITEMRANGE)
        {
            if (assistantItem == (int)DataBoolean.FALSE && myFuel >= 50)
            {
                BuyAssistantItem((int)ItemPrice.AssistantItemPrice, (int)DataBoolean.TRUE);
                ChangeValue();
            }
            else
                Debug.Log("이미 보유중입니다.");
        }
        else if (ItemScrollRect.horizontalNormalizedPosition >= SECONDITEMRANGE
                 && ItemScrollRect.horizontalNormalizedPosition < THIRDITEMRANGE)
        {
            if (lastBombItem == (int)DataBoolean.FALSE && myFuel >= 50)
            {
                BuyLastBombItem((int)ItemPrice.LastBombItemPrice, (int)DataBoolean.TRUE);
                ChangeValue();
            }
            else
                Debug.Log("이미 보유중입니다.");
        }
        else if (ItemScrollRect.horizontalNormalizedPosition >= THIRDITEMRANGE
                 && ItemScrollRect.horizontalNormalizedPosition < FOURTHITEMRANGE)
        {
            if (raptor == (int)DataBoolean.FALSE && myFuel >= 50)
                ChangeValue();
            else
                Debug.Log("이미 보유중입니다.");
        }
        else
        {
            if (blackHawk == (int)DataBoolean.FALSE && myFuel >= 50)
                ChangeValue();
            else
                Debug.Log("이미 보유중입니다.");
        }
    }
    void CheckInventory()
    {
        MyFuelText.text = myFuel.ToString();
        MyTimeText.text = restTime.ToString();

        if (addMissileItem == (int)DataBoolean.FALSE)
            MyInventroy[0].gameObject.SetActive(false);
        else
            MyInventroy[0].gameObject.SetActive(true);

        if (assistantItem == (int)DataBoolean.FALSE)
            MyInventroy[1].gameObject.SetActive(false);
        else
            MyInventroy[1].gameObject.SetActive(true);

        if (lastBombItem == (int)DataBoolean.FALSE)
            MyInventroy[2].gameObject.SetActive(false);
        else
            MyInventroy[2].gameObject.SetActive(true);
    }
    void BuyAddMissileItem(int price, int itemCount)
    {
        myFuel -= price;
        addMissileItem = itemCount;
        AccountInfo.ChangeFuelData(myFuel);
        AccountInfo.ChangeAddMissileItemData(1);
        CheckInventory();
    }
    void BuyAssistantItem(int price, int itemCount)
    {
        myFuel -= price;
        assistantItem = itemCount;
        AccountInfo.ChangeFuelData(myFuel);
        AccountInfo.ChangeAssistantItemData(1);
        CheckInventory();
    }
    void BuyLastBombItem(int price, int itemCount)
    {
        myFuel -= price;
        lastBombItem = itemCount;
        AccountInfo.ChangeFuelData(myFuel);
        AccountInfo.ChangeLastBombItemData(1);
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
    public void OnItem1Clicked()
    {
        Color32 magenta = new Color32(255, 255, 0, 255);
        MyInventroy[0].color = magenta;
        MyInventroy[1].color = white;
        MyInventroy[2].color = white;
        MyInventoryExplainText.text = "보조  미사일  장착";
    }
    public void OnItem2Clicked()
    {
        Color32 skyblue = new Color32(100, 100, 255, 255);
        MyInventroy[0].color = white;
        MyInventroy[1].color = skyblue;
        MyInventroy[2].color = white;
        MyInventoryExplainText.text = "보조  함선  장착";
    }
    public void OnItem3Clicked()
    {
        Color32 yellow = new Color32(250, 150, 150, 255);
        MyInventroy[0].color = white;
        MyInventroy[1].color = white;
        MyInventroy[2].color = yellow;
        MyInventoryExplainText.text = "최후의  무기  장착";
    }
}
