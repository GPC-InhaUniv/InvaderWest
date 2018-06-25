using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class AccountInfo : MonoBehaviour
{
    static AccountInfo instance;
    public static AccountInfo Instance
    {
        get { return instance; }
        set { instance = value; }
    }

    [SerializeField]
    GetPlayerCombinedInfoResultPayload info;
    public GetPlayerCombinedInfoResultPayload Info
    {
        get { return info; }
        set { info = value; }
    }
    
    public string Fuel, LevelOfDifficulty, AddMissileItem, AssistantItem, LastBombItem, Raptor, BlackHawk, BestScore, RestTime, StageData;

    void Awake()
    {
        if (instance != this)
            instance = this;

        DontDestroyOnLoad(gameObject);

        GamePlayManager.Instance.PlayerShipNum = 1;
        Debug.Log(GamePlayManager.Instance.PlayerShipNum);
    }

    //서버에 등록//
    public static void Register(string username, string email, string password)
    {
        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest()
        {
            TitleId = PlayFabSettings.TitleId,
            Email = email,
            Username = username,
            Password = password,
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegister, ManagerFuncion.OnAPIError);
    }
    static void OnRegister(RegisterPlayFabUserResult result)
    {
        Debug.Log("Registered with : " + result.PlayFabId);
    }
    //서버에 로그인//
    public static void Login(string username, string password)
    {
        LoginWithPlayFabRequest request = new LoginWithPlayFabRequest()
        {
            TitleId = PlayFabSettings.TitleId,
            Username = username,
            Password = password,

        };
        PlayFabClientAPI.LoginWithPlayFab(request, OnLogin, ManagerFuncion.OnAPIError);
    }
    static void OnLogin(LoginResult result)
    {
        Debug.Log("Login with : " + result.PlayFabId);
        SetAccountInfo(result.PlayFabId);
    }
    //사용자 계정 정보 셋팅//
    public static void SetAccountInfo(string playfabId)
    {
        GetPlayerCombinedInfoRequestParams paramsInfo = new GetPlayerCombinedInfoRequestParams()
        {
            GetTitleData = true,
            GetUserInventory = true,
            GetUserAccountInfo = true,
            GetUserVirtualCurrency = true,
            GetPlayerProfile = true,
            GetPlayerStatistics = true,
            GetUserData = true,
            GetUserReadOnlyData = true,
        };

        GetPlayerCombinedInfoRequest request = new GetPlayerCombinedInfoRequest()
        {
            PlayFabId = playfabId,
            InfoRequestParameters = paramsInfo,
        };

        PlayFabClientAPI.GetPlayerCombinedInfo(request, OnSetAccountInfo, ManagerFuncion.OnAPIError);
    }
    static void OnSetAccountInfo(GetPlayerCombinedInfoResult result)
    {
        Debug.Log("Updated Account Info!");
        Instance.Info = result.InfoResultPayload;

        SetUserDataInStart();
    }
    //서버에 데이타 셋팅//
    public static void SetUserData()
    {
        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                //재화//
                {"Fuel" , "500" },
                {"LevelOfDifficulty" , "0" },
                //소모성 아이템 종류//
                {"AddMissileItem" , "0" },
                {"AssistantItem" , "0" },
                {"LastBombItem" , "0" },
                //전투기 종류//
                {"Raptor" , "0" },
                {"BlackHawk", "0"},
                //시스템 관련//
                {"BestScore", "0" },
                {"RestTime" , "0" },
                //스테이지 저장//
                {"StageData" , "1" },
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnSetUserDataInLogIn, ManagerFuncion.OnAPIError);
    }
    static void OnSetUserDataInLogIn(UpdateUserDataResult result)
    {
        SetUserDataInStart();
        Debug.Log("Successfully updated user data");
    }

    static void OnUpdateUserDataInShop(UpdateUserDataResult result)
    {
        SetUserDataInShop();
        Debug.Log("Successfully updated user data");
    }
    static void OnUpdateUserDataInGame(UpdateUserDataResult result)
    {
        SetUserDataInGame();
        Debug.Log("Successfully updated user data");
    }

    //재화 데이타 변경 함수// 
    public static void ChangeFuelData(int fuel)
    {
        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {"Fuel", ""+fuel+""},
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnUpdateUserDataInShop, ManagerFuncion.OnAPIError);
    }
    public static void ChangeStageData(int stageData)
    {
        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {"StageData", ""+stageData+""},
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnUpdateUserDataInGame, ManagerFuncion.OnAPIError);
    }
    //소모성 아이템 데이타 변경 함수//
    public static void ChangeAddMissileItemData(int addMissileItem)
    {
        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {"AddMissileItem", ""+addMissileItem+""},
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnUpdateUserDataInShop, ManagerFuncion.OnAPIError);
    }
    public static void ChangeAssistantItemData(int assistantItem)
    {
        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {"AssistantItem", ""+assistantItem+""},
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnUpdateUserDataInShop, ManagerFuncion.OnAPIError);
    }
    public static void ChangeLastBombItemData(int lastBombItem)
    {
        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {"LastBombItem", ""+lastBombItem+""},
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnUpdateUserDataInShop, ManagerFuncion.OnAPIError);
    }
    public static void ChangeRaptorData(int raptor)
    {
        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {"Raptor", ""+raptor+""},
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnUpdateUserDataInShop, ManagerFuncion.OnAPIError);
    }
    public static void ChangeBlackHawkData(int blackHawk)
    {
        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {"BlackHawk", ""+blackHawk+""},
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnUpdateUserDataInShop, ManagerFuncion.OnAPIError);
    }

    //시스템 관련 데이타 변경 함수//
    public static void ChangeBestScoreData(int bestScore)
    {
        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {"BestScore", ""+bestScore+""},
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnUpdateUserDataInGame, ManagerFuncion.OnAPIError);
    }
    public static void ChangeLevelOfDifficulty(int levelofdifficulty)
    {
        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {"LevelOfDifficulty", ""+levelofdifficulty+""},
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnUpdateUserDataInGame, ManagerFuncion.OnAPIError);
    }
    public static void ChangeRestTimeData(int restTime)
    {
        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {"RestTime", ""+restTime+""},
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnUpdateUserDataInGame, ManagerFuncion.OnAPIError);
    }

    //시작시 유저 데이타 셋팅//
    public static void SetUserDataInStart()
    {
        GetUserDataRequest request = new GetUserDataRequest()
        {
            Keys = null
        };

        PlayFabClientAPI.GetUserData(request, OnSetUserDataInStart, ManagerFuncion.OnAPIError);
    }
    public static void OnSetUserDataInStart(GetUserDataResult result)
    {
        result.DataVersion = 1;
        Debug.Log("Got user data:");
        if (!result.Data.ContainsKey("Fuel"))
        {
            Debug.Log("Now Creating Data");
            SetUserData();
        }
        else
        {
            SetInfoList(result);
            SetShopList(result);
            Debug.Log("ShopData set up complete");
            if (int.Parse(instance.LevelOfDifficulty) != 0) 
                SceneManager.LoadScene("Intro");
            else
                SceneManager.LoadScene("Story");
        }
    }

    //게임중 데이타 변경시 데이타셋팅//
    public static void SetUserDataInShop()
    {
        GetUserDataRequest request = new GetUserDataRequest()
        {
            Keys = null
        };

        PlayFabClientAPI.GetUserData(request, OnSetUserDataInShop, ManagerFuncion.OnAPIError);
    }
    public static void OnSetUserDataInShop(GetUserDataResult result)
    {
        result.DataVersion = 1;
        Debug.Log("Got user data:");
        if (!result.Data.ContainsKey("Fuel"))
        {
            Debug.Log("Now Creating Data");
            SetUserData();

        }
        else
        {
            SetShopList(result);
            Debug.Log("ShopData set up complete");
        }
    }
    public static void SetUserDataInGame()
    {
        GetUserDataRequest request = new GetUserDataRequest()
        {
            Keys = null
        };

        PlayFabClientAPI.GetUserData(request, OnSetUserDataInGame, ManagerFuncion.OnAPIError);
    }
    public static void OnSetUserDataInGame(GetUserDataResult result)
    {
        result.DataVersion = 1;
        Debug.Log("Got user data:");
        if (!result.Data.ContainsKey("Fuel"))
        {
            Debug.Log("Now Creating Data");
            SetUserData();

        }
        else
        {
            SetInfoList(result);
            Debug.Log("ShopData set up complete");
        }
    }

    public static void SetInfoList(GetUserDataResult result)
    {
        instance.LevelOfDifficulty = result.Data["LevelOfDifficulty"].Value; 
        instance.RestTime = result.Data["RestTime"].Value;
        instance.BestScore = result.Data["BestScore"].Value;
        instance.StageData = result.Data["StageData"].Value;
    }
    public static void SetShopList(GetUserDataResult result)
    {
        instance.Fuel = result.Data["Fuel"].Value;
        instance.AddMissileItem = result.Data["AddMissileItem"].Value;
        instance.AssistantItem = result.Data["AssistantItem"].Value;
        instance.LastBombItem = result.Data["LastBombItem"].Value;
        instance.Raptor = result.Data["Raptor"].Value;
        instance.BlackHawk = result.Data["BlackHawk"].Value;
    }
}
