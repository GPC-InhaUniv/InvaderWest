using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System.Text.RegularExpressions;
public class LogInManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> menus = new List<GameObject>();

    [SerializeField]
    private InputField loginUsername;
    [SerializeField]
    private InputField loginUserPassword;
    [SerializeField]
    private InputField RegisterUsername;
    [SerializeField]
    private InputField RegisterEmail;
    [SerializeField]
    private InputField RegisterPassword;
    [SerializeField]
    private InputField RegisterConfirmPassword;

    public GameObject ErrorPanel;

    public void Login()
    {
        AccountInfo.Login(loginUsername.text, loginUserPassword.text);
        GamePlayManager.Instance.PlayerName = loginUsername.text;
    }
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
        {
            ShowErrorMessange();
        }
    }
    public void ChangeMenu(int i)
    {
        ManagerFuncion.ChangeMenu(menus.ToArray(), i);
    }
    public void ClearInputField()
    {
        RegisterUsername.text = "";
        RegisterEmail.text = "";
        RegisterPassword.text = "";
        RegisterConfirmPassword.text = "";
    }
    void ShowErrorMessange()
    {
        ErrorPanel.gameObject.SetActive(true);
    }
    public void ReturnRegisterPanel()
    {
        ErrorPanel.gameObject.SetActive(false);

    }


}
