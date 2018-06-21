using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;


public static class ManagerFuncion
{
    //로그인창 메뉴 전환//
    public static void ChangeMenu(GameObject[] menus, int id)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive(i == id ? true : false);
        }
    }
    //PlayFab에러발생시 호출//
    public static void OnAPIError(PlayFabError error)
    {
        Debug.Log("Error");
        Debug.LogError(error);
    }
}
