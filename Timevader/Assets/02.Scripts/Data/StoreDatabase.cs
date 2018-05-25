using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class StoreDatabase : MonoBehaviour
{
    private static StoreDatabase instance;
    public static StoreDatabase Instance
    {
        get { return instance; }
        set { instance = value; }
    }

    [SerializeField]
    private List<CatalogItem> catalogPotion;
    public List<CatalogItem> CatalogPotion
    {
        get { return catalogPotion; }
        set { catalogPotion = value; }
    }
    //연습용 포션데이터 가져오기//
    //[SerializeField]
    //private List<Potion> potion;
    //public List<Potion> Potion
    //{
    //    get { return potion; }
    //    set { potion = value; }
    //}
    [SerializeField]
    private int[] cost;
    public int[] Cost
    {
        get { return cost; }
        set { cost = value; }
    }

    private void Awake()
    {
        if (instance != this)
            instance = this;

        DontDestroyOnLoad(gameObject);
    }

    //Playfab에 등록한Catalogitem가져오기(상점 구현에 이용)
    //public static void UpdateDatabase()
    //{
    //    GetCatalogItemsRequest request = new GetCatalogItemsRequest()
    //    {
    //        CatalogVersion = GameConstant.CATALOG_VERISION,
    //    };

    //    PlayFabClientAPI.GetCatalogItems(request, OnUpdateDatabase, ManagerFuncion.OnAPIError);
    //}
    //Playfab에 등록한Catalogitem가져오기(상점 구현에 이용)
    //static void OnUpdateDatabase(GetCatalogItemsResult result)
    //{
    //    for (int i = 0; i < result.Catalog.Count; i++)
    //    {
    //        if (result.Catalog[i].ItemClass == GameConstant.ITEM_POTION)
    //        {
    //            Instance.CatalogPotion.Add(result.Catalog[i]);
    //            Instance.Potion.Add(ManagerFuncion.CreatedPotion(result.Catalog[i]));


    //            //아이템 가격 및 정보 처리는 여기에서//
    //            //Debug.Log(DataBase.Instance.Potion.Count);
    //            //Debug.Log(instance.potion[i].Cost);
    //            Instance.Cost[i] = Instance.potion[i].Cost;

    //        }
    //    }
    //}

}
