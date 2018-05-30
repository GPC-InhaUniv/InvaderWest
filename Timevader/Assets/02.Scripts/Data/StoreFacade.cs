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

    public ScrollRect ShopItem;

    public void Update()
    {
        ShopItem.horizontalNormalizedPosition = 0.5f;
        Debug.Log(ShopItem.horizontalNormalizedPosition);
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
