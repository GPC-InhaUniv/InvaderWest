using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
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

    public void Login()
    {
        AccountInfo.Login(loginUsername.text, loginUserPassword.text);
    }
    public void Register()
    {
        if (RegisterConfirmPassword.text == RegisterPassword.text)
            AccountInfo.Register(RegisterUsername.text, RegisterEmail.text, RegisterPassword.text);
        else
            Debug.LogError("Password do not match");
    }
    public void ChangeMenu(int i)
    {
        ManagerFuncion.ChangeMenu(menus.ToArray(), i);
    }


}
