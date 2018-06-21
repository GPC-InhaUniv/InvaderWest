using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System.Text.RegularExpressions;
public class LogInManager : MonoBehaviour
{
    //로그인창 , 등록창 전환//
    [SerializeField]
    List<GameObject> menus = new List<GameObject>();

    [SerializeField]
    InputField loginUsername;
    [SerializeField]
    InputField loginUserPassword;
    [SerializeField]
    InputField RegisterUsername;
    [SerializeField]
    InputField RegisterEmail;
    [SerializeField]
    InputField RegisterPassword;
    [SerializeField]
    InputField RegisterConfirmPassword;

    public GameObject ErrorPanel;
    //로그인//
    public void Login()
    {
        AccountInfo.Login(loginUsername.text, loginUserPassword.text);
        GamePlayManager.Instance.PlayerName = loginUsername.text;
    }
    //등록//
    public void Register()
    {
        ////@"(?x) 공백제거
        //^(?=.*  시작해서 일자로쭉읽고
        //(\d|  숫자
        //\p{P}| 기호
        //\p{S})) 문장기호
        //.{6,}" 6개이상
        string minString = @"(?x)^(?=.*(\d|\p{P}|\p{S})).{6,}";

        if (Regex.IsMatch(RegisterUsername.text, minString) && Regex.IsMatch(RegisterPassword.text, minString))
        {
            if (RegisterConfirmPassword.text == RegisterPassword.text)
            {
                AccountInfo.Register(RegisterUsername.text, RegisterEmail.text, RegisterPassword.text);
                ManagerFuncion.ChangeMenu(menus.ToArray(), 0);
            }
            else
                Debug.LogError("Password do not match");
        }
        else
            ShowErrorMessange();
    }
    public void ChangeMenu(int change)
    {
        // change = 0   -> Login Panal
        // change = 1   -> Register Panal
        ManagerFuncion.ChangeMenu(menus.ToArray(), change);
    }
    //Register 완료,오류 필드초기화//
    public void ClearInputField()
    {
        RegisterUsername.text = "";
        RegisterEmail.text = "";
        RegisterPassword.text = "";
        RegisterConfirmPassword.text = "";
    }
    //에러메세지 표시//
    void ShowErrorMessange()
    {
        ErrorPanel.gameObject.SetActive(true);
    }
    //등록 완료시 Login창으로 돌아가기//
    public void ReturnRegisterPanel()
    {
        ErrorPanel.gameObject.SetActive(false);
    }
}