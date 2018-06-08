using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;


public static class ManagerFuncion
{
    public static void ChangeMenu(GameObject[] menus, int id)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive(i == id ? true : false);
        }
    }
    //public static Potion CreatedPotion(CatalogItem item)
    //{
    //    Potion potion = new Potion();
    //    potion.Cost = int.Parse(GetCatalogCustomData(GameConstant.ITEM_COST, item));
    //    potion.Name = item.DisplayName;

    //    return potion;
    //}

    public static string GetCatalogCustomData(int i, CatalogItem item)
    {
        //Debug.Log(item.CustomData);
        string cDataTemp = item.CustomData.Trim();
        cDataTemp = cDataTemp.TrimStart('{');
        cDataTemp = cDataTemp.TrimEnd('}');
        string[] newCData;
        newCData = cDataTemp.Split(',', ':');

        for (int s = 0; s < newCData.Length; s++)
        {
            if (i == s)
            {
                newCData[s] = newCData[s].Trim();
                newCData[s] = newCData[s].TrimStart('"');
                newCData[s] = newCData[s].TrimEnd('"');
                newCData[s] = newCData[s].Trim();
                return newCData[s];
            }
        }
        Debug.Log(string.Format("GetCatalogCustomData - could not find ID: {0} in {1}", i, item.DisplayName));
        return "ERROR";
    }
    //에러//
    public static void OnAPIError(PlayFabError error)
    {
        Debug.Log("Error");
        Debug.LogError(error);

    }
}
