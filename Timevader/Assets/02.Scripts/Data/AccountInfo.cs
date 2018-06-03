using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public interface ISubjectable
{
    void RegisterObserver(IObserverable o);
    void RemoveObserver(IObserverable o);
    void NotifyObservers();
}
public interface IObserverable
{
    void UpdateData(int fuel, int time, int addMissileitem, int assistantitem, int lastBombitem, int raptor,
                    int blackHawk, int bestScore, int restTime, int nextStage);
}
public interface IDisplayable
{
    void DisPlay();
}



public class AccountInfo : MonoBehaviour, ISubjectable
{
    private static AccountInfo instance;
    public static AccountInfo Instance
    {
        get { return instance; }
        set { instance = value; }
    }

    [SerializeField]
    private GetPlayerCombinedInfoResultPayload info;
    public GetPlayerCombinedInfoResultPayload Info
    {
        get { return info; }
        set { info = value; }
    }

    ////잔액확인//
    //[SerializeField]
    //private int myMonoey;
    //public int MyMonoey
    //{
    //    get { return myMonoey; }
    //    set { myMonoey = value; }
    //}



    public string Fuel, Time, AddMissileitem, Assistantitem, LastBombitem, Raptor, BlackHawk, BestScore, RestTime, NextStage;
    private int fuel, time, addMissileitem, assistantitem, lastBombitem, raptor, blackHawk, bestScore, restTime, nextStage;

    List<IObserverable> observerList = new List<IObserverable>();


