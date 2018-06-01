using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreFacade : MonoBehaviour {
    //재화관련//
    public Text MyTime;
    public Text MyFuel;
    //인벤토리관련//
    public Image MyInventroy1;
    public Image MyInventroy2;
    public Image MyInventroy3;

    public Text MyInventoryExplain;
    //스크롤 관련//
    public ScrollRect ItemScrollRect;
    public Text ItemExplain;


    private void Start()
    {
        ItemExplain.text = "Item1 is helping player";
        MyInventoryExplain.text = " ";
        MyFuel.text = AccountInfo.Instance.Fuel;

        MyInventroy1.gameObject.SetActive(false);

        Debug.Log(AccountInfo.Instance.Fuel);
    }

    public void ChangeValue()
    {
        if (ItemScrollRect.horizontalNormalizedPosition < 0.15f)
        {
            ItemExplain.text = "Item1 is helping player";
        }
        if (ItemScrollRect.horizontalNormalizedPosition >= 0.15f && ItemScrollRect.horizontalNormalizedPosition < 0.5)
        {
            ItemExplain.text = "Item2 is addingmissile";
        }
        if (ItemScrollRect.horizontalNormalizedPosition >= 0.5f && ItemScrollRect.horizontalNormalizedPosition < 0.85)
        {
            ItemExplain.text = "Item3 is RaptorPlayership";
        }
        if (ItemScrollRect.horizontalNormalizedPosition >= 0.85)
        {
            ItemExplain.text = "Item4 is hawkeyeplayership";
        }
        //0.15
        //0.5
        //0.85

        //Debug.Log(ItemScrollRect.horizontalNormalizedPosition);

    }

    public void Update()
    {

    }

    public void BuyAddMissileitem()
    {

    }
    public void BuyAssistantitem()
    {

    }
    public void BuyLastBombitem()
    {

    }
    public void BuyRaptor()
    {

    }
    public void BuyBlackHawk()
    {

    }

    public void BuyButton()
    {

    }

}