    private void Awake()
    {
        if (instance != this)
            instance = this;

        DontDestroyOnLoad(gameObject);

        GamePlayManager.Instance.PlayerShipNum = 5;
    }

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
        GetAccountInfo(result.PlayFabId);
    }

    public static void GetAccountInfo(string playfabId)
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

        PlayFabClientAPI.GetPlayerCombinedInfo(request, OnGetAccountInfo, ManagerFuncion.OnAPIError);
    }
    static void OnGetAccountInfo(GetPlayerCombinedInfoResult result)
    {
        Debug.Log("Updated Account Info!");
        Instance.Info = result.InfoResultPayload;

        //Debug.Log(Instance.info.UserVirtualCurrency["GC"]);

        //스토어에서Data끌어오기//
        //StoreDatabase.UpdateDatabase();
        //돈확인하기//
        //UpdateMyMoney();

        GetUserData();

    }
    ////실험으로 만들어본것//
    //public static void UpdateMyMoney()
    //{
    //    Instance.myMonoey = Instance.info.UserVirtualCurrency["GC"];
    //    //Debug.Log(DataBase.Instance.Potion[0].Cost);
    //}
    ////상점구현할때 이거사용할것//
    //public void BuyHPPotion()
    //{
    //    Instance.MyMonoey = Instance.MyMonoey - StoreDatabase.Instance.Potion[0].Cost;


    //}

    public static void SetUserData()
    {
        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                //재화//
                {"Fuel" , "0" },
                {"Time" , "0" },
                //소모성 아이템 종류//
                {"AddMissileitem" , "0" },
                {"Assistantitem" , "0" },
                {"LastBombitem" , "0" },
                //장착성 아이템,전투기 종류//
                {"Raptor" , "0" },
                //{"Thunderblot" , "0" },
                //{"Spirit" , "0" },
                {"BlackHawk", "0"},
                //시스템 관련//
                {"BestScore", "0" },
                {"RestTime" , "0" },
                //{"Stage1Clear" , "0" },
                //{"Stage2Clear" , "0" },
                //{"Stage3Clear" , "0" },
                {"NextStage" , "1" },

            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnSetUserData, ManagerFuncion.OnAPIError);
    }
    static void OnSetUserData(UpdateUserDataResult result)
    {
        Debug.Log("Successfully updated user data");
        //GetUserData();
        
    }

    ////버튼으로 데이타 저장 실험//
    //public void SetDataButton()
    //{
    //    SetUserData();
    //}

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
        PlayFabClientAPI.UpdateUserData(request, OnSetUserData, ManagerFuncion.OnAPIError);
    }
    public static void ChangeTimeData(int time)
    {
        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {"Time", ""+time+""},
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnSetUserData, ManagerFuncion.OnAPIError);
    }
    //소모성 아이템 데이타 변경 함수//
    public static void ChangeAddMissileitemData(int addMissileitem)
    {
        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {"AddMissileitem", ""+addMissileitem+""},
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnSetUserData, ManagerFuncion.OnAPIError);
    }
    public static void ChangeAssistantitemData(int assistantitem)
    {
        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {"Assistantitem", ""+assistantitem+""},
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnSetUserData, ManagerFuncion.OnAPIError);
    }
    public static void ChangeLastBombitemData(int lastBombitem)
    {
        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {"LastBombitem", ""+lastBombitem+""},
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnSetUserData, ManagerFuncion.OnAPIError);
    }
    //장착성 아이템,전투기 데이타 변경 함수//
    ////public static void ChangeThunderblotData(int thunderblot)
    //{
    //    UpdateUserDataRequest request = new UpdateUserDataRequest()
    //    {
    //        Data = new Dictionary<string, string>()
    //        {
    //            {"Thunderblot", ""+thunderblot+""},
    //        }
    //    };
    //    PlayFabClientAPI.UpdateUserData(request, OnSetUserData, ManagerFuncion.OnAPIError);
    //}
    //public static void ChangeSpiritData(int spirit)
    //{
    //    UpdateUserDataRequest request = new UpdateUserDataRequest()
    //    {
    //        Data = new Dictionary<string, string>()
    //        {
    //            {"Spirit", ""+spirit+""},
    //        }
    //    };
    //    PlayFabClientAPI.UpdateUserData(request, OnSetUserData, ManagerFuncion.OnAPIError);
    //}
    public static void ChangeRaptoritemData(int raptor)
    {
        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {"Raptor", ""+raptor+""},
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnSetUserData, ManagerFuncion.OnAPIError);
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
        PlayFabClientAPI.UpdateUserData(request, OnSetUserData, ManagerFuncion.OnAPIError);
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
        PlayFabClientAPI.UpdateUserData(request, OnSetUserData, ManagerFuncion.OnAPIError);
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
        PlayFabClientAPI.UpdateUserData(request, OnSetUserData, ManagerFuncion.OnAPIError);
    }
    public static void ChangeNextStageData(int nextStage)
    {
        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {"NextStage", ""+nextStage+""},
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnSetUserData, ManagerFuncion.OnAPIError);
    }

    public static void GetUserData()
    {
        GetUserDataRequest request = new GetUserDataRequest()
        {
            Keys = null
        };

        PlayFabClientAPI.GetUserData(request, OnGetUserData, ManagerFuncion.OnAPIError);

    }
    public static void OnGetUserData(GetUserDataResult result)
    {
        result.DataVersion = 1;
        Debug.Log("Got user data:");
        if (!result.Data.ContainsKey("Fuel"))
        {
            Debug.Log("Now Creating Data");
            SetUserData();
            Debug.Log("돈다");

        }
        else
        {
            //Debug.Log("Fuel: " + result.Data["Fuel"].Value);
            //Debug.Log("Time: " + result.Data["Time"].Value);
            //Debug.Log("AddMissileitem: " + result.Data["AddMissileitem"].Value);
            //Debug.Log("Assistantitem: " + result.Data["Assistantitem"].Value);
            //Debug.Log("LastBombitem: " + result.Data["LastBombitem"].Value);
            //Debug.Log("Raptor: " + result.Data["Raptor"].Value);
            ////Debug.Log("Thunderblot: " + result.Data["Thunderblot"].Value);
            ////Debug.Log("Spirit: " + result.Data["Spirit"].Value);
            //Debug.Log("BlackHawk: " + result.Data["BlackHawk"].Value);
            //Debug.Log("BestScore: " + result.Data["BestScore"].Value);
            //Debug.Log("RestTime: " + result.Data["RestTime"].Value);
            //Debug.Log("NextStage: " + result.Data["NextStage"].Value);

            instance.Fuel = result.Data["Fuel"].Value;
            instance.Time = result.Data["Time"].Value;
            instance.AddMissileitem = result.Data["AddMissileitem"].Value;
            instance.Assistantitem = result.Data["Assistantitem"].Value;
            instance.LastBombitem = result.Data["LastBombitem"].Value;
            instance.Raptor = result.Data["Raptor"].Value;
            instance.BlackHawk = result.Data["BlackHawk"].Value;
            instance.BestScore = result.Data["BestScore"].Value;
            instance.RestTime = result.Data["RestTime"].Value;
            instance.NextStage = result.Data["NextStage"].Value;

            Debug.Log("1번");
        }
    }

    public void RegisterObserver(IObserverable o)
    {
        observerList.Add(o);
    }

    public void RemoveObserver(IObserverable o)
    {

        observerList.Remove(o);
    }

    public void NotifyObservers()
    {
        for (int i = 0; i < observerList.Count; i++)
        {
            observerList[i].UpdateData(fuel, time, addMissileitem, assistantitem, lastBombitem, raptor,
                                       blackHawk, bestScore, restTime, nextStage);
        }

    }

    public void MeasureChangedData()
    {
        //데이터 확인//
        GetUserData();
        //데이터 인트로 바꾸기//
        ParseData();
        //옵저버에게 알리기//
        NotifyObservers();

        Debug.Log("12344");

    }


    public void ParseData()
    {
        fuel = int.Parse(Fuel);
        time = int.Parse(Time);
        addMissileitem = int.Parse(AddMissileitem);
        assistantitem = int.Parse(Assistantitem);
        lastBombitem = int.Parse(LastBombitem);
        raptor = int.Parse(Raptor);
        blackHawk = int.Parse(BlackHawk);
        bestScore = int.Parse(BestScore);
        restTime = int.Parse(RestTime);
        nextStage = int.Parse(NextStage);
        Debug.Log("2번");

    }



    ////버튼으로 저장한 데이타 출력//
    //public void GetdataButton()
    //{
    //    GetUserData();
    //}
}
